using UnityEngine;


public class CoinManager : MonoBehaviour
{
	public float value = 1;
	GameManager GM;

	private void Start()
	{
		GM = GameManager.Instance;
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player")
		{
			GM.updateCoins();
			Destroy(gameObject);
		}
	}

}
