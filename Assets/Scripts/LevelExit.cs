using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadSpeed = 1f;
    public Animator transition;

    const string CROSSFADE_START = "Crossfade_Start";
    TimeSpan mapTime;
    DateTime mapStart;

    private void Awake()
    {
        
    }

    void Start()
    {
        mapStart = DateTime.Now;
        Debug.Log("Map elején kezdem: "+mapStart);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        MapTime();
        StartCoroutine(NextLevel());
        


    }

    void MapTime()
    {
        mapTime = DateTime.Now - mapStart;
        Debug.Log("Map Time: " + mapTime);
        mapStart = DateTime.Now;
        mapTime = TimeSpan.Zero;
        Debug.Log(mapTime);
    }

    public IEnumerator NextLevel()
    {
        //GetComponent<PlayerMovement>().ChangeAnimationState("Player_NextLevel");
        //transition.Play(CROSSFADE_START);
        yield return new WaitForSecondsRealtime(LevelLoadSpeed);
        transition.SetTrigger("Start");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<Scene_Persist>().ResetScenePersist();

        SceneManager.LoadScene(nextSceneIndex);
        
        Debug.Log("Új pálya: " + mapStart);
    }
}
