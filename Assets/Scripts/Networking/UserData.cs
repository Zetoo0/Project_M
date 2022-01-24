using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }

    public UserData ConvertUserDataFromString(UserData stringData)
    {
        UserData userData = new UserData();
        userData.UserEmail = stringData.UserEmail;
        userData.UserName = stringData.UserName;
        userData.UserPassword = stringData.UserPassword;
        return userData;
    }

}



