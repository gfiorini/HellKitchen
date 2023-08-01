using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
   private const string COUNTDOWN_TRIGGER = "countdownTrigger";

   [SerializeField]
   private TextMeshProUGUI countdownText;

   private Animator _animator;

   private int previousTimer;
   
   private int currTimer;

   private void Start() {
      _animator = GetComponent<Animator>();
      GameManager.Instance.OnGameStateChange += OnGameStateChanged;
      Hide();
   }

   private void Update() {
      float countdown = GameManager.Instance.GetCountdownTimer();
      previousTimer = Mathf.CeilToInt( GameManager.Instance.GetCountdownTimer());
      if (currTimer != previousTimer){
         currTimer = previousTimer;
         countdownText.text = currTimer.ToString(); 
         _animator.SetTrigger(COUNTDOWN_TRIGGER);
         SFXManager.Instance.PlayCountdown();
      }
      
      
      
   }
   private void OnGameStateChanged(object sender, GameManager.EventGameState e) {
      if (e.state == GameManager.GameState.COUNTDOWN){
         Show();
      } else{
         Hide();
      }
   }
   private void Hide() {
      gameObject.SetActive(false);
   }
   private void Show() {
      gameObject.SetActive(true);
   }
}
