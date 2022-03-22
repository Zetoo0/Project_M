using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static bool IsLevelUnlocked(string levelName)
    {
        bool isLevelUnlocked;
        int lockValue = PlayerPrefs.GetInt(levelName);

        if(lockValue == 1)
        {
            return isLevelUnlocked = true;
        }
        else
        {
            return isLevelUnlocked = false;
        }

        
    }
}
