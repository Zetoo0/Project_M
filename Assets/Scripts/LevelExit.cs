using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadSpeed = 1f;
    public Animator transition;

    const string CROSSFADE_START = "Crossfade_Start";

  

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(NextLevel());


        
        
    }

    public IEnumerator NextLevel()
    {

        //transition.Play(CROSSFADE_START);
        transition.SetTrigger("Start");
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
