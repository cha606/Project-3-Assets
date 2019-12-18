using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public float speed = 10f;
    public float rotSpeed = 10f;
    public bool still = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float step = rotSpeed * Time.deltaTime;
        if (moveVertical != 0)
        {
            transform.rotation =
                 Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z), step, 0.0f));
            still = false;
        }

        transform.Translate(new Vector3(moveHorizontal, 0f, moveVertical) * Time.deltaTime * speed);
        /*
        if (Input.GetKeyDown(KeyCode.F) && IsGrounded() && still)
        {
            AnimateWave();
            waving = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f)
        {
            AnimateIdle();
            waving = false;
        }

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            still = true;
            if (IsGrounded() && !waving)
                AnimateIdle();
        }
        else
        {
            if (moveVertical < 0)
            {
                AnimateBackwardsRun();
            }
            else
            {
                AnimateRun();
            }
        }
        #endregion

        #region jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !(IsGrounded()))
        {
            if (doubleJumped && jumpBananaCount >= 1)
            {
                extraJumpSound.Play();
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpBananaCount--;
            }
            else if (!doubleJumped)
            {
                firstJumpSound.Play();
                doubleJumped = true;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if (!IsGrounded())
            AnimateJumpFloat();
        else if (IsGrounded())
        {
            doubleJumped = false;
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.L))
        {
            speed += .03f;
            jumpForce += .025f;
        }

        if (Input.GetKeyDown(KeyCode.P) && cam.GetComponent<RPGCamera>().sensitivity < 300)
        {
            cam.GetComponent<RPGCamera>().sensitivity += 25f;
        }

        if (Input.GetKeyDown(KeyCode.O) && cam.GetComponent<RPGCamera>().sensitivity > 0)
        {
            cam.GetComponent<RPGCamera>().sensitivity -= 25f;
        }*/
    }
}
