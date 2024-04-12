using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    Vector3 moveWarp;
    Vector3 viewportPos;

    private void Update()
    {
        warpPlayer();
    }

    private void warpPlayer()
    {
        // Convertimos nuestra posición con respecto a la camara a un vector. Este vector estara en el rango de 0 a 1.
        //Esto significa que x ira de 1 a 0 (derecha a izquierda) y de 1 a 0 (arriba a abajo)
        viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        // Si nos hemos movido fuera de la camara
        moveWarp = Vector3.zero;

        //En caso de salir por la izquierda
        if (viewportPos.x < 0)
        {
            moveWarp.x += 1;
        }
        //En caso de salir por la derecha
        else if (viewportPos.x > 1)
        {
            moveWarp.x -= 1;
        }
        //En caso de salir por abajo
        else if (viewportPos.y < 0)
        {
            moveWarp.y += 1;
        }
        //En caso de salir por arriba
        else if (viewportPos.y > 1)
        {
            moveWarp.y -= 1;
        }

        // Convertimos las coordenadas de vuelta para mover al jugador
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos + moveWarp);
    }
}
