
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject userPanel;
    int currentSceneIndex;
    int nextSceneIndex;

    public void usernamePanel()
    {
        OpenExitButton.Open(userPanel);
    }

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
    }

    public void FirstLevel()
    {
        SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Single);

    }

    public void SetUserPanelInactive()
    {
        OpenExitButton.Exit(userPanel);
    }
}
