using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

using TMPro;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadSpeed = 1f;
    public Animator transition;
    
    const string CUSTOM_START= "Custom_Start";
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

    [SerializeField] GameObject pauseMenuGO;

    [SerializeField] TextMeshProUGUI exitTimeText;

    void Start()
    {
        mapStart = DateTime.Now;
        Debug.Log("Map elején kezdem: "+mapStart);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement.isPlayerCanMove = false;
        MapTime();
        //SetUserDatasForPost();
        StartPost();
        StartCoroutine(NextLevel());
        


    }

   /* public void WriteDataToFile(UserLog data)
    {
        string path = "UserDatas.csv";
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter sw_out = new StreamWriter(fs);
        sw_out.WriteLine("UserName;Point;Death;Maptime");
        sw_out.Write(data.name);
        sw_out.Write(";");
        sw_out.Write(data.point);
        sw_out.Write(";");
        sw_out.Write(data.death);
        sw_out.Write(";");
        sw_out.Write(data.maptime);
        sw_out.Write("\n");
    }*/

   /* public void SetUserDatasForPost()// User adatok beállítása
    {
        userName = UserName.username;
        userPoint = GetComponent<GameSession>().playerScore;
        userDeaths = GetComponent<GameSession>().deaths;
        userMapTime = mapTimeInString;
    }*/


    private void Update()
    {
        userMapTime += Time.deltaTime;
    }

    UserLog CreatePlayerData()
    {
        userPoint = FindObjectOfType<GameSession>().playerScore;
        userDeaths = FindObjectOfType<GameSession>().deaths;


        var userData = new UserLog()//A post metódushoz az adatok elõkészítése
        {
            name = UserName.username,
            point = userPoint,
            death = userDeaths,
            maptime = mapTimeInString,
            levelId = levelId
        };

        return userData;
    }

    void StartPost()
    {
       // Debug.Log("Adatok sikeres elõkészítése");

        StartCoroutine(PostData(postURL, CreatePlayerData()));
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
