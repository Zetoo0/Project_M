using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class ResumeGameButton : MonoBehaviour
{
    public void ResumeGame()
    {
        GetComponentInParent<Pause>().Resume();
    }
}
