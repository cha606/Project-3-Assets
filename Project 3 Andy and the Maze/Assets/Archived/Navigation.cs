using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour //Navigation script for monkey friends
{
    public NavMeshAgent agent;
    public GameObject player;
    public bool stopped;

    void Update()
    {
        stopped = agent.isStopped;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        agent.SetDestination(player.transform.position);
        if (distance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }

    public void stopNavigation() //temporarily pauses navigation (when ambushed)
    {
        agent.isStopped = true;
    }

    public void resumeNavigation() //resumes navigation
    {
        agent.isStopped = false;
    }
}
