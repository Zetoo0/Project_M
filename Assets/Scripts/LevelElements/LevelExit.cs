using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadSpeed = 1f;
    public Animator transition;

    const string CUSTOM_START= "Custom_Start";
    private TimeSpan mapTime;
    private DateTime mapStart;
    public string mapTimeInString;
    [SerializeField] public string postURL;
    



    void Start()
    {
        mapStart = DateTime.Now;
        Debug.Log("Map elej�n kezdem: "+mapStart);
        //transition = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //int playerScore = GetComponent<GameSession>().playerScore;
        MapTime();
        //StartPost();
        NextLevel();
        


    }

    void StartPost()
    {
        var userData = new UserLog()//A post met�dushoz az adatok el�k�sz�t�se
        {
            name = "testIT2",
            point = 250,
            deaths = GetComponent<GameSession>().deaths,
            maptime = mapTimeInString,
            date = DateTime.Now.ToString()
        };

        /*Debug.Log(userData.maptime);
        Debug.Log(userData.name);
        Debug.Log(userData.point);*/

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
        Debug.Log("Next LEvel");
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
        Debug.Log("�j p�lya: " + mapStart);
    }
}
