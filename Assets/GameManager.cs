using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    public event EventHandler OnPause;
    public event EventHandler OnResume;

    //private float waitToStartTimer = 1f;
    private float countdownTimer = 3f;
    private float GAMEPLAY_TIMER = 180f;
    private float currentRunTimer;
    
    private GameState state = GameState.WAIT_TO_START;

    private bool isPaused;
    
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        isPaused = false;
        GameInput.Instance.OnPauseHandler += GameInputOnPauseHandler;
        GameInput.Instance.OnInteractHandler += OnInteractHandler;
    }
    private void OnInteractHandler(object sender, EventArgs e) {
        if (state == GameState.WAIT_TO_START){
            state = GameState.COUNTDOWN;
            OnGameStateChange?.Invoke(this, new EventGameState(){state = state});
        }
    }
    private void Update() {
        switch (state){
            case GameState.WAIT_TO_START:
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
                if (currentRunTimer >= GAMEPLAY_TIMER){
                    state = GameState.GAME_OVER;
                    OnGameStateChange?.Invoke(this, new EventGameState(){state = state});            
                }
                break;                  
            case GameState.GAME_OVER:
                break;               
        }
    }

    private void GameInputOnPauseHandler(object sender, EventArgs e) {
        TogglePause();
    }
    public void TogglePause() {
        if (!isPaused){
            OnPause?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0f;
        } else{
            OnResume?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1f;
        }
    
        isPaused = !isPaused;
    }
    public float GetCountdownTimer() {
        return countdownTimer;
    }
    
    public float GetPlayTimerNormalized() {
        return currentRunTimer / GAMEPLAY_TIMER;
    }

    public GameState GetState() {
        return state;
    }

}
