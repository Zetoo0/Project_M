using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fillerexit : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        FindObjectOfType<GameSession>().ResetGameSessionBetweenLevels();
        SceneManager.LoadScene(nextSceneIndex);
    }


}
