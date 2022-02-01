using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject userPanel;
    public void usernamePanel()
    {
        userPanel.SetActive(true);
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
