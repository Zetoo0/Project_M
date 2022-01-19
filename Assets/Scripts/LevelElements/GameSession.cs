using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameSession : MonoBehaviour
{
    public int deaths = 0;
    public int playerScore = 0;
    [SerializeField] TextMeshProUGUI playerDeathsText;
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI playerDeathText;

    void Awake()
    {
        
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(player);
            //DontDestroyOnLoad(savePoint);
        }
    }

     void Start()
    {
        playerScore = 0;
        playerDeathsText.text = deaths.ToString();
        playerScoreText.text = playerScore.ToString();

    }

    public void ProcessPlayerDeath()
    {
        /*if(playerLives > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            ResetGameSession();
        }*/
        DeadTextOut();
        StartCoroutine(TakeLife());
    }

    void DeadTextOut()
    {
        playerDeathText.gameObject.SetActive(true);
    }

    void ResetGameSession()
    {
        FindObjectOfType<Scene_Persist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    IEnumerator TakeLife()
    {

        yield return new WaitForSecondsRealtime(1);
        //playerLives--;
        deaths++;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        //GetComponent<PlayerMovement>().rb.transform.position = savePosition;
        playerDeathsText.text = deaths.ToString();
        //dead.SetActive(false);
        playerDeathText.gameObject.SetActive(false);
    }

    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        playerScoreText.text = playerScore.ToString();
    }
}
