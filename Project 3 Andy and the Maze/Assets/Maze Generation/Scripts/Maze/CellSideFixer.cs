using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CellSideFixer : MonoBehaviour
{
    public GameObject side0; //left side 
    public GameObject side1; //right side 
    public GameObject side2; //top side
    public GameObject side3; //bottom side

    public void deleteSides(bool[] sides)
    {
        if (sides[0])
        {
            side0.layer = 10;
            side0.GetComponent<NavMeshObstacle>().enabled = false;
            Object.Destroy(side0, 0f);
        }
        if (sides[1])
        {
            side1.layer = 10;
            side1.GetComponent<NavMeshObstacle>().enabled = false;
            Object.Destroy(side1, 0f);
        }
        if (sides[2])
        {
            side2.layer = 10;
            side2.GetComponent<NavMeshObstacle>().enabled = false;
            Object.Destroy(side2, 0f);
        }
        if (sides[3])
        {
            side3.layer = 10;
            side3.GetComponent<NavMeshObstacle>().enabled = false;
            Object.Destroy(side3, 0f);
        }
    }
}
