using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_HC : MonoBehaviour
{

    public TMP_Text round;
    public TMP_Text coins;
    public TMP_Text score;
    public TMP_Text thought;
    public GameObject gameOverPanel;
    public GameObject recordPanel;

    [Header("HP")]
    public GameObject health;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;

    public void UpdateCoins(int coins, int objective)
    {
        if(objective > -1)
        {
            this.coins.text = "PK : " + coins.ToString() + " / " + objective;
        }
        else
        {
            this.coins.text = "PK : " + coins.ToString();
        }
    }

    public void UpdateRound(int round)
    {
        this.round.text = "Round : " + round.ToString();
    }

    public void UpdateFloor(int floor)
    {
        this.round.text = "Floor : " + floor.ToString();
    }

    public void UpdateScore(int score)
    {
        this.score.text = "Your score: " + score.ToString();
    }


    public void UpdateThought(string thought)
    {
        this.thought.text = thought;
    }

    public void showNewRecord()
    {
        recordPanel.SetActive(true);
    }

    public void healthVisibility(bool visible)
    {
        health.SetActive(visible);
    }

    public void setHealth(int health)
    {
        switch (health)
        {
            case 0:
                hp1.SetActive(false);
                hp2.SetActive(false);
                hp3.SetActive(false);
                break;
            case 1:
                hp1.SetActive(true);
                hp2.SetActive(false);
                hp3.SetActive(false);
                break;
            case 2:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(false);
                break;
            case 3:
                hp1.SetActive(true);
                hp2.SetActive(true);
                hp3.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
