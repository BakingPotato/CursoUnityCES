using UnityEngine;


public class CoinManager : MonoBehaviour
{
	public float value = 1;
	FloorManager GM;

	private void Start()
	{
		GM = FloorManager.Instance;
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
