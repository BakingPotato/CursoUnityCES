using TMPro;
using UnityEngine;

public class UIManager_HC : MonoBehaviour
{

    public TMP_Text round;
    public TMP_Text coins;
    public GameObject gameOverPanel;

    public void UpdateCoins(int coins)
    {
        this.coins.text = "Coins : " + coins.ToString();
    }

    public void UpdateRound(int round)
    {
        this.round.text = "Round : " + round.ToString();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
