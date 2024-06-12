using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarvationMenu : MonoBehaviour
{
    public TMP_Text record;

    private void Start()
    {
        record.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void SurvivalMode()
    {
        SceneManager.LoadScene("CoinHell_Infinite");
    }

    public void AdventureMode()
    {
        SceneManager.LoadScene("CoinHell_1");
    }
}
