using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PotionHolder : MonoBehaviour
{
    public Potion potion;
    public float activeTime;
    public KeyCode key;
    bool isKeyPressed;

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

        if (isKeyPressed)
        {
            HandlePotion();
        }

    }

    void HandlePotion()
    {
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
    }


    void OnPickedUp(InputValue value)
    {
        switch(state)
        {                
            case PotionState.pickedUp:
                //GetComponent<PlayerMovement>().isPickedUp == true
                if (value.isPressed)
                {
                    potion.Activate(gameObject);
                    //activeTime = potion.activeTime;
                    isKeyPressed = true;
                }
                
                break;

        }
    }

   

}
