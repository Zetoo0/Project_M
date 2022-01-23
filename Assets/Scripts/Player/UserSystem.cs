using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;

[System.Serializable]
public class UserSystem : MonoBehaviour
{
    [SerializeField] string getURL;
    StreamWriter sw_users = new StreamWriter("userDatas.txt");

    List<UserData> userData;
    //StreamWriter sw_Users = new StreamWriter("userDatas.txt");

    private void Start()
    {
        StartCoroutine(Get(getURL));
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
                    Debug.Log(result);
                    result = "{\"result\":" + result + "}";

                    var resultList = JsonHelper.FromJson<UserData>(result);

                    foreach(var data in resultList)
                    {
                        sw_users.Write(data);
                    }

                    Debug.Log("Succesfull write! :)");

                    sw_users.Flush();
                    sw_users.Close();

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
