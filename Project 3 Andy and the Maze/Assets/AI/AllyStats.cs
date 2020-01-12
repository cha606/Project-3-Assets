using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStats : MonoBehaviour
{
    public float hp;
    public float maxHP = 100;

    void Start()
    {
        hp = maxHP;
    }

    public float getHP()
    {
        return hp;
    }


    public float getMaxHP()
    {
        return maxHP;
    }

    public void depleteHP(float x)
    {
        hp -= x;
    }

    public void restoreHP(float x)
    {
        hp += x;
    }

}
