using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SFXManager : MonoBehaviour
{
    [SerializeField]
    private SoundFX_SO sfx;
    void Start() {
        DeliveryManager.Instance.OnOrderSuccess += OnOrderSuccess;
        DeliveryManager.Instance.OnOrderFailed += OnOrderFailed;
    }
    private void OnOrderFailed(object sender, EventArgs e) {
        AudioClip clip = GetAudioClip(sfx.deliveryFail);
        PlaySound(clip, (sender as Transform).position);
    }
    private void OnOrderSuccess(object sender, EventArgs e) {
        AudioClip clip = GetAudioClip(sfx.deliverySuccess);
        PlaySound(clip, (sender as Transform).position);
    }
    
    // private void OnPickupObject(object sender, EventArgs e) {
    //     AudioClip clip = GetAudioClip(sfx.objectPickup);
    //     PlaySound(clip, (sender as BaseCounter).transform.position);
    // }
    
    private AudioClip GetAudioClip(List<AudioClip> clips) {
        return clips[Random.Range(0, clips.Count - 1)];
    }
    // Update is called once per frame
    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1.0f) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}
