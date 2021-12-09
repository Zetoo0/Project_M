using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int deaths = 0;
    int playerScore = 0;
    [SerializeField] TextMeshProUGUI playerDeathsText;
    [SerializeField] TextMeshProUGUI playerScoreText;
    public Vector2 savePosition;
    //public Rigidbody2D savePoint;
    //public Rigidbody2D player;


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
        playerDeathsText.text = deaths.ToString();
        playerScoreText.text = playerScore.ToString();
        //player = GetComponent<Rigidbody2D>();
        //savePoint = GetComponent<Rigidbody2D>();
        savePosition = new Vector2(5f, 5f);

    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {
        FindObjectOfType<Scene_Persist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    IEnumerator TakeLife()
    {
        yield return new WaitForSecondsRealtime(2);
        //playerLives--;
        deaths++;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        GetComponent<PlayerMovement>().rb.transform.position = savePosition;
        playerDeathsText.text = deaths.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        playerScoreText.text = playerScore.ToString();
    }
}
