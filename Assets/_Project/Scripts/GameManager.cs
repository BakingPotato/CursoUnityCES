using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("Null GM");
			return _instance;
		}
	}

	private static GameManager _instance;

	public int round = 0;

	[Header("Coins")]
	public GameObject coin_prefab;
	public GameObject coins_parent;
	private int coin_count = 0;

	[Header("Evil Coins")]
	public GameObject evil_coin_prefab;
	public GameObject evil_coins_parent;
	public int max_evil_coins = 9;

	[Header("UI")]
	[SerializeField] TMP_Text ui_coins;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		ui_coins.text = "Coins: 0";
	}

	private void Update()
	{
		if(coins_parent.transform.childCount == 0)
		{
			GenerateMoreCoins();
		}	
	}

	private void GenerateMoreCoins()
	{
		int coin_number = Random.Range(12, 24);

		for (int i = 0; i < coin_number; i++)
		{
			Instantiate(coin_prefab, new Vector3(Random.Range(8f, -8.2f), 0, Random.Range(4.3f, -4.45f)), Quaternion.identity, coins_parent.transform);
		}

		//Cada 3 rondas, invocar nueva moneda malvada hasta que lleguemos al número máximo
		if(round%3 == 0 && evil_coins_parent.transform.childCount < max_evil_coins)
        {
			Instantiate(evil_coin_prefab, new Vector3(Random.Range(8f, -8.2f), 0, Random.Range(4.3f, -4.45f)), Quaternion.identity, evil_coins_parent.transform);
		}

		round++;
	}

	public void updateCoins()
	{
		coin_count++;
		ui_coins.text = "Coins: " + coin_count;
	}
}
