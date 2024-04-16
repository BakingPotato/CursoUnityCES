using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float rangoX = 3;
    public float rangoY = 3;

    public Asteroid[] asteroid_prefabs;

    public enum direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public direction spawnDirection;

    public void SpawnAsteroid()
    {
        Vector2 actual_direction = Vector2.zero;
        float ranX = Random.Range(-rangoX, rangoX);
        float ranY = Random.Range(-rangoY, rangoY);
        Asteroid prefab = Instantiate(asteroid_prefabs[Random.Range(0, asteroid_prefabs.Length)], transform.position + new Vector3(ranX, ranY, 0), Quaternion.identity);
        switch(spawnDirection)
        {
            case direction.Left:
                actual_direction = new Vector2(-1, Random.Range(-1, 1));
                break;
            case direction.Right:
                actual_direction = new Vector2(1, Random.Range(-1, 1));
                break;
            case direction.Up:
                actual_direction = new Vector2(Random.Range(-1, 1), 1);
                break;
            case direction.Down:
                actual_direction = new Vector2(Random.Range(-1, 1), -1);
                break;
        }

        prefab.startMoving(actual_direction);
    }
}
