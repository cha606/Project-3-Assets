using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    float health;
    float speed;
    GameObject Body;
    // Start is called before the first frame update
    void Start()
    {
        Body = GetComponent<Rigidbody>();
        health = 100f;
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            Body.transform.position;
        }
    }
}
