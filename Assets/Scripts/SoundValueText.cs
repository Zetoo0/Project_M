using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class SoundValueText : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI valueText;
    float volume = 100;
    float volOut;

    void Update()
    {
        volume = Mathf.Clamp(Volume.volumeVol, 100.0f, 0.0f);
        volOut = Volume.volumeVout;
        valueText.text = volOut.ToString();
    }

}
