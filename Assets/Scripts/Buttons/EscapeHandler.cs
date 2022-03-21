
using UnityEngine;
using UnityEngine.InputSystem;


public class EscapeHandler : MonoBehaviour
{
    [SerializeField]   GameObject userPanelEsc;
    [SerializeField]   GameObject settingsPanelEsc;



    void OnEscape(InputValue value)
    {
        if (value.isPressed)
        {
            if (IsUserPanelEnabled())
            {
                userPanelEsc.active = false;
            }
            else if (IsSettingsPanelEnabled())
            {
                settingsPanelEsc.active = false;
            }
        }
        else
        {
            return;
        }


        

    }

    bool IsUserPanelEnabled()
    {

        bool isEnabled;
        if (userPanelEsc.active == true)
        {
            return isEnabled = true;
        }
        else
        {
            return isEnabled = false;
        }



    }

    bool IsSettingsPanelEnabled()
    {
        bool isEnabled;
        if (settingsPanelEsc.active == true)
        {
            return isEnabled = true;
        }
        else
        {
            return isEnabled = false;
        }

    }

}
