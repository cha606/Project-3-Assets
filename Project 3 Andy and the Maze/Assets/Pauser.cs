using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Ui;
    void Start()
    {
        Ui= GameObject.Find("OverPause");
        Ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(Pause) && Ui.setActive){
            Ui.SetActive(false);
        }
        else if (Input.GetButton(Pause)){
            Ui.SetActive(true);
            Time.timeScale=0;
        }
    }
}
