
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;  

public class Pause : MonoBehaviour
{

    public bool  gameIsPaused = false;  
    [SerializeField] GameObject pauseMenu;  
    static public GameState gameState;
    TextMeshProUGUI text;

    void Start()
    {
        gameState = GameState.Gameplay;
        pauseMenu.SetActive(false);
        //DontDestroyOnLoad(pauseMenu);
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




    public void Resume()
    {
        OpenExitButton.Exit(pauseMenu);
        gameState = GameState.Gameplay;
        Debug.Log("Resume");
        Time.timeScale = 1f;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        gameIsPaused = false;
    }

    void PauseTheGame()
    {
        OpenExitButton.Open(pauseMenu);
        
        //SceneManager.LoadScene(0);
        gameState = GameState.Paused;
        Debug.Log("Pause");
        Time.timeScale = 0f;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        gameIsPaused = true;
    }



   

   

}
