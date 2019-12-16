using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Start is called before the first frame update
    public string [] Inventory = new string [9];
    public string [] PossiblePowerUps = new string[5]; //Change later
    //Stack wont be used so player can access each power up

    void Start()
    {
    for (int i =1; i<10; i++){
        Inventory[i] = "Empty";
    }
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Add(string item){
        int i=0;
        while(Inventory[i]== "Empty"){
            if (i<8)
            {i++;}
        }
        if (Inventory[i]!="Empty"){Inventory[i] = item;}
        else Debug.Log(item + " could not be added as the inventory was too big;");
        GameObject [] Inv = GameObject.FindGameObjectsWithTag("Inventory");
        Inv[i].SetActive(true);
        /*
        Sprite Changing;
        switch(item){
        case PossiblePowerUps[0]:
            Changing = ;
        case PossiblePowerUps[1]:
            Changing = ;
        case PossiblePowerUps[2]:
            Changing = ;
        case PossiblePowerUps[3]:
            Changing = ;
        case PossiblePowerUps[4]:
            Changing = ;
            }
        Inv[i].GetComponents<Image>().sprite=Changing;*/
    }
    void remove(string item){/////////////////Important we need to add power ups and find away to fix up the remove function
        for (int i = Inventory.Length; i>0; i--){
            GameObject [] Inv = GameObject.FindGameObjectsWithTag("Inventory");
            if (Inventory[i] == item){
                Inventory[i] = "Empty";
                Inv[i].SetActive(false);
            }
        }
    }
}
