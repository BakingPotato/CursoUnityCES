using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MueveHielo : MonoBehaviour {

    bool bloqueo = false;

    public float speed = 6;
    public float detection = 0.75f;
    int layerMask = 1 << 3;

    Coroutine sliding = null;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && sliding == null)
            slidingSequence(collision);
    }

    private void slidingSequence(Collision collision)
    {
        Debug.Log("Colisiona con jugador");

        Vector3 dir = Vector3.zero;

        //Sacamos la distancia de X y de Y del jugador respecto al hielo
        float xDistance = transform.position.x - collision.collider.transform.position.x;
        float yDistance = transform.position.y - collision.collider.transform.position.y;

        //Si la distancia de X es mayor, significa que el jugador esta a la dereha o a la izquierda
        //Absoluto nos permite tener el número sin signo - en caso de que lo tenga
        if(Mathf.Abs(xDistance) > Mathf.Abs(yDistance))
        {
            //El jugador esta a la derecha con lo que nos movemos a la izquierda
            if (xDistance < 0)
                dir.x = -1;
            //El jugador esta a la izquierda con lo que nos movemos a la derecha
            else if (xDistance > 0)
                dir.x = 1;
        }
        else  //Si la distancia de Y es mayor, significa que el jugador esta arriba o abajo
        {
            //El jugador esta arriba con lo que nos movemos hacia abajo
            if (yDistance < 0)
                dir.y = -1;
            //El jugador esta abajo con lo que nos movemos hacia arriba
            else if (yDistance > 0)
                dir.y = 1;
        }

        sliding = StartCoroutine(deslizar(dir));
    }

    IEnumerator deslizar(Vector3 dir)
    {
        RaycastHit hit;

        //Comprobamos que no haya objetos del layer Pared delante de la dirección en la que nos vamos a mover
        if (!Physics.Raycast(transform.position, dir, out hit, detection, layerMask))
        {
            //Mientras no se detecte pared nos movemos
            while (!bloqueo)
            {
                Debug.DrawRay(transform.position, dir, Color.yellow);

                transform.Translate(dir * speed * Time.deltaTime);

                //Se detecta pared por lo que nos salimos del bucle
                if (Physics.Raycast(transform.position, dir, out hit, detection, layerMask))
                    bloqueo = true;

                yield return new WaitForEndOfFrame();
            }
        }

        
        bloqueo = false;
        sliding = null;
    }

}
