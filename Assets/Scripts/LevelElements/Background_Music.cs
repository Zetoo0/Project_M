using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Music : MonoBehaviour
{
    [SerializeField] AudioClip backgroundMusic;
    void Update()
    {
        AudioSource.PlayClipAtPoint(backgroundMusic, Camera.main.transform.position);
    }
}
