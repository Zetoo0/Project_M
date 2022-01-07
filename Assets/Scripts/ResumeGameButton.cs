using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class ResumeGameButton : MonoBehaviour
{
    GameObject pauseMenu;


    public void ResumeGame()
    {
        GetComponent<Pause>().Resume();
    }
}
