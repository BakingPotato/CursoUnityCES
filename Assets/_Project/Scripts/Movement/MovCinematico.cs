using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCinematico : MonoBehaviour
{
    //Mov cinematico, sin físicas (titiritero) - Se aplica siempre
    //sobre la transform
    public GameObject player;//Null

    public Vector3 vMov;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Primero identificar el objeto al que vamos a aplicar movimiento
        //player.transform.Translate((Vector3.right + Vector3.forward) * 3 * Time.deltaTime);

        //Inputs de usuario
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirZ = Input.GetAxisRaw("Vertical");
        vMov.x = dirX;
        vMov.z = dirZ;
        player.transform.Translate(vMov * 3 * Time.deltaTime);

        //if (Input.GetButtonDown("Fire1"))
        //{

        //}
        //if (Input.GetMouseButtonUp(0))//Los botones de Mouse están mapeados con enteros
        //{//0 clic izq, 1, clic der, 2, clic de la bola o central, 3 lateral izq, 4 lateral der...

        //}
        //if (Input.GetKey(KeyCode.Escape))
        //{

        //}

        //3 tipos de gets y 3 subtipos por cada tipo
        //Botones (mandos), teclas (teclado), ratón - Hay un cuarto tipo touch
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    player.transform.Translate(player.transform.forward * 1); //* Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    player.transform.Translate(player.transform.forward * -3 * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.RightArrow)||(Input.GetKey(KeyCode.D)))
        //{
        //    player.transform.Translate(player.transform.right * 3 * Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    player.transform.Translate(player.transform.right * -3 * Time.deltaTime);
        //}

        //Input.GetKey //Mientras se pulsa - Mientras pulsas la tecla, lo que hay
        //dentro se ejecuta una vez por frame
        //Input.GetKeyDown //Se ejecuta sólo una vez
        //En el frame en el que comienza la pulsación
        //Input.GetKeyUp //Una sóla vez en el primer frame que dejas de pulsar


        //El atributo más importante del rb es el velocity 
        //

    }
}
