using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretMap : MonoBehaviour
{
    public bool allCollectibleColorIsPickedUpOnTheCurrentMap = false;


    void Update()
    {
        CheckCollectibles();
    }

    void CheckCollectibles()
    {
        if (allCollectibleColorIsPickedUpOnTheCurrentMap)
        {
            gameObject.SetActive(true);
        }
    }
}
