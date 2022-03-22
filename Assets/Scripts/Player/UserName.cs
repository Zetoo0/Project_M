using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UserName : MonoBehaviour
{
    public static string username;
    PlayerPrefs playerUserName;
    [SerializeField]TMP_InputField usernameInputField;

    public void SetUserName()
    {
        username = usernameInputField.text;
        Debug.Log(username);
        SceneManager.LoadScene(LevelLoader.chosenMapId);
    }   
}
