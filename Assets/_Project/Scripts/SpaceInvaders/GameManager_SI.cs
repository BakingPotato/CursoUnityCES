using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_SI : MonoBehaviour
{
    //El gameManager gestiona todos los aspectos del juego
    //Normalmente es el que configura la escena de inicio
    //Conviene convertirlo en instancia �nica, y quiz�s persistente
    public GameObject[] enemies = new GameObject[2];
    public Transform initialEnemyPosition;

    public float enemy_speed = 0.1f;
    public bool moveRight = true;

    public static GameManager_SI instance;

    GameObject enemy_parent;

    float score = 0;

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy_parent = initialEnemyPosition.transform.parent.gameObject;
        spawnAliens();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnAliens()
    {
        GameObject actual_enemy;
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 11; i++)
            {
                actual_enemy = Instantiate(enemies[i % 2], enemy_parent.transform);
                actual_enemy.transform.position = initialEnemyPosition.transform.position;
                initialEnemyPosition.position += new Vector3(0.5f, 0, 0);
            }
            initialEnemyPosition.position += new Vector3(-0.5f * 11, -0.5f, 0);
        }
        //Lo retornamos a la posición original
        initialEnemyPosition.position += new Vector3(-0.5f * 11, -0.5f * 4, 0);
    }

    public void moveAliensDown()
    {
        enemy_speed += 0.1f;
        moveRight = !moveRight;
        enemy_parent.transform.position += new Vector3(0, -0.25f, 0);

    }

    public void Reset()
    {
        enemy_parent.transform.position = Vector3.zero;
        spawnAliens();
    }

    public void updateScore(float points)
    {
        score += points;
    }

}
