using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;

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
    [SerializeField] string levelName;
    [SerializeField] int saveId;

    [SerializeField] GameObject pauseMenuGO;

    [SerializeField] TextMeshProUGUI exitTimeText;

    private string path = "";
    private string persistentPath = "";

    [SerializeField]IsThisUnlocked _isThisUnlocked;
    

    void Start()
    {
        mapStart = DateTime.Now;
        _isThisUnlocked = GetComponent<IsThisUnlocked>();
        Debug.Log("Map elej�n kezdem: " + mapStart);
    }

    public void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    public void SaveData(UserLog userData)
    {
        string savePath = path;

        Debug.Log("Sikeres ment�s, el�r�si �t: " + savePath);
        string json = JsonUtility.ToJson(userData);
        Debug.Log(json);
        using StreamWriter write = new StreamWriter(savePath);
        write.WriteLine(json);
        write.Flush();
        write.Close();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement.isPlayerCanMove = false;
        //SetPaths();
        // IsThisUnlocked.MapUnlock(levelName, saveId);
        CheckIfMapIsNotUnlocked();
        MapTime();
        //SetUserDatasForPost();
        StartPost();
        StartCoroutine(NextLevel());
    }

    public void SetMapUnlockedIfNotExists()
    {
        int temp;
        if (PlayerPrefs.HasKey(levelName))
        {
            temp = PlayerPrefs.GetInt(levelName);
            if(temp == 1)
            {
                Debug.Log("M�r unlockoltad" + levelName + " " + levelId);
            }
            else
            {
                Debug.Log("idk");
            }
        }
        else
        {
            int saveId = UnityEngine.Random.Range(5, 5000);
            Debug.Log("Unlocked map :): " + levelName + " " + saveId);
            PlayerPrefs.SetInt(levelName, saveId);
            PlayerPrefs.Save();
        }
    }

    public void CheckIfMapIsNotUnlocked()
    {
        foreach(MapData map in MapMapLoading.mapDatas)
        {
            Debug.Log(map.partName);
            if(map.partName == levelName)
            {
                map.IsUnlocked = true;
                Debug.Log("Unlocked: " + map.partName);
                break;
            }
        }
    }


    private void Update()
    {
        userMapTime += Time.deltaTime;
    }

    void StartPost()
    {
    // Debug.Log("Adatok sikeres el�k�sz�t�se");
        userPoint = FindObjectOfType<GameSession>().playerScore;
        userDeaths = FindObjectOfType<GameSession>().deaths;

        var userData = new UserLog(UserName.userID,UserName.username, userPoint, userDeaths, mapTimeInString, levelId, partId);//A post met�dushoz az adatok el�k�sz�t�se
        

       //SaveData(userData);

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

    void MapTime()//A v�gigvitt map idej�nek kisz�m�t�sa �s elt�rol�sa
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

    public IEnumerator NextLevel()//K�vetkez� szintre l�p�s, ha nincs t�bb p�lya akkor visszadob a men�be
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
        // Debug.Log("�j p�lya: " + mapStart);
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
