using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserName : MonoBehaviour
{
    public string username;
    [SerializeField]TMP_InputField usernameInputField;

    public void SetUserName()
    {
        username = usernameInputField.text;
        Debug.Log(username);
    }
}
