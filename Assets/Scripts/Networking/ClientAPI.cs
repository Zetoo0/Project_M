using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ClientAPI : MonoBehaviour
{
    public string getUrl = "localhost:3000/user";
    public string postUrl = "localhost:3000/user_create";
    [SerializeField] InputField username;
    [SerializeField] InputField password;
    [SerializeField] InputField birthdate;

    void Start()
    {   
        /*var user = new User()
        {
            id = 1,
            name = "eci",
            password = "uwuwuwuuw",
            birthdate = "2005-05-14"
        };*/

       // StartCoroutine(Post(postUrl, user));Coroutinnal kiküldése a script elindulásakor
        StartCoroutine(Get(getUrl));
    }

    public IEnumerator Get(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    //Handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    //Debug.Log(result);
                    result = "{\"result\":" + result + "}";

                    var resultList = JsonHelper.FromJson<User>(result);

                    Debug.Log(resultList);
                }
                else
                {
                    //If problem, send the error message
                    Debug.Log("Error! Data couldn't get.");
                }
            }

        }
    }

    public IEnumerator Post(string url, User user)//az elsõ az értendõen a megkapni kívánt url a másodiknak pedig egy classnak kell lennie mint pl User user és azon belül ugye létre kell hozni adatokat
    {
        var jsonData = JsonUtility.ToJson(user);//a paraméterezett adat jsonné alakítása
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))//Unitywebrequest létrehozása amivel postolunk
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));//a küldés elõkészítése

            yield return www.SendWebRequest();//yield returnnel az adat küldése az adatbázisba 
            //Ezek már csak mellékes doolgok, ellenörzésre nagyrészt
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    //Handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    result = "{\"result\":" + result + "}";

                    var resultUserList = JsonHelper.FromJson<User>(result);

                    foreach (var data in resultUserList)
                    {
                        Debug.Log(data.name);
                    }


                }
                else
                {
                    Debug.Log("Error! Data couldn't get");
                }
            }


        }
    }
}
