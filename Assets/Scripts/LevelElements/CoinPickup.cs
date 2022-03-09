using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int pointsForPickUp = 125;

    [SerializeField] bool wasCollected = false;
    Animator anim;
    string currentState;

    [SerializeField] AudioMixerGroup auMixer;
    
    string exposedName = "volume";

    //ANIMATIONS
    const string COIN_PICKUP = "coin_pickup";

   

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    float GetVolume()
    {
        float volOut;
        bool isCanGetVol = auMixer.audioMixer.GetFloat(exposedName, out volOut); //auMixer.GetFloat(exposedName, out volOut);
        if (isCanGetVol)
        {
            return volOut;
        }
        else
        {
            return 0f;
        }

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
            gameObject.SetActive(false);
            
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
