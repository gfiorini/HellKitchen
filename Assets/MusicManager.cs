using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance { get; private set; }
    private AudioSource _audioSource;

    private float volume = 0.3f;
    private void Awake() {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }
    void Start() {
        //volume = PlayerPrefs.GetFloat(Preferences.MUSIC_VOLUME.ToString());
        _audioSource.volume = volume;
    }

    public void ChangeVolume() {
        volume += 0.1f;
        if (Math.Round((decimal)volume, 2) > 1){
            volume = 0f;
        }
        //PlayerPrefs.SetFloat(Preferences.MUSIC_VOLUME.ToString(), volume);
        //PlayerPrefs.Save();
        _audioSource.volume = volume;
    }
    
    public float GetVolume() {
        return volume;
    }
}
