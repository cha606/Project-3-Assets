using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AnimationController))]
public class AllyAIBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    private AllyStats stats;
    private AnimationController animationController;

    public bool stunned;
    public bool invincible;
    public bool inAmbush;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        stats = GetComponent<AllyStats>();
        animationController = GetComponent<AnimationController>();
    }

    void FixedUpdate()
    {
        if(stats.getHP() <= 0)
        {
            animationController.Animate("death");
            StartCoroutine(death(animationController.animationStateLength() * 2f));
        }
        inAmbush = GameObject.FindGameObjectWithTag("Manager").GetComponent<AmbushManager>().isAmbushActive();
        agent.SetDestination(player.transform.position);
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (inAmbush) //if in ambush, stop navigation and panic flip
        {
            agent.isStopped = true;
            //animationController.Animate("flip");
        }

        else if(distance > agent.stoppingDistance) //if not in ambush and not within stopping distance, start navigation
        {
            agent.isStopped = false;
        }

        else if(distance <= agent.stoppingDistance) //if not in ambush and within stopping distance, stop
        {
            agent.isStopped = true;
        }

        //Animations
        if (stats.getHP() <= 0)
        {
            animationController.Animate("death"); //death
        }
        else if (stunned)
        {
            animationController.Animate("attacked"); //attacked
        }
        else if (inAmbush)
        {
            animationController.Animate("flip"); //if in ambush and not dead or attacked, panic flip
        }
        else if (!inAmbush && agent.isStopped) //if not navigating, idle
        {
            animationController.Animate("idle");
        }
        else if(!inAmbush && !agent.isStopped) //if navigating, walk
        {
            animationController.Animate("forward-walk");
        }
    }

    public void attacked()
    {
        if (!invincible)
        {
            stunned = true;
            invincible = true;
            stats.depleteHP(50f);
            StartCoroutine(removeStun(animationController.animationStateLength() * .3f));
            StartCoroutine(removeInvincibility(animationController.animationStateLength()));
        }
    }

    IEnumerator death(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
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
}
