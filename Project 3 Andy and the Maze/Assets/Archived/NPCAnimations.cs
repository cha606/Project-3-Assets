using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimations : MonoBehaviour
{
    Vector3 lastPosition;
    Vector3 currentPosition;
    public bool inAnimation;
    public bool attacking;
    public NavMeshAgent agent;
    bool inAmbush;
    public Animator animator;
    #region animationMethods
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPosition = GetComponent<Transform>().position;
        lastPosition = currentPosition;
    }
    public void Animate(string boolName) //boolName is the Animator Parameter
    {
        DisableOtherAnimations(animator, boolName);
        animator.SetBool(boolName, true);
    }

    public void DisableOtherAnimations(Animator animator, string animation)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name != animation)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    public void setAmbushState(bool x)
    {
        inAmbush = x;
    }
    #endregion

    // Update is called once per frame
    void FixedUpdate()
    {

        currentPosition = GetComponent<Transform>().position;
        if(tag == "Buddy" && inAmbush && currentPosition == lastPosition && !inAnimation)
        { //monkey friends freeze up when ambushed
            Animate("flip");
            inAnimation = true;
        }
        if (attacking)
        {
            Animate("parry");
            inAnimation = true;
        }
        if (attacking && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.5f) //ends current animation when complete
        {
            Animate("idle");
            inAnimation = false;
            attacking = false;
        }
        if (!attacking && agent.isStopped && !inAnimation)
        {
            Animate("idle");
        }

        if (!agent.isStopped && !attacking)
        {
            Animate("forward-run");
        }
        lastPosition = currentPosition;

    }


}
