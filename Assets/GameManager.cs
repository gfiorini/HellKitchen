using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public enum GameState
    {
        WAIT_TO_START,
        COUNTDOWN,
        RUNNING,
        GAME_OVER
    }

    public class EventGameState : EventArgs
    {
        public GameState state;
    };
    public event EventHandler<EventGameState> OnGameStateChange;

    private float waitToStartTimer = 1f;
    private float countdownTimer = 3f;
    private float MAX_RUN_TIMER = 10f;
    private float currentRunTimer;
    
    private GameState state = GameState.WAIT_TO_START;
    
    private void Awake() {
        Instance = this;
    }

    private void Update() {
        switch (state){
            case GameState.WAIT_TO_START:
                waitToStartTimer -= Time.deltaTime;
                if (waitToStartTimer < 0){
                    state = GameState.COUNTDOWN;
                    OnGameStateChange?.Invoke(this, new EventGameState(){state = state});
                }
                break;
            case GameState.COUNTDOWN:
                countdownTimer -= Time.deltaTime;
                if (countdownTimer <0){
                    state = GameState.RUNNING;
                    OnGameStateChange?.Invoke(this, new EventGameState(){state = state});     
                }
                break;                
            case GameState.RUNNING:
                currentRunTimer += Time.deltaTime;
                if (currentRunTimer >= MAX_RUN_TIMER){
                    state = GameState.GAME_OVER;
                    OnGameStateChange?.Invoke(this, new EventGameState(){state = state});            
                }
                break;                  
            case GameState.GAME_OVER:
                break;               
        }
    }

    public float GetCountdownTimer() {
        return countdownTimer;
    }
    
    public float GetPlayTimerNormalized() {
        return currentRunTimer / MAX_RUN_TIMER;
    }
}