
using UnityEngine;
using UnityEngine.InputSystem;

public class NavigateInMenu : MonoBehaviour
{

    Vector2 keyboardNavigate;
   
    void OnNavigate(InputValue value)
    {
        keyboardNavigate = value.Get<Vector2>();
    }


}
