using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartasGame : MonoBehaviour
{
    public SimulaBD bd;
    public Image modoJuego;

    public List<Carta> mano = new List<Carta>();

    private bool espera = true;

    //---------------------------------------------------
    //Tarea 1º Parte
    public enum Modo
    {
        normal,
        overkill,
        tanque
    }
    //Pon aquí tu struct con sus constructores de carta
    public struct Carta
    {
        //Variable declaration
        public int mana;
        public int vida;
        public int ataque;
        public Modo modo;


        //Constructor (not necessary, but helpful)
        public Carta(int mana, int ataque, int vida, Modo modo)
        {
            this.mana = Mathf.Clamp(mana, 1, 5);
            this.ataque = Mathf.Clamp(ataque, 1, 15);
            this.vida = Mathf.Clamp(vida, 1, 10);
            this.modo = modo;

            switch (this.modo)
            {
                case Modo.overkill:
                    if (this.vida != 1)
                    {
                        this.vida = 1;
                        this.ataque *= 2;
                    }
                    break;

                case Modo.tanque:
                    this.vida = 12;
                    this.mana += 4;
                    this.ataque = Mathf.Max(this.ataque - 6, 0);
                    break;

            }

        }
    }

    //----------------------------------------------------


    //---------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        bd.SacarCarta();
    }
    //---------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bd.SacarCarta();
        }
    }
    //---------------------------------------------------------------------
    public void ClickEnBoton(int idBoton)
    { //Tarea 2º parte
        //Usar estas variables que es la INFO obtenida de la base de datos
        int magic = bd.ConsultaMana();
        int life = bd.ConsultaVida();
        int attk = bd.ConsultaAtaque();
        Carta carta_actual; 
       
        switch (idBoton)
        {
            case 1:
            default:
                //llama al Constructor normal
                carta_actual = new Carta(magic, attk, life, Modo.normal);
                modoJuego.color = Color.black;
                break;
            case 2:
                //llama al Constructor Overkill
                carta_actual = new Carta(magic, attk, life, Modo.overkill);

                modoJuego.color = Color.red;
                break;
            case 3:
                //llama al Constructor Tanque
                carta_actual = new Carta(magic, attk, life, Modo.tanque);

                modoJuego.color = Color.yellow;
                break;
        }
        //2º parte: Ejecuta la línea 66 con los valores
        //de tu struct (cambia el (0,0,0))
        bd.PintarEnUI(carta_actual.mana, carta_actual.vida, carta_actual.ataque);

        //Usa esto /\ para pintar los stats de la carta

        //3º OPCIONAL - parte - guarda tu carta en una lista "mano" (hasta 4, vas descartando)
        GuardaCartasEnMano(carta_actual);
        //Y completar GuardaCartasEnMano()
    }
    //----------------------------------


    void GuardaCartasEnMano(Carta carta_actual)
    {
        if(mano.Count > 4)
        {
            mano.RemoveAt(0);
            mano.Add(carta_actual);
        }
    }
    //4º parte - usa las teclas 1 al 4 para mostrar tu carta en mano. Da igual
    //la imagen
    void MuestraCartas()
    {

    }

    void ObtieneCartas()
    {

    }


}
