using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countText;
    void Start()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChange;
        Hide();
    }
    private void OnGameStateChange(object sender, GameManager.EventGameState e) {
        if (e.state == GameManager.GameState.GAME_OVER){
            int deliveries = DeliveryManager.Instance.GetDeliveries();
            countText.text = deliveries.ToString();
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
