using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private Vector3 prA, prB, destino;
    private SpriteRenderer sr;

    public float velocity = 4;

    public bool left = true;

    // Start is called before the first frame update
    void Start()
    {//La función getChild nos va dando la transform de los hijos, en orden de arriba abajo
        sr = this.GetComponent<SpriteRenderer>();

        prA = this.transform.GetChild(0).position;
        prB = this.transform.GetChild(1).position;
        if(left)
            goLeft();
        else
            goRight();

    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position == destino)
        {//Cuando llego a mi destino, lo cambio
            if (destino == prA)
            {
                goLeft();
            }
            else
            {
                goRight();
            }
        }

        this.transform.position = Vector3.MoveTowards(this.transform.position, destino,
            velocity * Time.deltaTime);

    }

    private void goLeft()
    {
        sr.flipX = false;
        destino = prB;
    }

    private void goRight()
    {
        sr.flipX = true;
        destino = prA;
    }
}
