using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PotionHolder : MonoBehaviour
{
    public Potion potion;
    public float activeTime;
    //public KeyCode key;

    public enum PotionState
    {
        pickedUp,
        notPickedUp
    }

    public PotionState state = PotionState.notPickedUp;

     void Start()
    {
        activeTime = potion.activeTime;
    }


    void Update()
    {
        /*
        if(state == PotionState.pickedUp)
        {
            potion.Activate(gameObject);
            if(activeTime > 0)
            {
                StartCoroutine(CountDown());    
            }
            if(activeTime == 0)
            {
                state = PotionState.notPickedUp;
                Debug.Log("Inactive");
                GetComponent<PlayerMovement>().isPickedUp = false;
                GetComponent<PlayerMovement>().jumpSpeed = 15;    
            }
        }*/

        switch (state)
        {                
            case PotionState.pickedUp:
                //GetComponent<PlayerMovement>().isPickedUp == true
                //if (Input.GetKeyDown(key))
                //{
                potion.Activate(gameObject);
                //activeTime = potion.activeTime;
                //}
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                    Debug.Log(activeTime);
                }
                else
                {
                    state = PotionState.notPickedUp;
                    Debug.Log("Inactive");
                    GetComponent<PlayerMovement>().isItemPickedUp = false;
                    GetComponent<PlayerMovement>().jumpSpeed = 15;    

                }
                break;

        }
    }

    /*IEnumerator CountDown()
    {
        yield return new WaitForSecondsRealtime(1);
        activeTime--;
    }*/

}
