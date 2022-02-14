using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damage : MonoBehaviour
{
    int critDamage = 50;
    int normalDamage = 25;
    public float critPercent = 50;
    System.Random random = new System.Random();
    int critMin = 0;
    int critMax = 100;
    

    public int DamageCalculate()
    {
        int critResult = random.Next(critMin, critMax);
        if (!IsCrit(critResult))
        {
            return normalDamage;
        }
        else
        {
            return critDamage;
        }

    }

    static bool IsCrit(int result)
    {
        bool isCrit = true;
        int critPercent = 50;

        if(result < critPercent)
        {
            return isCrit;
        }
        else
        {
            return !isCrit;
        }        
    }
}
