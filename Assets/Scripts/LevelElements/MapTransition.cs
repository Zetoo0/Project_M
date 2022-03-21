
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    public Animator transition;

    //ANIMATION STATES
    string CUSTOM_END = "Custom End";


    void Start()
    {
        transition = GetComponent<Animator>();
        ChangeAnimationState(CUSTOM_END);
    }

    public void ChangeAnimationState(string newState)
    {
        transition.Play(newState);

    }



}
