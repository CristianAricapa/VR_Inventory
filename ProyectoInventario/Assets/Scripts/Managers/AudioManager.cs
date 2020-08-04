using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioManager;

    public AudioClip[] _audio;


    // Use this for initialization
    void Start()
    {
        audioManager = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(Constants.AudioFX type)
    {
        if ((int)type == 3)
        {
            audioManager.PlayOneShot(_audio[(int)type], 0.2f);
        }
        else
            audioManager.PlayOneShot(_audio[(int)type]);

    }
}
