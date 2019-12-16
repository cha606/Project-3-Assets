using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
    public GameObject GUI;
    void Start()
    {
        Player= GameObject.FindWithTag("Player");
        GUI.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Parry(GameObject Attacker, GameObject Defender)
    {
        //Player.CanBeAttacked = false;
        bool Success = false;

        GUI.SetActive(true);
        yield return new WaitForSeconds(5);
        if (Input.GetButton("ParryButton")){Success = true;}
        if (Success){Destroy(Attacker);}  //We could have an animation
        else{Player.GetComponent<Player>().Health -=20;}

    }
    
}
