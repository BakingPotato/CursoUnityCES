using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovDinamico : MonoBehaviour
{
    GameManager_Castle GM;

    //Es un mov. físico. Le aplican fuerzas, rozamiento, fricción...
    //Sobre el rigidbody - Añadir fuerzas
    public Rigidbody rbPlayer;
    Vector3 vMov;
    bool saltando = false;
    bool saltoFixU = false;

    public float speed = 70;
    public float jumpForce = 8;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager_Castle.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //inputMovement();

        inputJump();
        //El atributo más importante del rb es el velocity
        //Es un vector 3 donde se indica la velocidad dinámica acumulada
        //rbPlayer.velocity = Vector3.zero;
        //Equivalente en velocidad angular (de rotación)
        //rbPlayer.angularVelocity = Vector3.zero;
        //Otros atributos importantes
        //rbPlayer.drag
        //rbPlayer.angularDrag

    }//End Update

    void FixedUpdate()
    {//Igual que el update, pero prioriza fisicas

        jump();

        inputFixedMovement();
    }

    private void inputMovement()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirZ = Input.GetAxisRaw("Vertical");
        vMov.x = dirX;
        vMov.z = dirZ;
        rbPlayer.AddForce(vMov  * Time.deltaTime * speed);
    }

    private void inputFixedMovement()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirZ = Input.GetAxisRaw("Vertical");
        vMov.x = dirX;
        vMov.z = dirZ;

        if (Input.GetKey(KeyCode.Mouse0) && (!saltando))
            rbPlayer.velocity = new Vector3(0, rbPlayer.velocity.y, 0);
        else
            rbPlayer.AddForce(vMov  * speed);
    }

    private void inputJump()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (!saltando))
        {
            saltando = true;
            saltoFixU = true;
            //rbPlayer.AddForce(Vector3.up  * 6, ForceMode.Impulse);
        }
    }

    private void jump()
    {
        if (saltoFixU)
        {
            saltoFixU = false;//"Cerrar la puerta" - Solo un frame entra y niega la condición
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //Siempre una colisión es entre dos objetos
        //Uno es el otro - other
        //El segundo es el que lleva el código, this
        //Debug.LogWarning(other.gameObject.name);
        //Lo habitual es primero consultar con quien chocamos
        if (other.gameObject.CompareTag("Suelo"))
        {
            Debug.Log("COLISION!");
            saltando = false;
        }

    }
    private void OnCollisionExit(Collision other)
    {

    }
    private void OnCollisionStay(Collision other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Muerte"))
        {
            Debug.Log("DEATH!");
            GM.respawn();
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
    private void OnTriggerStay(Collider other)
    {

    }

}// End Class
