using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Asteroid : MonoBehaviour
{
    public static GameManager_Asteroid instance;

    public float asteroid_time_spawn = 3;

    [HideInInspector] public int asteroid_count = 0;

    public int num_asteroids = 10;

    public AsteroidSpawner[] spawners;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(spawnAsteroidsRoutine());
    }

    IEnumerator spawnAsteroidsRoutine()
    {
        while (true)
        {
            if (asteroid_count == 0 || asteroid_count < num_asteroids)
            {
                // Spawneamos un número al azar entre 1 y el número de asteroides que quedan
                int objective = Random.Range(1, num_asteroids - asteroid_count);
                for (int i = 0; i < objective; i++)
                {
                    spawners[Random.Range(0, spawners.Length)].SpawnAsteroid();
                }
            }

            yield return new WaitForSeconds(asteroid_time_spawn);
        }
    }

    //private void SpawnAsteroid()
    //{
    //    // Usamos el viewport para saber donde spawnear
    //    //Sacamos un número random
    //    float offset = Random.Range(0f, 1f);
    //    Vector2 viewportSpawnPosition;

    //    // Sacamos en que borde vamos a spawnear
    //    int edge = Random.Range(0, 4);

    //    //(x, 0) Abajo izquierda
    //    if (edge == 0)
    //    {
    //        viewportSpawnPosition = new Vector2(offset, 0);
    //    }
    //    //(x, 1) arriba derecha
    //    else if (edge == 1)
    //    {
    //        viewportSpawnPosition = new Vector2(offset, 1);
    //    }
    //    //(0, x) arriba izquierda
    //    else if (edge == 2)
    //    {
    //        viewportSpawnPosition = new Vector2(0, offset);
    //    }
    //    //(1, x) arriba izquierda
    //    else
    //    {
    //        viewportSpawnPosition = new Vector2(1, offset);
    //    }

    //    // Instanciamos el asteroide
    //    Vector3 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
    //    Instantiate(asteroid_prefab[Random.Range(0, asteroid_prefab.Length)], worldSpawnPosition, Quaternion.identity);
    //}

    public void GameOver()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        Debug.Log("Game Over");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return null;
    }
}
