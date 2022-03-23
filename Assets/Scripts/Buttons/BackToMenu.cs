
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuGO;
    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.Save();
        FindObjectOfType<GameSession>().ResetGameSession();
        Destroy(pauseMenuGO);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
