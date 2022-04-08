using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowMessageM : MonoBehaviour
{
    [SerializeField] TMP_Text textOut;

    

    public IEnumerator ShowMessage(string message)
    {
       // TMP_Text textOut;
       // textOut.rectTransform.position = new Vector3(-16.0f, -70.0f,0.0f);
        textOut.SetText(message);
        yield return new WaitForSecondsRealtime(5);
        textOut.SetText("");
    }
}
