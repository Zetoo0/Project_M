using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IsThisUnlocked : MonoBehaviour
{
    [SerializeField] public string levelName;
    [SerializeField] public int saveId;
    

    /*public void MapUnlock()
    {
        //Debug.Log(PlayerPrefs.GetInt(this.levelName, this.saveId));
        if (PlayerPrefs.GetInt(this.levelName, this.saveId) == this.saveId)
        {
            Debug.Log("Már unlockoltad ezt a pályát");
            Debug.Log(PlayerPrefs.GetInt(this.levelName, this.saveId));
        }
        else
        {
            //this.saveId = Random.Range(5, 5000);
            Debug.Log("Unlocked map :): " + this.levelName + " " + this.saveId);
            PlayerPrefs.SetInt(this.levelName, this.saveId);
            PlayerPrefs.Save();
            //.Log("Sikeres mentés:" + this.levelName + " " , this.saveId + " ",+ PlayerPrefs.GetInt(this.levelName));
            Debug.Log("Sikeres mentés adatok:");
            Debug.Log("Level name: " + this.levelName);
            Debug.Log("MEntett id: " + this.saveId);
            Debug.Log("Van-e mostmár kulcs: " + PlayerPrefs.HasKey(this.levelName));
            Debug.Log("Lekért adat id: " + PlayerPrefs.GetInt(this.levelName));
        }
    }*/

    public static void MapUnlock(string levelName, int saveId)
    {
       // PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt(levelName) == saveId)
        {
            Debug.Log("már unlockoltad");
            Debug.Log(PlayerPrefs.GetInt(levelName, saveId));
        }
        else
        {
            PlayerPrefs.SetInt(levelName, saveId);
            PlayerPrefs.Save();
            Debug.Log("Id: " + PlayerPrefs.GetInt(levelName));
        }
    }
}
