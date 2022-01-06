using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;  

public class Pause : MonoBehaviour
{

    public static bool  gameIsPaused = false;
    [SerializeField] GameObject pauseMenu;  
    GameState gameState;
    TextMeshProUGUI text;
    [SerializeField]PlayerInput playerInput;

    void Start()
    {
        pauseMenu.SetActive(false);
    }


    void OnPause(InputValue value)
    {


        if (value.isPressed)
        {
            if (!gameIsPaused)
            {
                PauseTheGame();
            }
            else
            {
                Resume();
            }

        }


    }




    void Resume()
    {
        pauseMenu.SetActive(false);
        gameState = GameState.Gameplay;
        Debug.Log("Resume");
        Time.timeScale = 1f;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        gameIsPaused = false;
    }

    void PauseTheGame()
    {
        pauseMenu.SetActive(true);
        //SceneManager.LoadScene(0);
        gameState = GameState.Paused;
        Debug.Log("Pause");
        Time.timeScale = 0f;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        gameIsPaused = true;
    }



   

   

}
