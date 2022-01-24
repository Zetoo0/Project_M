using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class UserSystem : MonoBehaviour
{
    [SerializeField] string getURL;
    StreamWriter sw_users = new StreamWriter("userDatas.txt");

    //List<UserData> userData;
    //StreamWriter sw_Users = new StreamWriter("userDatas.txt");

    [SerializeField] TextMeshProUGUI playerUserNameText;


    public string userName = "";

    private void Start()
    {
        StartCoroutine(Get(getURL));
        //Debug.Log(userData);
    }

    public void SetUserName()
    {
        userName = playerUserNameText.text.ToString();
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
                    result = "{ " + result + "}";
                    var resultRoot = JsonHelper.FromJson<UserData>(result);
                    /*for(int i = 0; i <= resultList.Count; i++)
                    {
                        userData.Add(resultList[i]);
                    }*/
                }
                else
                {
                    //If problem, send the error message
                    Debug.Log("Error! Data couldn't get.");
                }
            }

        }
    }

}
