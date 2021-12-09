using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadSpeed = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(NextLevel());


        
        
    }

    IEnumerator NextLevel()
    {
        

        yield return new WaitForSecondsRealtime(LevelLoadSpeed);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<Scene_Persist>().ResetScenePersist();

        SceneManager.LoadScene(nextSceneIndex);

    }
}
