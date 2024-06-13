using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilChaserMovement : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Rigidbody _rb;


    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        //Si el jugador esta presente se le persigue
        if (target)
        {
            pursuePlayer();
        }
        else //En caso contrario flotamos por ahí
        {
            _rb.velocity = _rb.velocity.normalized * (speed);
        }
    }

    private void Update()
    {
        searchForPlayer();
    }

    private void searchForPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else target = null;
    }

    private void pursuePlayer()
    {
        //La distancia del objeto al objetivo
        Vector3 direction = target.position - _rb.position;

        //Normaliza para que la dirección este en 1
        _rb.velocity = direction * speed;
    }
}
