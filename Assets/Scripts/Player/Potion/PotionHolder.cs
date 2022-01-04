using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHolder : MonoBehaviour
{
    public Potion potion;
    float activeTime;
    //public KeyCode key;

    public enum PotionState
    {
        pickedUp,
        //active,
        notPickedUp
    }

    public PotionState state = PotionState.notPickedUp;

    void Update()
    {
        switch (state)
        {                
            case PotionState.pickedUp:
                //GetComponent<PlayerMovement>().isPickedUp == true
                //if (Input.GetKeyDown(key))
                //{
                potion.Activate(gameObject);
                activeTime = potion.activeTime;
                //}
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                    Debug.Log(activeTime);
                }
                else
                {
                    state = PotionState.notPickedUp;
                }
                break;

        }
    }
}
