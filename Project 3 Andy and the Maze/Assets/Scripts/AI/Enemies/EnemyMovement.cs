using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject Player;
    public GameObject self;
    private GameObject [] Buddy;
    List<GameObject> Entity;
    public GameObject Parry;
    public float Speed=10f;
    public int vision = 10;
    // Start is called before the first frame update
    void Start()
    {
        Parry = GameObject.Find("ParryManager");
        Player= GameObject.FindGameObjectWithTag("Player");
        Buddy = GameObject.FindGameObjectsWithTag("Buddy");
        for(int i=0; i<Buddy.Length; i++ ){
            Entity.Add(Buddy[i]);
        }
        Entity.Add(Player);
        
    }

    // Update is called once per frame
    void Update()
    {   GameObject Agro = Entity[0];
        float dist= Vector3.Distance(transform.position, Entity[0].transform.position);
        for (int i =0; i<Entity.Count; i++){
            if (Vector3.Distance(transform.position, Entity[i].transform.position)<dist){
            Agro=Entity[i];
            dist = Vector3.Distance(transform.position, Entity[i].transform.position);
        } 
        }
        
        if (Vector3.Distance(transform.position, Agro.transform.position)<vision){
            transform.position = Vector3.MoveTowards(transform.position, Agro.transform.position, Speed*Time.deltaTime);
        }
        
        if (Vector3.Distance(transform.position, Player.transform.position)<3){
            Parry.GetComponent<ParrySystem>().Parry(self, Player);
        }
    }
}
