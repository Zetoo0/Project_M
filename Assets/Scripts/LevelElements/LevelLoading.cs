using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour
{
    public static void LoadMap()
    {
        SceneManager.LoadScene(LevelLoader.chosenMapId);
    }
}
