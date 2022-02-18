using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer audioMixer;

   public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        Debug.Log("Volume is changed to " + volume);
    }
}
