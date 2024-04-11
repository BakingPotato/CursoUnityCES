using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameManager_Chess : MonoBehaviour
{
    [Header("Balanceo")]
    public float time_to_fall = 1;
    public float time_falling = 2;
    public float cooldown = 1;


	public static GameManager_Chess Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("Null GM");
			return _instance;
		}
	}
	private static GameManager_Chess _instance;

    private void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
