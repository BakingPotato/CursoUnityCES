using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalPos : MonoBehaviour {

	public bool activada = false;

	private MuevePlayer mp;

	// Use this for initialization
	void Start () {
		mp = GameObject.Find("Jugador").GetComponent<MuevePlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//-------------------------------------------------
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.tag == "Hielo")
		{
			//if (mp.tecladoActivo)
			//{
				if (!(activada))
				{
					activada = true;
					//mp.goalsOk++;
				}
			//}
		}
	}
	//-------------------------------------------------
	private void OnTriggerExit(Collider other)
	{
		if (activada)
		{
			activada = false;
			//mp.goalsOk--;
		}
	}
}
