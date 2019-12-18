using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSideFixer : MonoBehaviour
{
    public GameObject side0; //left side 
    public GameObject side1; //right side 
    public GameObject side2; //top side
    public GameObject side3; //bottom side

    public void deleteSides(bool[] sides)
    {
        if (sides[0])
            Object.Destroy(side0, 0f);
        if (sides[1])
            Object.Destroy(side1, 0f);
        if (sides[2])
            Object.Destroy(side2, 0f);
        if (sides[3])
            Object.Destroy(side3, 0f);
    }
}
