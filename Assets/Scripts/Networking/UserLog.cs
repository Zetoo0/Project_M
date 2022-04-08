using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserLog//User datas
{
    public int userID;
    public string name;
    public int point;
    public int death;
    public string maptime;
    public int levelId;
    public int partId;

    public UserLog(int userId,string name, int point, int death, string maptime, int levelId, int partId)
    {
        this.userID = userId;
        this.name = name;
        this.point = point;
        this.death = death;
        this.maptime = maptime;
        this.levelId = levelId;
        this.partId = partId;
    }

    

}



