using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Click on region and Ctrl+M, M to expand/collapse.

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(PlayerStats))]

public class PlayerScript : MonoBehaviour
{
    public bool godMode;

    public Camera cam;
    private float rotSpeed = 10f; //camera rotation speed

    private float moveHorizontal; //horizontal input
    private float moveVertical; //vertical input

    public float speed; //movement speed
    public float defaultSprintSpeed; //running speed
    public float defaultSprintCost;
    public float defaultWalkSpeed; //walking speed

    public float rollDelay; //can only roll once per second.
    public float defaultRollDelay;
    public float defaultRollCost;

    public float parryCooldown;
    public float defaultParryCooldown;

    public bool parrying;
    public bool sprinting;
    public bool rolling;
    public bool stunned;
    public bool invincible;
    public bool engaged;

    private PlayerStats playerStats;

    private AnimationController animationController;

    void Start()
    {
        defaultSprintCost = Time.deltaTime * 10f;
        defaultSprintSpeed = 7f;
        defaultWalkSpeed = 4f;
        rollDelay = -1;
        defaultRollDelay = .5f;
        defaultRollCost = 10f;
        parryCooldown = -1f;
        defaultParryCooldown = 0f;
        parrying = false;
        sprinting = false;
        rolling = false;
        stunned = false;
        invincible = false;
        engaged = false;
        playerStats = GetComponent<PlayerStats>();

        animationController = GetComponent<AnimationController>();
    }

