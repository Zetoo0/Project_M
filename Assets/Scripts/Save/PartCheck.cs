using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PartCheck : MonoBehaviour
{
   
    [SerializeField] string levelName;
    [SerializeField] public int mapId;
    Button mapBtn;
    [SerializeField] GameObject userPanel;
    int tempKeyValue;

    

    private void Start()
    {
        this.mapBtn = GetComponent<Button>();
    }

    /*private void OnEnable()
    {
        CheckMapLockState();
    }*/

    private void IsUnlocked()
    {
        tempKeyValue = 0;

        if (PlayerPrefs.HasKey(levelName))
        {
            tempKeyValue = PlayerPrefs.GetInt(levelName);
            if(tempKeyValue == 1)
            {
                mapBtn.interactable = true;
            }
            else
            {
                mapBtn.interactable = false;
            }
        }
    }

    public void CheckMapLockState()
    {
        if (PlayerPrefs.HasKey(this.levelName))
        {
            Debug.Log("Sikeres + " + this.levelName + " " + PlayerPrefs.GetInt(this.levelName));
            mapBtn.interactable = false;
            return;
        }
        else
        {
            Debug.Log("Sikertelen + " + this.levelName + " " + PlayerPrefs.GetInt(this.levelName));
            return;
        }
    }

    public void SetMapstartPanelActiveAndChangeChosenMapId()
    {

        LevelLoader.chosenMapId = this.mapId;
        OpenExitButton.Open(userPanel);


    }


}
