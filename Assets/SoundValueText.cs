using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class SoundValueText : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI valueText;

    void Update()
    {
        valueText.text = Volume.volumeVol.ToString();
    }

}
