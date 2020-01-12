using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AnimationController))]
public class EnemyAIBehavior : MonoBehaviour
{
    private NavMeshAgent agent;

    private GameObject player;
    private GameObject closestBuddy;
    public GameObject closestTarget;

    private AnimationController animationController;

    public float lookRadius = 10f;
    public float attackCooldown = 0f;

    public bool omaeWaMouShindeiru;
    public bool attacking; //for animations
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animationController = GetComponent<AnimationController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (omaeWaMouShindeiru)
        {
            if (collision.gameObject.tag.Equals("Wall"))
            {
                GetComponent<Animator>().enabled = true;
                animationController.enabled = true;
                faceTarget();
                animationController.Animate("attacked");
                StartCoroutine(dieAfterDelay(animationController.animationStateLength()));
            }
        }
    }
    void FixedUpdate()
    {
        attackCooldown -= Time.deltaTime;
        //check if findgameobjectswithtag is null, and if it is, then set target to player and that should be the end of story. unless for some reason its not being null even after all the monkes be dead.
        closestTarget = decideTarget();

        if (!omaeWaMouShindeiru && getDistance(closestTarget) <= agent.stoppingDistance) //if within attacking range
        {
            agent.isStopped = true; //stop moving
            faceTarget();  //face target before attempting to attack

            if(attackCooldown <= 0f) //Only attacks if cooldown is ready
            {
                if(closestTarget == player && !player.GetComponent<PlayerScript>().getEngaged()) //too lazy to rewrite code...
                {
                    attack();
                }
            }
        }

        else if (!omaeWaMouShindeiru && getDistance(closestTarget) <= lookRadius) //if not within attacking range but within aggro range
        {
            agent.isStopped = false; //start moving
            agent.SetDestination(closestTarget.transform.position); //Destination set to target position
        }

        else if(!omaeWaMouShindeiru && getDistance(closestTarget) > lookRadius) //if not within aggro range
        {
            agent.isStopped = true; //stop moving
        }

        if (!omaeWaMouShindeiru && !attacking && agent.isStopped)
        {
            animationController.Animate("idle");
        }
        else if (!omaeWaMouShindeiru && !agent.isStopped) //if navigating, walk
        {
            animationController.Animate("forward-walk");
        }
    }

    GameObject decideTarget()
    {
        if (GameObject.FindGameObjectWithTag("Buddy") == null)
        {
            return player;
        }
        GameObject trgt;
        //Stores distance from enemy to player and the 'closest buddy'
        float playerDistance = getDistance(player);
        float closestBuddyDistance = getDistance(getClosestBuddy());
        if (playerDistance <= lookRadius)
        {
            trgt = player; //if player is within range
        }
        else if (closestBuddyDistance <= lookRadius)
        {
            trgt = getClosestBuddy(); //if player is not within range, and buddy is
        }
        else
        {
            trgt = player;
        }

        return trgt;    
    }
    GameObject getClosestBuddy()
    {
        GameObject[] buddies = GameObject.FindGameObjectsWithTag("Buddy");
        if(GameObject.FindGameObjectWithTag("Buddy") == null)
        {
            return null;
        }
        GameObject closest = buddies[0];
        float closestBuddyDistance = Vector3.Distance(buddies[0].transform.position, transform.position); //dummy value
        for (int i = 0; i < buddies.Length; i++)
        {
            float tempDistance = Vector3.Distance(buddies[i].transform.position, transform.position);
            if (buddies[i].activeSelf && tempDistance < closestBuddyDistance)
            {
                closest = buddies[i];
            }
        }
        return closest;
    }
    float getDistance(GameObject x)
    {
        return Vector3.Distance(x.transform.position, transform.position);
    }

    void faceTarget()
    {
        Vector3 direction = (closestTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void attack()
    {
        attacking = true;
        attackCooldown = 3f; //resets cooldown
        animationController.Animate("parry");
        float delay = animationController.animationStateLength() * .35f;
        StartCoroutine(DoDamage(delay)); //does damage after attacking animation is complete
    }

    IEnumerator DoDamage(float delay)
    {
        yield return new WaitForSeconds(delay);

        float newDistance = Vector3.Distance(closestTarget.transform.position, transform.position);
        if (newDistance <= agent.stoppingDistance)
        {
            if(closestTarget.tag == "Player")
            {
                closestTarget.GetComponent<PlayerScript>().startDefense(gameObject);
                closestTarget.GetComponent<PlayerScript>().setEngaged(true);
            }
            else if(closestTarget.tag == "Buddy" && closestTarget.GetComponent<AllyStats>().getHP() > 0)
            {
                closestTarget.GetComponent<AllyAIBehavior>().attacked();
            }
            attacking = false;
        }
    }

    public void rocketPunch(Vector3 direction) //RRRRAUWWCKEHTUHH PAWWWWWNCH!!11
    {
        omaeWaMouShindeiru = true;  //ignore this
        agent.enabled = false;
        
        GetComponent<Animator>().enabled = false;
        animationController.enabled = false;
        faceTarget();
        GetComponent<Rigidbody>().velocity = new Vector3(direction.x * 100f, direction.y * 5f, direction.y * 100f);
        StartCoroutine(dieAfterDelay(3f));
        
    }

    IEnumerator dieAfterDelay(float delay) //kills in 5 seconds if collision does not occur.
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
