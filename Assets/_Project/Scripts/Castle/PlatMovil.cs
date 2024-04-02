using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMovil : MonoBehaviour
{
    public bool horizontal = true;
    public Vector2 limits = new Vector2(-3.0f, 3.0f) ;

    public float limit_izq = -3;
    public float limit_der = 3;

    public float speed = 5 ;

    private bool goRight = false;
    private bool goUp = false;

    // Update is called once per frame
    void Update()
    {
        if (horizontal)
        {
            HorizontalMovement();
        }
        else
        {
            VerticalMovement();
        }
    }

    private void HorizontalMovement()
    {
        if (goRight)
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (this.transform.position.x < limits[0])
        {
            goRight = true;
        }
        if (this.transform.position.x > limits[1])//Si me paso del borde derecho
        {
            goRight = false;
        }
    }

    private void VerticalMovement()
    {
        if (goUp)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        if (this.transform.position.y < limits[0])
        {
            goUp = true;
        }
        if (this.transform.position.y > limits[1])//Si me paso del borde derecho
        {
            goUp = false;
        }
    }
}
