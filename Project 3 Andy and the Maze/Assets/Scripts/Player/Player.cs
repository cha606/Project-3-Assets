using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour

{
    // Start is called before the first frame update
    public float JumpFactor=10f;
    public float Health = 100f;
    public bool isGrounded = true;
    public GameObject DeathScreen;
    //public GameObject WinScreen;

    
    public float runFactor;
    //public AudioSource run;
    //public AudioSource jump;
    //public AudioSource LEVELUP;
    public Rigidbody rb;
    
    public void Start()
    {
        DeathScreen.SetActive(false);
        //WinScreen.SetActive(false);
        Health=50f;
        
        JumpFactor = 3f;
        runFactor =5f;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    public void Update()
    {
        
        if (Input.GetKey("w")) { //run.Play(); 
        transform.position += Vector3.forward*Time.deltaTime*runFactor;}
        if (Input.GetKey("a")) { //run.Play(); 
        transform.position += Vector3.left*Time.deltaTime*runFactor; }
        if (Input.GetKey("s")) { //run.Play(); 
        transform.position += Vector3.forward *-1*Time.deltaTime*runFactor;}
        if (Input.GetKey("d")) { //run.Play(); 
        transform.position += Vector3.right*Time.deltaTime*runFactor; }
        if (Input.GetButton("Jump") && isGrounded)
        {
            //jump.Play(); 
            rb.AddForce(Vector3.up * JumpFactor, ForceMode.Impulse);
            
            isGrounded = false;
        }
        if (transform.position.y <= -45f || Health <=0f) { Dead(); }
    
    }
    public float GetHealth(){return Health;}
    public void Dead() { 
        //setActive(false); DeathScreen.SetActive(true);  Time.timeScale = 0;
        }
    
    //public void win() {Being.SetActive(false); WinScreen.SetActive(true); //Debug.Log("Active");}
    public void TakeDamage(float amount)
    {
        Health -= amount;
    }

    public void OnCollisionStay(Collision col) {
        
        isGrounded = true; 
        //}

}

}
