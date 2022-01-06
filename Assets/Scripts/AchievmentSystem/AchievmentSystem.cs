using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentSystem : MonoBehaviour
{
    int killedEnemy = 0;

    void Start()
    {
        PlayerPrefs.DeleteAll();

        PointsOfInterest.OnPointsOfInteresEntered += 
            PointsOfInterest_OnPointsOfInterestEntered;

        PlayerBasedAchievments.OnKilledEnemy += PlayerBasedAchievments_OnKilledEnemy;
    }

    /*private void OnDestroy()
    {
        PointsOfInterest.OnPointsOfInteresEntered -=
            PointsOfInterest_OnPointsOfInterestEntered;
    }*/

    void PointsOfInterest_OnPointsOfInterestEntered(string poiName)
    {
        string achievmentKey = "achievment" + poiName;

        if (PlayerPrefs.GetInt(achievmentKey) == 1)
            return;

        PlayerPrefs.SetInt(achievmentKey, 1);
        Debug.Log("Unlocked " + poiName);


    }

    void PlayerBasedAchievments_OnKilledEnemy(string enemyName)
    {
        string achievmentKey = "achievment" + enemyName;
        killedEnemy++;
        
        switch (killedEnemy)
        {

            case 1:
                PlayerPrefs.SetInt(achievmentKey, 1);
                killedEnemy = 1;
                Debug.Log("You're a real " + enemyName + " kid killer");
                break;
            case 10:
                PlayerPrefs.SetInt(achievmentKey, 10);
                killedEnemy = 10;
                Debug.Log(enemyName + "Slayer");
                break;
            default:
                killedEnemy++;
                Debug.Log("Killed enemies: " + killedEnemy);
                break;
        }
    }
}
