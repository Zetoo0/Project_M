using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapMapLoading : MonoBehaviour
{
     string path = "";
    public static string persistentPath = "";
    public static List<MapData> mapDatas;

    int lockedMapNum = 2;

    public void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData" + Path.AltDirectorySeparatorChar + "SaveData.txt";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.txt";
    }

    void Start()
    {
        mapDatas = new List<MapData>();
        SetPaths();
        LoadMapDatas();
       // SaveMapDatas();
    }

    public static void SaveMapDatas()
    {
        /*foreach(var data in mapDatas)
        {
            Debug.Log(data);
            string json = JsonUtility.ToJson(data);
            Debug.Log(json);
            write.Write(json);
        }*/


        StreamWriter write = new StreamWriter(persistentPath);
        
            
                foreach (MapData data in mapDatas)
                {
                    if (data.IsUnlocked)
                    {
                        write.WriteLine(data.partName + ";1");
                        Debug.Log("Írtam: 1");
                    }
                    else
                    {
                        write.WriteLine(data.partName + ";0");
                        Debug.Log("Írtam: 0");
                    }
                }
            
        

        write.Flush();
        write.Close();
       
    }

    
    public void LoadIfFileNotExists()
    {
        Debug.Log(File.Exists(persistentPath));
        List<MapData> firstStartMapLoadingForUser = new List<MapData>();
        StreamWriter sw_ki = new StreamWriter(persistentPath);
        MapData SecFirst = new MapData();
        SecFirst.partName = "part2_1";
        SecFirst.IsUnlocked = false;
        firstStartMapLoadingForUser.Add(SecFirst);
        MapData ThirdFirst = new MapData();
        ThirdFirst.partName = "part3_1";
        ThirdFirst.IsUnlocked = false;
        firstStartMapLoadingForUser.Add(ThirdFirst);
        foreach (MapData map in firstStartMapLoadingForUser)
        {
            string mapname = map.partName;
            sw_ki.WriteLine(mapname + ";0");
        }
        sw_ki.Flush();
        sw_ki.Close();

        LoadFile();

        /*string[] fileA = File.ReadAllLines(persistentPath);

        for (int i = 1; i <= fileA.Length; i++)
        {
            string[] splitA = fileA[i].Split(';');
            MapData ujMapData = new MapData();
            ujMapData.partName = splitA[0];
            if (splitA[1] == "1")
            {
                ujMapData.IsUnlocked = true;
            }
            else
            {
                ujMapData.IsUnlocked = false;
            }
            mapDatas.Add(ujMapData);
        }



        foreach (MapData data in mapDatas)
        {
            Debug.Log(data.partName);
            Debug.Log(data.IsUnlocked);
        }*/

    }

    

    public void LoadFile()
    {
        string[] fileA = File.ReadAllLines(persistentPath);

        for (int i = 0; i < fileA.Length; i++)
        {
            string[] splitA = fileA[i].Split(';');
            MapData ujMapData = new MapData();
            ujMapData.partName = splitA[0];
            if (splitA[1] == "1")
            {
                ujMapData.IsUnlocked = true;
            }
            else
            {
                ujMapData.IsUnlocked = false;
            }
            mapDatas.Add(ujMapData);
        }
        foreach (MapData data in mapDatas)
        {
            Debug.Log(data.partName);
            Debug.Log(data.IsUnlocked);
        }
    }

    public void LoadMapDatas()
    {
        Debug.Log(persistentPath);
        if (!File.Exists(persistentPath))
        {
            LoadIfFileNotExists();
        }
        else
        {
            LoadFile();
           
        }




    }
    
}
