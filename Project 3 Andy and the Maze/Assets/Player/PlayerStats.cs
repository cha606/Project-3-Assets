using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp;
    public float stamina;
    public float maxHP = 200;
    public float maxStamina = 100;
    public float baseDamage;

    void Start()
    {
        hp = maxHP;
        stamina = maxStamina;
        baseDamage = 100;
    }

    void Update()
    {
        
    }

    public float getHP()
    {
        return hp;
    }

    public float getStamina()
    {
        return stamina;
    }

    public float getMaxHP()
    {
        return maxHP;
    }

    public float getMaxStamina()
    {
        return maxStamina;
    }
    public void depleteHP(float x)
    {
        hp -= x;
    }

    public void depleteStamina(float x)
    {
        stamina -= x;
    }

    public void restoreHP(float x)
    {
        hp += x;
    }

    public void restoreStamina(float x)
    {
        stamina += x;
    }
}
