using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public Transform pies;
    public LayerMask suelo;
    public LayerMask escalera;

    public float velZorro = 8;
    public float jumpForce = 15;
    public int nCerezas = 0;

    float h, v;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (anim.GetBool("Die"))
        {
            h = 0;
            v = 0;
        }

        if (h < 0) sr.flipX = true;
        else if (h > 0) sr.flipX = false;

        anim.SetInteger("Vel", (int)h);

        //Dar velocidad
        //rb.velocity = new Vector2(h * velZorro, rb.velocity.y);

        if ((Input.GetKeyDown(KeyCode.Space)) && (TocaSuelo()))
        {
            if (!TocaEscalera())
            {
                anim.SetTrigger("JumpStart");
                Invoke("PonSalto", 0.5f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

        }
        //Mirar tocar suelo solo cuando caemos
        if (rb.velocity.y < 0) //Cuando la velocidad en Y sea negativa, estamos cayendo
        {
            if (!anim.GetBool("Jump"))
            {   //Si mi animator no sabe que estoy saltando al caer 
                anim.SetBool("Jump", true);
            }
            //anim.SetBool("Jump", !TocaSuelo()); //<<-- Tampoco va

            //if (TocaSuelo()) //<<<- Consultar, a veces da falso cuando no debería
            //{ //Cuando va con mucha velocidad da falso
            //    anim.SetBool("Jump", false);
            //}
        }

        //Si estoy saltando y toco suelo, quito el salto
        if ((anim.GetBool("Jump")) && (TocaSuelo())) anim.SetBool("Jump", false);

        //-------------------- Crouch -------
        if ((Input.GetKeyDown(KeyCode.S)) ||
            (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            if (!anim.GetBool("Climb"))
            {
                CapsuleCollider2D cc = this.GetComponent<CapsuleCollider2D>();
                cc.size = new Vector2(cc.size.x, 0.8f);
                velZorro = 4;
                anim.SetBool("Crouch", true);
            }

        }
        else if ((Input.GetKeyUp(KeyCode.S)) ||
          (Input.GetKeyUp(KeyCode.DownArrow)))
        {
            CapsuleCollider2D cc = this.GetComponent<CapsuleCollider2D>();
            cc.size = new Vector2(cc.size.x, 1.02f);
            velZorro = 8;
            anim.SetBool("Crouch", false);
        }

        //---------------------------------
        if /*(*/(TocaEscalera())//&&(!anim.GetBool("Jump")))
        {
            //Solo CLIMB cuando pulsemos en V
            rb.gravityScale = 0;
            anim.SetBool("Climb", true);
            if (v != 0)
            {//Si pulso arriba o abajo
                anim.speed = 1; //Se mueve la animación
                rb.velocity = new Vector2(h, v) * velZorro; //inputs en 4 direcciones
            }
            else
            {//Si no pulso arriba o abajo
                anim.speed = 0; //Velocidad de la animación a 0 (quieta la animación)
                rb.velocity = new Vector2(h * velZorro, 0); //puedo salir de la escalera
            }
        }
        else
        {
            anim.SetBool("Climb", false);
            anim.speed = 1;
            rb.gravityScale = 4;
            rb.velocity = new Vector2(h * velZorro, rb.velocity.y);
        }



    }//End update
    //-----------------------------------------------------
    bool TocaSuelo()
    {
        Collider2D col = Physics2D.OverlapBox(pies.position,
            Vector2.one * 0.4f, 0, suelo.value);
        //Lo mismo que hacer
        //if (col == null) return false;
        //else return true;
        //if (col) return true;
        return col;
    }
    //-----------------------------------------------------
    bool TocaEscalera()
    {
        Collider2D col = Physics2D.OverlapBox(this.transform.position,
            Vector2.one * 0.2f, 0, escalera.value);

        return col;
    }
    //------------------------------------------------
    void PonSalto()
    {
        anim.SetBool("Jump", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Foe"))
        {
            if (!anim.GetBool("Die"))
            {
                anim.SetBool("Die", true);
                rb.velocity = Vector2.zero;
                Camera.main.transform.parent = null; //Quitamos a la camara el que sea hijo del player
                StartCoroutine(Muerte());
            }
        }
        else if (other.CompareTag("PickUp"))
        {
            nCerezas++;
            Destroy(other.gameObject);
        }
    }

    IEnumerator Muerte()
    {
        rb.gravityScale = 0;

        CapsuleCollider2D cc = this.GetComponent<CapsuleCollider2D>();
        cc.isTrigger = true;
        Vector3 destino = this.transform.position;
        destino.y += 1;
        while (destino != this.transform.position)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, destino,
                Time.deltaTime * 3f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = 1;
    }



}
