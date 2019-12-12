using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
    GameObject GUI;
    char ParryButton;
    void Start()
    {
        Player= GameObject.FindWithTag("Player");
        GUI.setActive=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Parry(GameObject Attacker, GameObject Defender)
    {
        //Player.CanBeAttacked = false;
        bool Success = false;

        GUI.setActive=true;
        yield return new WaitForSeconds(5);
        if (Input.GetKey(KeyCode.ParryButton)){Success = true;}
        if (Success){Destroy(Attacker);}  //We could have an animation
        //else(Player.Health -=1);

    }
    
}
