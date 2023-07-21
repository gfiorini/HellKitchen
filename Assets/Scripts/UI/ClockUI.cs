using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField]
    private Image clockImage;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChange += OnGameStatusChanged;
        gameObject.SetActive(false);
    }
    private void OnGameStatusChanged(object sender, GameManager.EventGameState e) {
        if (e.state == GameManager.GameState.RUNNING){
            gameObject.SetActive(true);
        } else{
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        clockImage.fillAmount = GameManager.Instance.GetPlayTimerNormalized();
    }
}
