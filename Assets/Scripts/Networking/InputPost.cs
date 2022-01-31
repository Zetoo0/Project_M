using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputPost : MonoBehaviour
{
    [SerializeField] public string postURL;//A backend küldésére szolgáló url

   

    public void StartPost()
    {
        var userData = new UserLog()//A post metódushoz az adatok előkészítése
        {
            name = "testname",
            point = 666
        };

        StartCoroutine(PostData(postURL, userData));//Coroutine segítségével indítjuk a metódust
    }

    public IEnumerator PostData(string url, UserLog userLog)//paraméterek ugye az url és egy olyan opcionális paraméter amit testreszabhatunk a saját adatainkkal, attól függ mit szeretnénk küldeni
    {
        var jsonData = JsonUtility.ToJson(userLog);//A kapott adatot jsonné konvertálja
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))//Unitywebrequest segitségével tudunk postolni
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));//az adat előkészítése 

            yield return www.SendWebRequest();//yield return segítségével továbbítjuk az adatot az adatbázisba

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }


        }
    }
}
