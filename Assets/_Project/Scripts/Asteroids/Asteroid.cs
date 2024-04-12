using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int size = 3;
    GameManager_Asteroid GM;
    public GameObject son_prefab;
    Vector3 viewportPos;
    public Vector2 direction;

    private void Start()
    {
        GM = GameManager_Asteroid.instance;

        GM.asteroid_count++;
    }

    public void startMoving()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
    }

    public void Update()
    {
        destroyAsteroid();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GM.asteroid_count--;

            Destroy(collision.gameObject);

            if (size > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(son_prefab, transform.position, Quaternion.identity);
                }
            }

            Destroy(gameObject);
        }
    }

    private void destroyAsteroid()
    {
        // Convertimos nuestra posición con respecto a la camara a un vector. Este vector estara en el rango de 0 a 1.
        // Esto significa que x ira de 1 a 0 (derecha a izquierda) y de 1 a 0 (arriba a abajo)
        viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPos.x < -0.3 || viewportPos.x > 1.3 || viewportPos.y < -0.3 || viewportPos.y > 1.3)
        {
            GM.asteroid_count--;
            Destroy(this.gameObject);
        }
    }
}
