using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    GameManager_SI GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager_SI.instance;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeDir();
    }

    private void FixedUpdate()
    {
    }

    void ChangeDir()
    {
        if(this.transform.position.x <= -3 || this.transform.position.x >= 3){
            GM.moveAliensDown();
        }
    }

    void Move()
    {
        if (GM.moveRight)
        {
            transform.Translate(Vector3.right * GM.enemy_speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * GM.enemy_speed * Time.deltaTime);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            GM.updateScore(1);
            Destroy(this.gameObject);
        }
    }
}
