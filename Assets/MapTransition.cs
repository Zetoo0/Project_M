using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    public Animator transition;

    //ANIMATION STATES
    string CUSTOM_END = "Custom End";


    void Start()
    {
        transition = GetComponent<Animator>();

    }

    public void ChangeAnimationState(string newState)
    {
        transition.Play(newState);
       

        

       
    }



}
