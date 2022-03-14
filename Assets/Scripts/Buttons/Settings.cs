using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
   [SerializeField]GameObject settingsPanel;
    bool isSettingsOn;

    public void SettingsOn()
    {
        OpenExitButton.Open(settingsPanel);
        isSettingsOn = true;
    }

    public void SettingsOff()
    {
        if (isSettingsOn)
        {
            isSettingsOn = false;
            OpenExitButton.Exit(settingsPanel);
            
        }
    }
}