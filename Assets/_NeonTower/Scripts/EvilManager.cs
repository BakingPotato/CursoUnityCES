using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilManager : MonoBehaviour
{

	FloorManager GM;

	private void Start()
	{
		GM = FloorManager.Instance;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GM.updateScore();
			if(GM.coin_objective > -1)
            {
				int hp = GM.takeDamage();
				if (hp <= 0)
				{
					GM.ui.GameOver();
					Destroy(collision.gameObject);
				}
            }
            else
            {
				GM.ui.GameOver();
				Destroy(collision.gameObject);
			}


		}
	}
}
