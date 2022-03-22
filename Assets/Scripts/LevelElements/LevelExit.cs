using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

using TMPro;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadSpeed = 1f;
    public Animator transition;

    const string CUSTOM_START = "Custom_Start";
    private TimeSpan mapTime;
    private DateTime mapStart;
    public string mapTimeInString;
    [SerializeField] public string postURL;
    List<UserLog> userDataList = new List<UserLog>();

    public string userName;
    int userPoint;
    int userDeaths;
    float userMapTime;

    [SerializeField] int levelId;
    [SerializeField] int partId;

    [SerializeField] GameObject pauseMenuGO;

    [SerializeField] TextMeshProUGUI exitTimeText;

    private string path = "";
    private string persistentPath = "";

    void Start()
    {
        mapStart = DateTime.Now;
        Debug.Log("Map elején kezdem: " + mapStart);
    }

    public void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    public void SaveData(UserLog userData)
    {
        string savePath = path;

        Debug.Log("Sikeres mentés, elérési út: " + savePath);
        string json = JsonUtility.ToJson(userData);
        Debug.Log(json);
        using StreamWriter write = new StreamWriter(savePath);
        write.Write(json);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement.isPlayerCanMove = false;
        SetPaths();
        MapTime();
        //SetUserDatasForPost();
        StartPost();
        StartCoroutine(NextLevel());
        


    }


    private void Update()
    {
        userMapTime += Time.deltaTime;
    }

    void StartPost()
    {
    // Debug.Log("Adatok sikeres elõkészítése");
        userPoint = FindObjectOfType<GameSession>().playerScore;
        userDeaths = FindObjectOfType<GameSession>().deaths;

        var userData = new UserLog(UserName.username, userPoint, userDeaths, mapTimeInString, levelId, partId);//A post metódushoz az adatok elõkészítése
        

       //SaveData(userData);

        StartCoroutine(PostData(postURL, userData));
    }

    public IEnumerator PostData(string url, UserLog userLog)//paraméterek ugye az url és egy olyan opcionális paraméter amit testreszabhatunk a saját adatainkkal, attól függ mit szeretnénk küldeni
    {
        var jsonData = JsonUtility.ToJson(userLog);//A kapott adatot jsonné konvertálja
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))//Unitywebrequest segitségével tudunk postolni
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));//az adat elõkészítése 

            yield return www.SendWebRequest();//yield return segítségével továbbítjuk az adatot az adatbázisba



            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }


        }

       // Debug.Log("Sikeres feltöltés az adatbázisba");

        

    }

    void MapTime()//A végigvitt map idejének kiszámítása és eltárolása
    {
        //mapTime = DateTime.Now - mapStart;
        mapTimeInString = userMapTime.ToString();
        ExitTimeTextOut(mapTimeInString);
        userMapTime = 0;
        Debug.Log("Map Time: " + mapTimeInString);
        //mapStart = DateTime.Now;
       // mapTime = TimeSpan.Zero;
        Debug.Log(mapTime);

    }

    public IEnumerator NextLevel()//Következõ szintre lépés, ha nincs több pálya akkor visszadob a menübe
    {
        yield return new WaitForSecondsRealtime(3);
        Debug.Log("Next Level");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        exitTimeText.gameObject.SetActive(false);
        FindObjectOfType<GameSession>().ResetGameSessionBetweenLevels();
        SceneManager.LoadScene(nextSceneIndex);
        // Debug.Log("Új pálya: " + mapStart);
    }

    void ExitTimeTextOut(string time)
    {
        exitTimeText.gameObject.SetActive(true);
        exitTimeText.text = "MAP TIME: \n\n" + time;
    }


}




































// GetComponent<MapTransition>().ChangeAnimationState(CUSTOM_START);

//  Destroy(pauseMenuGO);
//FindObjectOfType<Scene_Persist>().ResetScenePersist();
