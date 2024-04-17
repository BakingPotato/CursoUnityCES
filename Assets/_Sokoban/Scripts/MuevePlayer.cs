using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuevePlayer : MonoBehaviour {

    Rigidbody rb;
    RigidbodyConstraints originalConstraints;

    [Header("Movement")]
    public float speed = 5;
    float dirX, dirY;
    Vector3 vMov;
    bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputMovement();
    }

    private void inputMovement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        //eSTE IF LE DA MÁS FLUIDEZ AL MOVIMIENTO
        if (dirX > 0 || dirX < 0)
        {
            dirY = 0;
        }
        else
        {
            dirY = Input.GetAxisRaw("Vertical");
        }
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        if (canMove)
        {
            //Esto limita las diagonales
            if (((dirX > 0 || dirX < 0) && dirY == 0) || ((dirY > 0 || dirY < 0) && dirX == 0))
            {
                vMov.x = dirX;
                vMov.y = dirY;

                transform.rotation = Quaternion.LookRotation(transform.forward, vMov);
                transform.Translate(vMov.normalized * speed * Time.deltaTime, Space.World);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }
    //-------------------------------------------------------------
}
