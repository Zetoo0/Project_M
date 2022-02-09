using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

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
    string userMapTime; 

    [SerializeField] int levelId;

    void Start()
    {
        mapStart = DateTime.Now;
        Debug.Log("Map elején kezdem: "+mapStart);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        MapTime();
        SetUserDatasForPost();
        StartPost();
        NextLevel();
        


    }

    public void WriteDataToFile(UserLog data)
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
    }

    void SetUserDatasForPost()// User adatok beállítása
    {
        userName = UserName.username;
        userPoint = GameSession.playerScore;
        userDeaths = GameSession.deaths;
        userMapTime = mapTimeInString;
    }

    

    void StartPost()
    {

        
        var userData = new UserLog()//A post metódushoz az adatok elõkészítése
        {
            name = userName,
            point = userPoint,
            death = userDeaths,
            maptime = userMapTime,
            levelId = levelId
        };

        //WriteDataToFile(userData);
        

      

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

        CheckLevelNumber();

        

    }

    void CheckLevelNumber()
    {
        if(levelId == 3)
        {
            ClearStaticUserDatas();
        }
    }

    void ClearStaticUserDatas()
    {
        GameSession.playerScore = 0;
        GameSession.deaths = 0;
        Debug.Log(GameSession.playerScore);
        Debug.Log("Sikeresen tisztítva");
    }


    void MapTime()//A végigvitt map idejének kiszámítása és eltárolása
    {
        mapTime = DateTime.Now - mapStart;
        mapTimeInString = mapTime.TotalSeconds.ToString();
        Debug.Log("Map Time: " + mapTimeInString);
        mapStart = DateTime.Now;
        mapTime = TimeSpan.Zero;
        Debug.Log(mapTime);

    }

    public void NextLevel()//Következõ szintre lépés, ha nincs több pálya akkor visszadob a menübe
    {
        Debug.Log("Next Level");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<Scene_Persist>().ResetScenePersist();

        SceneManager.LoadScene(nextSceneIndex);
        GetComponent<MapTransition>().ChangeAnimationState(CUSTOM_START);
        Debug.Log("Új pálya: " + mapStart);
    }
}
