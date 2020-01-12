using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] allies;
    void Start()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject monkeyBuddy = UnityEngine.Object.Instantiate(allies[i], spawnPoints[i].transform.position, new Quaternion(0f, 0f, 0f, 0f));
        }
    }
}
