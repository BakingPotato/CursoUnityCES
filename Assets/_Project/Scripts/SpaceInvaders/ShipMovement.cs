using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    Vector3 vMov;

    [Header("Movimiento")]
    public float speed = 3;

    [Header("Disparos")]
    public GameObject pea_shoot;
    public float cooldown = 1;

    GameManager_SI GM;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager_SI.instance;
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x >= -2.9f)
                transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x <= 2.9f)
                transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    IEnumerator Shoot()
    {
        Vector3 shoot_pos;

        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                shoot_pos = transform.position + new Vector3(0, 0.4f, 0);

                Instantiate(pea_shoot, shoot_pos, Quaternion.identity);

                yield return new WaitForSeconds(cooldown);

            }
            yield return new WaitForEndOfFrame();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            GM.ResetGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            GM.ResetGame();
        }
    }
}
