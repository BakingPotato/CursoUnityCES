using TMPro;
using UnityEngine;

public class UIManager_HC : MonoBehaviour
{

    public TMP_Text round;
    public TMP_Text coins;
    public TMP_Text score;
    public GameObject gameOverPanel;

    public void UpdateCoins(int coins)
    {
        this.coins.text = "Coins : " + coins.ToString();
    }

    public void UpdateRound(int round)
    {
        this.round.text = "Round : " + round.ToString();
    }

    public void UpdateScore(int score)
    {
        this.score.text = "Your score: " + score.ToString();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
