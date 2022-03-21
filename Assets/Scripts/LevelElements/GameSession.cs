using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameSession : MonoBehaviour
{
    public int deaths = 0;
    public int playerScore = 0;
    public int pickedUpCollectible = 0;
    [SerializeField] TextMeshProUGUI playerDeathsText;
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI playerDeathText;

    //Collectible colors
    const string blueCollectible = "BlueCollectible";
    bool blueCollectibleIsPickedUp = false;
    const string yellowCollectible = "YellowCollectible";
    bool yellowCollectibleIsPickedUp = false;
    const string redCollectible = "RedCollectible";
    bool redCollectibleIsPickedUp = false;

    private string CUSTOM_START = "Custom_Start";


    void Awake()
    {
        
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            playerScore = 0;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(player);
            //DontDestroyOnLoad(savePoint);
        }
    }

    void Start()
    {
        //deaths = 0;
        // playerScore = 0;
        SetTextsAtStart();

    }

    void SetTextsAtStart()
    {
        playerDeathsText.text = deaths.ToString();
        playerScoreText.text = playerScore.ToString();
    }

     void Update()
    {
        CheckCollectibles();
    }

    public void ResetGameSessionBetweenLevels()
    {
        Destroy(gameObject);
    }


    public void ProcessPlayerDeath()
    {
        DeadTextOut();
        StartCoroutine(TakeLife());
    }

    void CheckCollectibles()
    {
        if(blueCollectibleIsPickedUp && yellowCollectibleIsPickedUp && redCollectibleIsPickedUp)
        {
            GetComponent<SecretMap>().allCollectibleColorIsPickedUpOnTheCurrentMap = true;
        }
    }

    void DeadTextOut()
    {
        playerDeathText.gameObject.SetActive(true);
    }

    public void ResetGameSession()
    {
        FindObjectOfType<Scene_Persist>().ResetScenePersist();
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    IEnumerator TakeLife()//Respawn/Újraéledés
    {

        yield return new WaitForSecondsRealtime(1);
        playerScore = 0;
        playerScoreText.text = "0";
        deaths++;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        playerDeathsText.text = deaths.ToString();
        playerDeathText.gameObject.SetActive(false);
        GetComponent<MapTransition>().ChangeAnimationState(CUSTOM_START);
       // ResetGameSessionBetweenLevels();

    }

    public void AddToScore(int pointsToAdd)//Ponthoz hozzáadás
    {
        playerScore += pointsToAdd;
        playerScoreText.text = playerScore.ToString();
    }


    public void AddToCollectible(string color)
    {
        pickedUpCollectible++;
        switch (color)
        {
            case blueCollectible:
                blueCollectibleIsPickedUp = true;
                break;
            case yellowCollectible:
                yellowCollectibleIsPickedUp = true;
                break;
            case redCollectible:
                redCollectibleIsPickedUp = true;
                break;
        }
    }
}
