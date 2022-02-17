using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeHandler : MonoBehaviour
{
    [SerializeField]   GameObject userPanelEsc;
    [SerializeField]   GameObject settingsPanelEsc;

    void Update()
    {
        ExitFromActive();    
    }

    void ExitFromActive()
    {
        if ()
        {
            if (IsUserPanelEnabled())
            {
                userPanelEsc.SetActive(false);
            }
            else if (IsSettingsPanelEnabled())
            {
                settingsPanelEsc.SetActive(false);
            }
        }

    }

    bool IsUserPanelEnabled()
    {


        bool isEnabled = userPanelEsc.activeInHierarchy;

        return isEnabled;
    }

    bool IsSettingsPanelEnabled()
    {
        bool isEnabled = settingsPanelEsc.activeInHierarchy;

        return isEnabled;

    }

}
