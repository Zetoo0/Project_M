using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Volume : MonoBehaviour
{
    public AudioMixer audioMixer;
    string exposedName = "volume";
    public static float volumeVol;
    float volumeVout;

   public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat(exposedName, volume);
        volumeVol = volume;
        Debug.Log(audioMixer.GetFloat(exposedName, out volume));
       // Debug.Log("Volume is changed to " + volume);
    }
}
