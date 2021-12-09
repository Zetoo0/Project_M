using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHolder : MonoBehaviour
{
    public Potion potion;
    float activeTime;
    public KeyCode key;

    enum PotionState
    {
        pickedUp,
        active,
        notPickedUp
    }

    PotionState state = PotionState.notPickedUp;

    void Update()
    {
        switch (state)
        {                
            case PotionState.pickedUp:
                //GetComponent<PlayerMovement>().isPickedUp == true
                if (Input.GetKeyDown(key))
                {
                    potion.Activate(gameObject);
                    state = PotionState.active;
                    activeTime = potion.activeTime;
                }
                break;
            case PotionState.active:
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = PotionState.notPickedUp;
                }
                break;

        }
    }
}
