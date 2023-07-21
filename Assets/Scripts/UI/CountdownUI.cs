using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
   [SerializeField]
   private TextMeshProUGUI countdownText;

   private void Start() {
      GameManager.Instance.OnGameStateChange += OnGameStateChanged;
      Hide();
   }

   private void Update() {
      float countdown = GameManager.Instance.GetCountdownTimer();
      countdownText.text = Math.Ceiling(countdown).ToString();
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
