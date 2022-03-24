
using UnityEngine;
public class ExitGame : MonoBehaviour
{
    MapMapLoading mapSave;
    public void ExitFromGame()
    {
        PlayerPrefs.Save();
        MapMapLoading.SaveMapDatas();
        Application.Quit();
    }
}
