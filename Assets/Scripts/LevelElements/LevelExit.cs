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


    void Start()
    {
        mapStart = DateTime.Now;
        Debug.Log("Map elején kezdem: "+mapStart);
        //transition = GetComponent<Animator>();
       // StartPost();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //int playerScore = GetComponent<GameSession>().playerScore;
        MapTime();
        SetUserDatasForPost();
        StartPost();
        //NextLevel();
        


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

    void SetUserDatasForPost()
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
            name = userName,//GetComponent<UserName>().username,
            point = userPoint,
            death = userDeaths,
            maptime = userMapTime
        };

        WriteDataToFile(userData);
        

        /*Debug.Log(userData.maptime);
        Debug.Log(userData.name);
        Debug.Log(userData.point);*/

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
    }


    void MapTime()
    {
        mapTime = DateTime.Now - mapStart;
        mapTimeInString = mapTime.TotalSeconds.ToString();
        Debug.Log("Map Time: " + mapTimeInString);
        mapStart = DateTime.Now;
        mapTime = TimeSpan.Zero;
        Debug.Log(mapTime);

    }

    public void NextLevel()
    {
        //GetComponent<PlayerMovement>().ChangeAnimationState("Player_NextLevel");
        //transition.Play(CROSSFADE_START);
        //transition.Play(CUSTOM_END);
        Debug.Log("Next Level");
        //yield return new WaitForSecondsRealtime(LevelLoadSpeed);
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
