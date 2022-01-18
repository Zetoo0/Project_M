using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{
    float time;

     void Start()
    {
        time = 0;
    }



    private void Update()
    {
        Debug.Log(time);
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (gameObject.active && collision.gameObject.tag == "Player")
        {
            StartCoroutine(DestroyPlatform());
        }
        if(!gameObject.activeSelf)
        {
            Invoke("ActivatePlatform", 5);
        }
    }

    void ActivatePlatform()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }


    //private void Update()
    //{
    //;


    //}

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSecondsRealtime(3);
        if (gameObject.active)
        {
            gameObject.SetActive(false);
        }
        
    }




}
