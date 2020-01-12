using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBars : MonoBehaviour
{
    public Image HP;
    public Image Stamina;

    void FixedUpdate()
    {
        HP.fillAmount = GameObject.FindWithTag("Player").GetComponent<PlayerStats>().getHP()/GameObject.FindWithTag("Player").GetComponent<PlayerStats>().getMaxHP();
        Stamina.fillAmount = GameObject.FindWithTag("Player").GetComponent<PlayerStats>().getStamina()/GameObject.FindWithTag("Player").GetComponent<PlayerStats>().getMaxStamina();
    }
}
