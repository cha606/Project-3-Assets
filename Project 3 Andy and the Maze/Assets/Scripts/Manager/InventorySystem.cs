using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Start is called before the first frame update
    public List <string> Inventory = new List<string>();
    public string [] PossiblePowerUps = new string[5]; //Change later
    //Stack wont be used so player can access each power up
    public GameObject [] Inv;
    void Start()
    {
        Inv = GameObject.FindGameObjectsWithTag("Inventory");
    for (int i =1; i<10; i++){
        Inventory[i] = "Empty";
    }
     for (int i=1; i<9; i++){
         /*
        Sprite Changing;
        switch(Inventory[i]){
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
        case Empty:
            Changing = null;
            }
        break;
        if (Changing!=null){
        Inv[i].GetComponents<Image>().sprite=Changing;}
        else Inv[i].GetComponent<Image>().SetActive(false);*/
     }
    }

    // Update is called once per frame
    void Update()
    {
        while (Inventory.Count <= 9){Inventory.Add("Empty");}
        while (Inventory.Count >= 9){Inventory.Remove("Empty");}
    }

    void Add(string item){
        if (Inventory.Count>=9){Debug.Log("Inventory is already full");
                return;}
        Inventory.Add(item);
        Inv[Inventory.Count-1].SetActive(true);
        
    }
    void remove(string item){/////////////////Important we need to add power ups and find away to fix up the remove function
        for (int i = Inventory.Count; i>0; i--){
            if (Inventory[i] == item){
                Inventory[i] = "Empty";
                Inv[i].SetActive(false);
            }
        }
    }
}
