using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushManager : MonoBehaviour //Spawns Ambush Spheres periodically in front of Andy
{
    private GameObject player;
    public GameObject ambush;

    public bool inAmbush;

    private System.Random randy;
    private float seconds = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        randy = new System.Random();
    }

    void FixedUpdate()
    {
        if(GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            endAmbush();
        }

        //Every 5 seconds...
        if (!inAmbush && seconds > 5)
        {
            //There is a 1 in 2 chance of spawning an ambush
            if (randy.Next(0, 2) == 0)
            {
                Ray playerDown = new Ray(player.GetComponent<Transform>().position, Vector3.down);
                RaycastHit[] iveBeenHit = Physics.RaycastAll(player.GetComponent<Transform>().position + new Vector3(0f, 5f, 0f), Vector3.down);
                foreach (RaycastHit hitThing in iveBeenHit)
                    if (hitThing.transform.parent.tag.Equals("Cell"))
                    {
                        UnityEngine.Object.Instantiate(ambush, hitThing.transform.position, Quaternion.identity);
                        inAmbush = true;
                    }
            }
            seconds -= 5; //resets timer
        }
        else if (!inAmbush)
            seconds += Time.deltaTime;
    }

    public bool isAmbushActive()
    {
        return inAmbush;
    }

    public void endAmbush()
    {
        inAmbush = false;
    }
}