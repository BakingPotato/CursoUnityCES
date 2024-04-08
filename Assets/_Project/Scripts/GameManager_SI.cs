using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_SI : MonoBehaviour
{
    //El gameManager gestiona todos los aspectos del juego
    //Normalmente es el que configura la escena de inicio
    //Conviene convertirlo en instancia única, y quizás persistente
    public GameObject[] enemies = new GameObject[2];
    public Transform initialEnemyPosition;

    // Start is called before the first frame update
    void Start()
    {
        for(int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 11; i++)
            {
                Instantiate(enemies[0]);
                enemies[0].transform.position = initialEnemyPosition.transform.position;
                initialEnemyPosition.position += new Vector3(0.5f, 0, 0);
            }
            initialEnemyPosition.position += new Vector3(-0.5f * 11, -0.5f, 0);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
