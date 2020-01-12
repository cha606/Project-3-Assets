using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private static string[] actions = new string[] { "flip", "wave", "parry", "pickup", "attacked", "death" };

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Animate(string boolName) //boolName is the Animator Parameter
    {
        bool currentAnimationIsAction = IsCurrentlyInAction();
        if (currentAnimationIsAction) //if current animation is an action
        {
            if (animationTime() >= 1f) //if current animation is complete
            {
                DisableOtherAnimations(boolName); //Disables other animation(s)
                animator.SetBool(boolName, true); //Starts boolName animation
            }
        }

        else if (!currentAnimationIsAction) //if current animation is not an action
        {
            DisableOtherAnimations(boolName); //Disables other animation(s)
            animator.SetBool(boolName, true);
        }
    }

    public void DisableOtherAnimations(string animation)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name != animation)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    private float animationTime() //returns the normalized time of the current animation. 1 is the end of the animation. 0.5 is the middle of the animation.
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public float animationStateLength()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public bool IsCurrentlyInAction()
    {
        bool currentAnimationIsAction = false; //checks if current animation is an action
        for (int i = 0; i < actions.Length; i++)
        {
            if (CurrentAnimationIs(actions[i]))
            {
                currentAnimationIsAction = true;
                break;
            }
        }
        return currentAnimationIsAction;
    }

    public bool CurrentAnimationIs(string other) //checks if the name of the current animation is 'other'.
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(other);
    }

}
