using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SFXManager : MonoBehaviour
{
    
    public static SFXManager Instance {get; private set; }
    
    [SerializeField]
    private SoundFX_SO sfx;
    void Start() {
        Instance = this;
        DeliveryManager.Instance.OnOrderSuccess += OnOrderSuccess;
        DeliveryManager.Instance.OnOrderFailed += OnOrderFailed;
        CutterCounter.OnAnyCut += OnAnyCut;
        Player.Instance.OnPickupObject += OnPickUpObject;
        BaseCounter.OnDroppedObject += OnDroppedObject;
        TrashCounter.OnTrash += OnTrash;
        Plate.OnPlateAdd += OnAddIngredient;

    }
    private void OnAddIngredient(object sender, EventArgs e) {
        AudioClip clip = GetAudioClip(sfx.objectDrop);
        PlaySound(clip, (sender as GameObject).transform.position);
    }
    private void OnTrash(object sender, EventArgs e) {
        AudioClip clip = GetAudioClip(sfx.trash);
        PlaySound(clip, (sender as GameObject).transform.position);
    }
    private void OnDroppedObject(object sender, EventArgs e) {
        AudioClip clip = GetAudioClip(sfx.objectDrop);
        PlaySound(clip, (sender as GameObject).transform.position);
    }
    private void OnPickUpObject(object sender, EventArgs e) {
        AudioClip clip = GetAudioClip(sfx.objectPickup);
        PlaySound(clip, (sender as GameObject).transform.position);
    }
    private void OnAnyCut(object sender, EventArgs e) {
        AudioClip clip = GetAudioClip(sfx.chop);
        Debug.Log((transform.position));
        PlaySound(clip, (sender as GameObject).transform.position);
    }
    private void OnOrderFailed(object sender, EventArgs e) {
        AudioClip clip = GetAudioClip(sfx.deliveryFail);
        PlaySound(clip, (sender as GameObject).transform.position);
    }
    private void OnOrderSuccess(object sender, EventArgs e) {
        AudioClip clip = GetAudioClip(sfx.deliverySuccess);
        PlaySound(clip, (sender as GameObject).transform.position);
    }
    
    // private void OnPickupObject(object sender, EventArgs e) {
    //     AudioClip clip = GetAudioClip(sfx.objectPickup);
    //     PlaySound(clip, (sender as BaseCounter).transform.position);
    // }
    
    private AudioClip GetAudioClip(List<AudioClip> clips) {
        return clips[Random.Range(0, clips.Count)];
    }
    // Update is called once per frame
    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1.0f) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayFootsteps(Vector3 position, float volume = 1.0f) {
        AudioClip clip = GetAudioClip(sfx.footstep);
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}
