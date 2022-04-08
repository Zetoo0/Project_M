using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
public class Register : MonoBehaviour
{
    [SerializeField] string regUrl;
    [SerializeField] TMP_InputField name;
    [SerializeField] TMP_InputField password;
    string uriR = "localhost:3000/register";
    string uName;
    string uPassword;

    [SerializeField]TMP_Text messageText;

   

    public void ShowMessage(string message)
    {
        messageText.SetText(message);
    }

    public void SetStartToRegister()
    {
        LogRegUser regUser = new LogRegUser(name.text, password.text);

        StartCoroutine(RegisterCheck(regUrl, regUser));
    }

    public IEnumerator RegisterCheck(string url, LogRegUser user)
    {
        var jsonData = JsonUtility.ToJson(user);//A kapott adatot jsonné konvertálja
        //Debug.Log(jsonData);

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
            else
            {
                if (www.isDone)
                {
                    var result = www.downloadHandler.text;//System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if (result.Contains("happy"))
                    {
                        ShowMessage("Sikeres regisztráció!");
                        StartCoroutine(RegisterUser(uriR, user));
                    }
                    else
                    {
                        ShowMessage("A felhasználónév már létezik!");
                        Debug.Log(result);
                    }


                    //   Debug.Log(result);


                }
            }
        }
    }

    public IEnumerator RegisterUser(string url, LogRegUser user)
    {
        var jsonData = JsonUtility.ToJson(user);//A kapott adatot jsonné konvertálja
        //Debug.Log(jsonData);

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
            else
            {
                if (www.isDone)
                {
                    var result = www.downloadHandler.text;//System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                   // Debug.Log(result);


                }
            }
        }
    }
}
