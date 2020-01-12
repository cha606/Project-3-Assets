using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambush : MonoBehaviour
{
    private System.Random randy;

    public GameObject indicator;
    public GameObject[] spawnPoints;

    public GameObject enemy1;
    public int enemyCount = 0;

    private bool spawned = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && spawned == false)
        {
            randy = new System.Random();
            List<int> usedUpSpots = new List<int>();
            int enemyAmount = randy.Next(2, 5);
            for (int i = 0; i < enemyAmount; i++)
            {
                int location = randy.Next(9);
                usedUpSpots.Add(location);
                while (usedUpSpots.Contains(location))
                {
                    location = randy.Next(9);
                }
                spawnEnemy(enemy1, spawnPoints[location]);
                enemyCount++;
            }
            spawned = true;
        }
    }

    private void spawnEnemy(GameObject e, GameObject spLocation)
    {
        UnityEngine.Object.Instantiate(e, spLocation.transform.position, new Quaternion(0f, 0f, 0f, 0f));
    }
}
