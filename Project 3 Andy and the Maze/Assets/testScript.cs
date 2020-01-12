using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(-10f, 10f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
