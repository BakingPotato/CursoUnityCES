using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Castle : MonoBehaviour
{
    public static GameManager_Castle Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Null GM");
            return _instance;
        }
    }

    private static GameManager_Castle _instance;

    public GameObject player;
    public GameObject playerPrefab;
    public GameObject spawnPoint;

    void Awake()
    {
        _instance = this;
    }
    public void respawn()
    {
        Destroy(player.gameObject);
        player = Instantiate(playerPrefab);
        player.transform.position = spawnPoint.transform.position;
    }
}
