using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GetUserExists : MonoBehaviour
{
    string url = "http://s1.siralycore.hu:25502/statisztika";
    string name = "ilyeUgySincsen";

    private void Start()
    {
        StartCoroutine(GetLeaderboard(url));
    }

    public IEnumerator GetLeaderboard(string url)
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
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data); ;
                    result = "{\"result\":" + result + "}";
                   // Debug.Log(result[0]);
                    var resultList = JsonHelper.FromJson<UserLog>(result);

                    foreach(var stat in resultList)
                    {
                        Debug.Log(stat.name);
                    }

                   // Debug.Log(resultList);
                }
                else
                {
                    //If problem, send the error message
                    Debug.Log("Error! Data couldn't get.");
                }
            }

        }
    }

    /*public IEnumerator GetUsername(string url, string name) 
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url))
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
                    var result = www.downloadHandler.text//System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);
                    /*if(result is null)
                    {
                        Debug.Log(false);
                    }
                    else
                    {
                        Debug.Log(true);
                    }*/
                   // result = "{\"result\":" + result + "}";

                    //var resultList = JsonHelper.FromJson<User>(result);

                    //Debug.Log(resultList);
               // }
               // else
               // {
                    //If problem, send the error message
              //      Debug.Log("Error! Data couldn't get.");
              //  }
           // }

       // }
    
}
