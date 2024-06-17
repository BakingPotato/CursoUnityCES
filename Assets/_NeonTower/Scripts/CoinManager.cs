using System.Collections;
using UnityEngine;


public class CoinManager : MonoBehaviour
{
	public float value = 1;
	FloorManager GM;
	public AudioSource pop;

	private void Start()
	{
		GM = FloorManager.Instance;
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player")
		{
			pop.Play();
			GM.updateCoins();
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			StartCoroutine(AutoDestroy());
		}else if (other.gameObject.name.Contains("Static"))
        {
			Destroy(gameObject);
		}
	}

	IEnumerator AutoDestroy()
    {
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
    }

}
