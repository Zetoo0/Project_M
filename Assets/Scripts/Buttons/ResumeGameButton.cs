
using UnityEngine;

public class ResumeGameButton : MonoBehaviour
{
    public void ResumeGame()
    {
        GetComponentInParent<Pause>().Resume();
    }
}
