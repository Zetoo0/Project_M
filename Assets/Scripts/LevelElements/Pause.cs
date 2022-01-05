using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public static bool  gameIsPaused = false;


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
        Debug.Log("Resum");
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void PauseTheGame()
    {
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



}
