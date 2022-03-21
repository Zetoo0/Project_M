
using UnityEngine;

public class OpenExitButton
{
    public GameObject panel { get; set; }

   static public void Exit(GameObject panel)
    {
        panel.SetActive(false);
    }

   static public void Open(GameObject panel)
    {
        panel.SetActive(true);
    }




}
