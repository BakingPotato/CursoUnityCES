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
        float ranX = Random.Range(-rangoX, rangoX);
        float ranY = Random.Range(-rangoY, rangoY);
        Asteroid prefab = Instantiate(asteroid_prefabs[Random.Range(0, asteroid_prefabs.Length)], transform.position + new Vector3(ranX, ranY, 0), Quaternion.identity);
        switch(spawnDirection)
        {
            case direction.Left:
                prefab.direction = new Vector2(-1, Random.Range(-1, 1));
                break;
            case direction.Right:
                prefab.direction = new Vector2(1, Random.Range(-1, 1));
                break;
            case direction.Up:
                prefab.direction = new Vector2(Random.Range(-1, 1), 1);
                break;
            case direction.Down:
                prefab.direction = new Vector2(Random.Range(-1, 1), -1);
                break;
        }

        prefab.startMoving();
    }
}
