using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    bool platformIsNotActive;
    [SerializeField] GameObject DestroyThisPlatform;
    [SerializeField] BoxCollider2D CheckerCollider;

    public void Start()
    {
        platformIsNotActive = false;
    }

    

    IEnumerator ActivatePlatform()
    {
        yield return new WaitForSecondsRealtime(3);
        platformIsNotActive = false;
        CheckerCollider.enabled = true;
        DestroyThisPlatform.SetActive(true);

        /*for(int i = 0; i <= platforms.Length; i++)
        {
            if (!platforms[i].active)
            {
                yield return new WaitForSecondsRealtime(1);
                platforms[i].SetActive(true);
            }
        }*/
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("Collided");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with the player");
            StartCoroutine(DestroyPlatform());
        }
    }

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSecondsRealtime(3);
        if (!platformIsNotActive)
        {
            platformIsNotActive = true;
            DestroyThisPlatform.SetActive(false);
            CheckerCollider.enabled = false;
            StartCoroutine(ActivatePlatform());
        }

    }

    

    /*IEnumerator DestroyPlatforms()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].active)
            {
                Debug.Log(platforms[i].name);
                yield return new WaitForSecondsRealtime(3);
                platforms[i].SetActive(false);
                StartCoroutine(ActivatePlatform());
                gameObject.SetActive(false);
            }
        }
        
    }*/




}
