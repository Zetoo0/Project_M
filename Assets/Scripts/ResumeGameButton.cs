using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResumeGameButton : MonoBehaviour
{
    GameObject pauseMenu;


    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;    
    }
}
