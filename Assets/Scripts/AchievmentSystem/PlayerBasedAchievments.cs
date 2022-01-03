using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasedAchievments : MonoBehaviour
{
    public static event Action<string> OnKilledEnemy;

    [SerializeField] private string enemyName;

    public string EnemyName { get { return enemyName; } }

    void OnDestroy()
    {
        if (OnKilledEnemy != null)
            OnKilledEnemy(this.enemyName);
    }

}
