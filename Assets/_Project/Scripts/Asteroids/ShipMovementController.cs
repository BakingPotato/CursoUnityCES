using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementController : MonoBehaviour
{

    Rigidbody2D rb;
    RigidbodyConstraints2D originalConstraints;

    //Parametros
    public float mainThrust = 2.5f;
    public float rotationThrust = 5;
    public float maxVelocity = 5;
    public float thrustingStopForce = 0.01f;
    public float stoppingForce = 0.2f;

    //Cache
    bool isAccelerating = false;
    bool rotatingRight = false;
    bool rotatingLeft = false;
    //Estado


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalConstraints = rb.constraints;

    }

    // Update is called once per frame
    void Update()
    {
        checkShipAcceleration();
        checkShipRotation();
        ProcessStop();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void checkShipAcceleration()
    {
        isAccelerating = Input.GetKey(KeyCode.Mouse0);
    }

    private void checkShipRotation()
    {
        rotatingRight = Input.GetKey(KeyCode.D);
        rotatingLeft = Input.GetKey(KeyCode.A);
    }


    private void ProcessThrust()
    {
        if (isAccelerating)
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void ProcessStop()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            StartStopping();
        }
        else if (!Input.GetKey(KeyCode.Mouse0))
        {
            StopStopping();
        }

    }

    private void StartThrusting()
    {
        rb.constraints = originalConstraints;
        //addRelative añade fuerza dependiendo del sistema de coordenadas del objeto
        rb.AddRelativeForce(Vector3.up * mainThrust);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

    }

    private void StartStopping()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0,0), stoppingForce);

        //rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | originalConstraints;
    }

    private void StopThrusting()
    {
        if(rb.gravityScale == 0)
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, 0), thrustingStopForce);
    }

    private void StopStopping()
    {

        rb.constraints = originalConstraints;
    }


    //Rotation
    void ProcessRotation()
    {
        if (rotatingLeft)
        {
            RotateLeft();
        }
        else if (rotatingRight)
        {
            RotateRight();
        }
    }


    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
    }


    private void ApplyRotation(float direction)
    {
        rb.freezeRotation = true; // Congela la rotación para que podamos rotar manualmente
        transform.Rotate(Vector3.forward * direction);
        // Descongela las rotaxiones x e y asi como la posicion z 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}
