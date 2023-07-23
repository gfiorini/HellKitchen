using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;

    private float MAX_TIMER = 0.2f;

    private float timer;
    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer > MAX_TIMER){
            if (player.IsWalking()){
                PlayMoveSound();
            }
            timer = 0;
        }
    }
    private void PlayMoveSound() {
        float volume = 1f;
        SFXManager.Instance.PlayFootsteps(player.transform.position, volume);
    }
}
