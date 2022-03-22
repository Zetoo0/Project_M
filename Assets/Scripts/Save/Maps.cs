using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour
{
    [SerializeField] PartCheck[] maps;

    public void GoThroughOnMaps()
    {
        foreach(PartCheck map in maps)
        {
            map.CheckMapLockState();
        }
    }

    
}
