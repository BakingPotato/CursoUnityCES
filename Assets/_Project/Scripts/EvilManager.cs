using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilManager : MonoBehaviour
{

	GameManager GM;

	private void Start()
	{
		GM = GameManager.Instance;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GM.updateScore();
            GM.ui.GameOver();
			Destroy(collision.gameObject);
		}
	}
}
