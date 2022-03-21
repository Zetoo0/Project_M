using UnityEngine;
//using System;

public class Damage : MonoBehaviour
{
    [SerializeField] int critDamage = 50;
    [SerializeField] int normalDamage = 25;
    [SerializeField] int critMin = 0;
    [SerializeField] int critMax = 100;
    [SerializeField] static public bool isCritted;




    /*public int DamageCalculate()
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
    }*/

    public int DamageCalculate()
    {
        int critResult = Random.Range(critMin, critMax); //Same as using C# own System just include the numbers rnd.Next(critMin, critMax);
        if (!IsCrit(critResult))
        {
            return normalDamage;
        }
        else
        {
            return critDamage;
        }
    }

    bool IsCrit(int result)
    {
        bool isCrit;
        int critPercent = 50;

        if (CritChanceValue())
        {
            if (result < critPercent)
            {
                ShowDPS.isCritted = false;
                return isCrit = false;
            }
            else
            {
                ShowDPS.isCritted = true;
                return isCrit = true;
            }
        }
        else
        {
            if (result > critPercent)
            {
                ShowDPS.isCritted = false;
                return isCrit = false;
            }
            else
            {
                ShowDPS.isCritted = true;
                return isCrit = true;
            }
        }
    }

    bool CritChanceValue()
    {
        bool itShouldBeLittle;
        int littleOrBiggerNum = Random.Range(1, 2);
        if (littleOrBiggerNum == 1)
        {
            return itShouldBeLittle = true;
        }
        else
        {
            return itShouldBeLittle = false;
        }
    }
}
