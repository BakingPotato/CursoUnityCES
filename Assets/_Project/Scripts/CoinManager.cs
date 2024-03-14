using UnityEngine;


public class CoinManager : MonoBehaviour
{
	public float value = 1;
	GameManager GM;

	private void Start()
	{
		GM = GameManager.Instance;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			GM.updateCoins();
			Destroy(gameObject);
		}

	}

}
