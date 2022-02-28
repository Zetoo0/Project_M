using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Volume : MonoBehaviour
{
    public AudioMixer audioMixer;
    string exposedName = "volume";
    public static float volumeVol;
    public static float volumeVout;

    [SerializeField] Slider auSlider;



    void Start()
    {
        auSlider.value = PlayerPrefs.GetFloat("VolumeSlider", volumeVol);
    }


    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat(exposedName, volume);
        volumeVol = volume;
        volumeVout = Mathf.Round(Mathf.Abs(volumeVol));
        PlayerPrefs.SetFloat("VolumeSlide", volumeVol);
        Debug.Log(audioMixer.GetFloat(exposedName, out volume));
        Debug.Log("Volume is changed to " + volume);
    }
}
