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
    float userMapTime;

    [SerializeField] int levelId;

    [SerializeField] GameObject pauseMenuGO;

    void Start()
    {
        mapStart = DateTime.Now;
        Debug.Log("Map elej�n kezdem: "+mapStart);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        MapTime();
        //SetUserDatasForPost();
        StartPost();
        NextLevel();
        


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

   /* public void SetUserDatasForPost()// User adatok be�ll�t�sa
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

    void StartPost()
    {

        userPoint = FindObjectOfType<GameSession>().playerScore;
        userDeaths = FindObjectOfType<GameSession>().deaths;


        var userData = new UserLog()//A post met�dushoz az adatok el�k�sz�t�se
        {
            name = UserName.username,
            point = userPoint,
            death = userDeaths,
            maptime = mapTimeInString,
            levelId = levelId
        };

       // Debug.Log("Adatok sikeres el�k�sz�t�se");



        StartCoroutine(PostData(postURL, userData));
    }

    public IEnumerator PostData(string url, UserLog userLog)//param�terek ugye az url �s egy olyan opcion�lis param�ter amit testreszabhatunk a saj�t adatainkkal, att�l f�gg mit szeretn�nk k�ldeni
    {
        var jsonData = JsonUtility.ToJson(userLog);//A kapott adatot jsonn� konvert�lja
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))//Unitywebrequest segits�g�vel tudunk postolni
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));//az adat el�k�sz�t�se 

            yield return www.SendWebRequest();//yield return seg�ts�g�vel tov�bb�tjuk az adatot az adatb�zisba



            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }


        }

       // Debug.Log("Sikeres felt�lt�s az adatb�zisba");

        

    }

   /* void CheckLevelNumber()
    {
        if(levelId == 3)
        {
            ClearStaticUserDatas();
        }
    }*/

   /* void ClearStaticUserDatas()
    {
        GameSession.playerScore = 0;
        GameSession.deaths = 0;
        Debug.Log(GameSession.playerScore);
        Debug.Log("Sikeresen tiszt�tva");
    }*/


    void MapTime()//A v�gigvitt map idej�nek kisz�m�t�sa �s elt�rol�sa
    {
        //mapTime = DateTime.Now - mapStart;
        mapTimeInString = userMapTime.ToString();
        Debug.Log("Map Time: " + mapTimeInString);
        //mapStart = DateTime.Now;
       // mapTime = TimeSpan.Zero;
        Debug.Log(mapTime);

    }

    public void NextLevel()//K�vetkez� szintre l�p�s, ha nincs t�bb p�lya akkor visszadob a men�be
    {
        Debug.Log("Next Level");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<GameSession>().ResetGameSessionBetweenLevels();
        SceneManager.LoadScene(nextSceneIndex);
       // Debug.Log("�j p�lya: " + mapStart);
    }
}




































// GetComponent<MapTransition>().ChangeAnimationState(CUSTOM_START);

//  Destroy(pauseMenuGO);
//FindObjectOfType<Scene_Persist>().ResetScenePersist();
