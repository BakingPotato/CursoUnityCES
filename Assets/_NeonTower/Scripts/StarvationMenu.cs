using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarvationMenu : MonoBehaviour
{
    public void SurvivalMode()
    {
        SceneManager.LoadScene("CoinHell_Infinite");
    }

    public void AdventureMode()
    {
        SceneManager.LoadScene("CoinHell_1");
    }
}
