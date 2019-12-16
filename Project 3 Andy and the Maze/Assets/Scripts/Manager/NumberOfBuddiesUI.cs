using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class NumberOfBuddiesUI : MonoBehaviour
{
    Text Number;
    // Start is called before the first frame update
    void Start()
    {
        Number = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Number.text = "Number of Monkey Buddies: " + GameObject.FindGameObjectsWithTag("Buddy").Length; 
    }
}