    void FixedUpdate()
    {
        #region cameraSensitivity
        if (Input.GetKeyDown(KeyCode.P) && cam.GetComponent<RPGCamera>().sensitivity < 300)
        {
            cam.GetComponent<RPGCamera>().sensitivity += 25f;
        }

        if (Input.GetKeyDown(KeyCode.O) && cam.GetComponent<RPGCamera>().sensitivity > 0)
        {
            cam.GetComponent<RPGCamera>().sensitivity -= 25f;
        }
        #endregion

        else if (!rolling && !parrying && parryCooldown <= 0f && playerStats.getHP() > 0 && !stunned && Input.GetKey(KeyCode.E)) // Press E to parry (action)
        {
            parrying = true;
            animationController.Animate("parry");
            StartCoroutine(noLongerParrying(animationController.animationStateLength()));
            parryCooldown = defaultParryCooldown;
            StartCoroutine(removeEngaged(animationController.animationStateLength()));
        }

        if (!godMode && playerStats.getHP() <= 0)
        {
            animationController.Animate("death");
            StartCoroutine(death(animationController.animationStateLength() * 2f)); //Make sure to make ending screen
        }

        if (!parrying) //decreases parry delay over time
        {
            parryCooldown -= Time.deltaTime;
        }

        if (!rolling) //decreases roll delay over time
        {
            rollDelay -= Time.deltaTime;
        }

        if (sprinting || rolling) //faster if springint or rolling
        {
            speed = defaultSprintSpeed;
        }

        else if(!stunned) //restores stamina over time if not sprinting or rolling
        {
            speed = defaultWalkSpeed;
            if(playerStats.getStamina() < playerStats.getMaxStamina())
            {
                playerStats.restoreStamina(Time.deltaTime * 15f);
            }
        }

        if (playerStats.getHP() > 0 && stunned) //attacked animation if stunned
        {
            animationController.Animate("attacked");
        }

        else if(playerStats.getHP() > 0 && rolling) //rolling animation if rolling
        {
            invincible = true;
            StartCoroutine(removeInvincibility(animationController.animationStateLength()));
        }

        #region movement
            moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        float step = rotSpeed * Time.deltaTime;
        if (!parrying && !animationController.IsCurrentlyInAction() && !stunned) //move if not in action
        {
            if (moveVertical != 0)
            {
                transform.rotation =
                     Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z), step, 0.0f));
            }
            transform.Translate(new Vector3(moveHorizontal, 0f, moveVertical) * Time.deltaTime * speed);
        }
        #endregion

        #region movementAnimations
        if (!parrying && playerStats.getHP() > 0 && (moveHorizontal != 0 || moveVertical != 0) && !stunned)
        {
            if (moveVertical == 0) //if not moving forward or backward
            {
                if (moveHorizontal < 0) //if moving left
                {
                    if (Input.GetKey(KeyCode.Space) && playerStats.getStamina() >= 10f && rollDelay <= 0) //if Space is pressed /AKJDNWAKJDNAWKJDNAWKJDNAKWJDNAKWJND
                    {
                        rolling = true;
                        sprinting = false;
                        animationController.Animate("left-roll"); //left roll
                        StartCoroutine(noLongerRolling(animationController.animationStateLength()));
                        playerStats.depleteStamina(defaultRollCost);
                        rollDelay = defaultRollDelay;
                    }
                    else if(!rolling) //if not rolling
                    {
                        sprinting = false;
                        animationController.Animate("walk-left"); //walk left
                    }
                }
                else if (moveHorizontal > 0) //if moving right
                {
                    if (Input.GetKey(KeyCode.Space) && playerStats.getStamina() >= 10f && rollDelay <= 0) //if Space is pressed /AWKDBAWKJDNAWKLJDNAKWJNDKAWJNDKAWJNDKAWJND
                    {
                        rolling = true;
                        sprinting = false;
                        animationController.Animate("right-roll"); //right roll
                        StartCoroutine(noLongerRolling(animationController.animationStateLength()));
                        playerStats.depleteStamina(defaultRollCost);
                        rollDelay = defaultRollDelay;
                    }
                    else if(!rolling) //if not rolling
                    {
                        sprinting = false;
                        animationController.Animate("walk-right"); //walk left
                    }
                }
            }
            
            else if (moveVertical > 0) //if moving forward
            {
                if (Input.GetKey(KeyCode.Space) && playerStats.getStamina() >= 10f && rollDelay <= 0) //if Space is pressed /AWDNAKLWDNJAWKJNDAKWJDNAKWDNAKJWDNAKWNDAKWJNDAKWDN
                {
                    rolling = true;
                    sprinting = false;
                    speed = defaultSprintSpeed;
                    animationController.Animate("front-roll"); //roll forward
                    StartCoroutine(noLongerRolling(animationController.animationStateLength()));
                    playerStats.depleteStamina(defaultRollCost);
                    rollDelay = defaultRollDelay;
                }
                else if (!rolling && Input.GetKey(KeyCode.LeftShift) && playerStats.getStamina() >= Time.deltaTime * 10f) //else if Shift is held and Space is not pressed
                {
                    sprinting = true;
                    animationController.Animate("forward-run"); //run forward
                    playerStats.depleteStamina(defaultSprintCost);
                }
                else if(!rolling) //if Shift is not held and Space is not pressed
                {
                    sprinting = false;
                    if (moveHorizontal < 0) //if moving left
                    {
                        animationController.Animate("forwardleft-walk"); //walk forward and left
                    }
                    else if (moveHorizontal > 0) //if moving right
                    {
                        animationController.Animate("forwardright-walk"); //walk forward and right
                    }
                    else //if not moving left or right
                    {
                        animationController.Animate("forward-walk"); //walk forward
                    }
                }
            }

            else if (playerStats.getHP() > 0 && moveVertical < 0) //if moving backward
            {
                if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space) && playerStats.getStamina() >= 10f && rollDelay <= 0) //if Space is pressed /AWDNAKLWDNJAWKJNDAKWJDNAKWDNAKJWDNAKWNDAKWJNDAKWDN
                {
                    rolling = true; //rolling uses sprint speed, also can't restore stamina while rolling
                    sprinting = false;
                    animationController.Animate("back-roll"); //roll backward
                    StartCoroutine(noLongerRolling(animationController.animationStateLength()));
                    playerStats.depleteStamina(defaultRollCost);
                    rollDelay = defaultRollDelay;
                }
                else if (!rolling && Input.GetKey(KeyCode.LeftShift) && playerStats.getStamina() >= Time.deltaTime * 10f) //else if Shift is held and Space is not pressed
                {
                    sprinting = true;
                    animationController.Animate("backward-run"); //run forward
                    playerStats.depleteStamina(defaultSprintCost);
                }
                else if(!rolling) //if Shift is not held and Space is not pressed
                {
                    sprinting = false;
                    if (moveHorizontal < 0) //if moving left
                    {
                        animationController.Animate("backwardleft-walk"); //walk backward and left
                    }
                    else if (moveHorizontal > 0) //if moving right
                    {
                        animationController.Animate("backwardright-walk"); //walk backward and right
                    }
                    else //if not moving left or right
                    {
                        animationController.Animate("backward-walk"); //walk backward
                    }
                }
            }
        }
        #endregion

        #region actions
        else if (playerStats.getHP() > 0 && !stunned && Input.GetKey(KeyCode.Space) && playerStats.getStamina() >= 10f && rollDelay <= 0) //Press Space when not moving to flip (action)
        {
            rolling = true; //rolling uses sprint speed, also can't restore stamina while rolling
            sprinting = false;
            animationController.Animate("flip");
            playerStats.depleteStamina(defaultRollCost);
            rollDelay = defaultRollDelay;
            StartCoroutine(noLongerRolling(animationController.animationStateLength()));
        }
        else if (playerStats.getHP() > 0 && !stunned && Input.GetKey(KeyCode.F)) // Press F when not moving to wave (action)
        {
            animationController.Animate("wave");
        }
        else if (playerStats.getHP() > 0 && !stunned && Input.GetKey(KeyCode.X)) //press X to pick up item (action)
        {
            animationController.Animate("pickup");
        }
        /*else if (!parrying && parryCooldown <= 0f && playerStats.getHP() > 0 && !stunned && Input.GetKey(KeyCode.E)) // Press E to parry (action)
        {
            parrying = true;
            animationController.Animate("parry");
            StartCoroutine(noLongerParrying(animationController.animationStateLength()));
            parryCooldown = defaultParryCooldown;
        }*/
        #endregion

        else if(!parrying && playerStats.getHP() > 0 && !stunned) //if not moving or doing action
        {
            animationController.Animate("idle");
        }
    }

    public bool getEngaged()
    {
        return engaged;
    }

    public void setEngaged(bool x)
    {
        engaged = x;
    }

    public void startDefense(GameObject other) //when attacked, parry or failed parry
    {
        if (isFacingTarget(other) && parrying)
        {
            invincible = true;
            successfulParry(other);
            StartCoroutine(removeInvincibility(animationController.animationStateLength() * 2f));
            StartCoroutine(removeEngaged(1f));
        }
        else
        {
            missedParry();
        }
    }

    public void successfulParry(GameObject other)
    {
        Vector3 direction = (other.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        other.GetComponent<EnemyAIBehavior>().rocketPunch(direction);
    }

    public void missedParry()
    {
        if (!invincible && !godMode)
        {
            stunned = true;
            invincible = true;
            playerStats.depleteHP(50f);
            StartCoroutine(removeStun(animationController.animationStateLength() * .3f));
            StartCoroutine(removeInvincibility(animationController.animationStateLength()));
            StartCoroutine(removeEngaged(animationController.animationStateLength()));
        }
    }

    bool isFacingTarget(GameObject other)
    {
        float dot = Vector3.Dot(transform.forward, (other.transform.position - transform.position).normalized);
        if (dot > 0.7f)
        {
            return true;
        }
        return false;
    }

    IEnumerator noLongerRolling(float delay)
    {
        yield return new WaitForSeconds(delay);
        rolling = false;
    }
    IEnumerator noLongerParrying(float delay)
    {
        yield return new WaitForSeconds(delay);
        parrying = false;
    }
    IEnumerator removeStun(float delay)
    {
        yield return new WaitForSeconds(delay);
        stunned = false;
    }

    IEnumerator removeInvincibility(float delay)
    {
        yield return new WaitForSeconds(delay);
        invincible = false;
    }

    IEnumerator removeEngaged(float delay)
    {
        yield return new WaitForSeconds(delay);
        engaged = false;
    }

    IEnumerator death(float delay) //sets player to inactive after a few seconds
    {
        yield return new WaitForSeconds(delay);
        //gameObject.SetActive(false);
    }
}