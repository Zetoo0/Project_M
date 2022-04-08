using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class UserName : MonoBehaviour
{
    public static string username;
    public static int userID;
    PlayerPrefs playerUserName;
    [SerializeField]TMP_InputField usernameInputField;

    public static void SetUserNameAndUserId(string userName, int userId)
    {
        username = userName;
        userID = userId;
        Debug.Log(userID);
        //SceneManager.LoadScene(LevelLoader.chosenMapId);
    }   
}
