using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Palas : MonoBehaviour
{
    GameObject pala;
    float halfW = Screen.width / 4;
    float halfH = Screen.height / 4;
    public float escalaTiempo = 1;
    float velAng = 90;
    public int nToques = 0;
    public int bolasOut = 1;

    public static GameManager_Palas gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pala = GameObject.Find("Pala");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = pala.transform.position;
        newPos.x = (Input.mousePosition.x - halfW) / halfW;
        newPos.y = (Input.mousePosition.y - halfH) / halfH;

        //3º tipo de "movimiento" - Mini teleport
        pala.transform.position = newPos;

        //Clase time, útil para crear dos tipos de cosas: pausa y tiempo bala
        Time.timeScale = escalaTiempo;
        //Habilitar grindeo / farmeo >>> + de valor 1
        Input.GetAxisRaw("Horizontal");
        //Efecto de inclinación
        float tiltX = Input.GetAxis("Mouse X") * velAng * 2;//Der y izq se lo damos a Z
        float tiltZ = Input.GetAxis("Mouse Y") * velAng * (-2);//Subir y bajar se lo damos a X

        pala.transform.rotation = Quaternion.Slerp(pala.transform.rotation,
                                    Quaternion.Euler(tiltZ, 0, tiltX), 
                                    Time.deltaTime * 5);

    }
}
