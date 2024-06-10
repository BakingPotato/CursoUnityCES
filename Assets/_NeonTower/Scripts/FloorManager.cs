using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FloorManager : MonoBehaviour
{

	public static FloorManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("Null GM");
			return _instance;
		}
	}

	enum mode
    {
		coins,
		target,
		survive
    }

	private static FloorManager _instance;

	public int round = 0;

	[Header("Coins")]
	public GameObject coin_prefab;
	public GameObject coins_parent;
	private int coin_count = 0;
	public int coin_objective = 50;
	public int min_coin_per_round = 12;
	public int max_coin_per_round = 24;

	[Header("Evil Coins")]
	public bool autogenerate = true;
	public GameObject evil_coin_prefab;
	public GameObject evil_coins_parent;
	public List<GameObject> evil_coins_list;
	public int max_evil_coins = 9;

	[Header("UI")]
	public UIManager_HC ui;
	int score = 0;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		ui.UpdateCoins(0, coin_objective);
		ui.UpdateFloor(GlobalGameManager.Instance.actual_floor);
	}

	private void Update()
	{
		if(coins_parent.transform.childCount == 0)
		{
			GenerateMoreCoins();
		}	

		if(coin_count > coin_objective)
        {
			deleteEvilCoins();
			nextFloor();
        }
	}

	private void GenerateMoreCoins()
	{
		int coin_number = Random.Range(min_coin_per_round, max_coin_per_round);
		GameObject aux;

		for (int i = 0; i < coin_number; i++)
		{
			Instantiate(coin_prefab, new Vector3(Random.Range(8f, -8.2f), 0, Random.Range(4.3f, -4.45f)), Quaternion.identity, coins_parent.transform);
		}

        if (autogenerate)
        {
			//Cada 3 rondas, invocar nueva moneda malvada hasta que lleguemos al número máximo
			if (round % 2 == 0 && evil_coins_parent.transform.childCount < max_evil_coins)
			{
				aux = Instantiate(evil_coin_prefab, new Vector3(Random.Range(8f, -8.2f), 0, Random.Range(4.3f, -4.45f)), Quaternion.identity, evil_coins_parent.transform);
				evil_coins_list.Add(aux);
			}
		}

		round++;
        //ui.UpdateRound(round);
    }

    public void updateCoins()
	{
		coin_count++;
        ui.UpdateCoins(coin_count, coin_objective);
    }

	public void deleteEvilCoins()
    {
		foreach(GameObject evil in evil_coins_list)
        {
			Destroy(evil);
        }
    }
	
    public void restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void updateScore()
    {
		score = coin_count * (round);
		ui.UpdateScore(score);

	}

	public void nextFloor()
    {
		GlobalGameManager.Instance.nextFloor();
    }
}
