using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Persist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersist = FindObjectsOfType<Scene_Persist>().Length;
        if (numScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);        
    }

}
