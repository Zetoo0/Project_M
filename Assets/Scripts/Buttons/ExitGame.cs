
using UnityEngine;
public class ExitGame : MonoBehaviour
{
    public void ExitFromGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
