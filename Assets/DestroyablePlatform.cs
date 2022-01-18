using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{
    bool platformIsNotActive;
    [SerializeField] GameObject DestroyThisPlatform;
    //[SerializeField] GameObject[] platforms;
    [SerializeField] BoxCollider2D CheckerCollider;

    public void Start()
    {
        platformIsNotActive = false;
    }

    public void Update()
    {
        /*if (platformIsNotActive)
        {
            StartCoroutine(ActivatePlatform());
        }*/
    }

    IEnumerator ActivatePlatform()
    {
        yield return new WaitForSecondsRealtime(3);
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
        Debug.Log("Collided");
        if (collision.gameObject.tag == "Player")
        {
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
