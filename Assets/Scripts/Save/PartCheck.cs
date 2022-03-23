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
        mapBtn = GetComponent<Button>();
    }

    private void OnEnable()
    {
       // PlayerPrefs.SetInt(levelName, Random.Range(5,10));
        CheckMapLockState();
    }

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
        if (PlayerPrefs.HasKey(levelName))
        {
            Debug.Log("Sikeres + " + levelName + " " + PlayerPrefs.GetInt(levelName));
            mapBtn.interactable = true;
            
        }
        else
        {
            Debug.Log("Locked + " + levelName + " " + PlayerPrefs.GetInt(levelName));
            
        }
    }

    public void SetMapstartPanelActiveAndChangeChosenMapId()
    {

        LevelLoader.chosenMapId = this.mapId;
        OpenExitButton.Open(userPanel);


    }
}
