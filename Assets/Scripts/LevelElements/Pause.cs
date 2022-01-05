using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class Pause : MonoBehaviour
{
    public static bool  gameIsPaused = false;
    [SerializeField] GameObject pauseMenu;
    GameState gameState;
    
    void OnPause(InputValue value)
    {


        if (value.isPressed)
        {
            if (gameIsPaused)
            {
                Resume();
            }   
            else
            {
                PauseTheGame();
            }
        }



    }


    void Resume()
    {
        pauseMenu.SetActive(false);
        gameState = GameState.Gameplay;
        Debug.Log("Resume");
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void PauseTheGame()
    {
        pauseMenu.SetActive(true);
        gameState = GameState.Paused;
        Debug.Log("Pause");
        Time.timeScale = 0f;
        gameIsPaused = true;
    }



    static bool GameIsPaused(bool value)
    {
        bool gameIsPaused;
        if (value)
        {
            gameIsPaused = true;
        }
        else
        {
            gameIsPaused = false;
        }

        return gameIsPaused;


    }

    void Start()
    {
        pauseMenu.SetActive(false);
    }


}
