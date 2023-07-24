using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SFXManager : MonoBehaviour
{
    
    public static SFXManager Instance {get; private set; }

    private float volume = 1f;
    
    [SerializeField]
    private SoundFX_SO sfx;

    private void Awake() {
        Instance = this;
        if (PlayerPrefs.HasKey(Preferences.SFX_VOLUME.ToString())){
            volume = PlayerPrefs.GetFloat(Preferences.SFX_VOLUME.ToString());
        }        
    }
    void Start() {
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
    
    private AudioClip GetAudioClip(List<AudioClip> clips) {
        return clips[Random.Range(0, clips.Count)];
    }
    // Update is called once per frame
    public void PlaySound(AudioClip clip, Vector3 position) {
        AudioSource.PlayClipAtPoint(clip, position, volume );
    }

    public void PlayFootsteps(Vector3 position) {
        AudioClip clip = GetAudioClip(sfx.footstep);
        AudioSource.PlayClipAtPoint(clip, position, volume );
    }
    public void ChangeVolume() {
        volume += 0.1f;
        if (Math.Round((decimal)volume, 2) > 1){
            volume = 0f;
        }
        PlayerPrefs.SetFloat(Preferences.SFX_VOLUME.ToString(), volume);
        PlayerPrefs.Save();        
    }

    public float GetVolume() {
        return volume;
    }
    
}
