using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int pointsForPickUp = 125;

    [SerializeField] bool wasCollected = false;
    Animator anim;
    string currentState;

    //ANIMATIONS
    const string COIN_PICKUP = "coin_pickup";

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wasCollected)
        {
            //anim.SetTrigger("PickedUp");
            ChangeAnimationState(COIN_PICKUP);
            //gameObject.transform.position = coinPickupUp;
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForPickUp);
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
            
        }
    }

    void ChangeAnimationState(string newState)
    {

        //hogyha az aktu�lis anim�ci� = a param�terrel akkor return�li
        if (currentState == newState)
        {
            return;
        }

        //anim�ci� lej�tsz�sa
        anim.Play(newState);

        //�t�rjuk az �jra a mostani �llapotunkat
        currentState = newState;
    }

}
