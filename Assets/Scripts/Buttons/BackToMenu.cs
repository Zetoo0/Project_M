using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuGO;
    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<GameSession>().ResetGameSession();
        Destroy(pauseMenuGO);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
