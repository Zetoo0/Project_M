    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    bool platformIsNotActive;
    [SerializeField] GameObject DestroyThisPlatform;
    [SerializeField] BoxCollider2D CheckerCollider;
    [SerializeField] float destroyTime;
    [SerializeField] float activateTime;

    public void Start()
    {
        platformIsNotActive = false;
    }

    IEnumerator ActivatePlatform()
    {
        yield return new WaitForSecondsRealtime(activateTime);
        platformIsNotActive = false;
        CheckerCollider.enabled = true;
        DestroyThisPlatform.SetActive(true);
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
        yield return new WaitForSecondsRealtime(destroyTime);
        if (!platformIsNotActive)
        {
            platformIsNotActive = true;
            DestroyThisPlatform.SetActive(false);
            CheckerCollider.enabled = false;
            StartCoroutine(ActivatePlatform());
        }

    }
       
}
