using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(DestroyPlatform());
        }
    }
    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSecondsRealtime(2);
        if (gameObject.active)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        
    }


}
