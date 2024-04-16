using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HealthManager))]
public class ChessPieceMovement : MonoBehaviour
{

    Rigidbody rb;
    RigidbodyConstraints originalConstraints;

    [Header("Actual Standing Square")]
    ChessSquare standing_square;
    RaycastHit hit;
    int layerMask;

    [Header("Movement")]
    public float speed = 5;
    float dirX, dirZ;
    Vector3 vMov;
    bool canMove = true;
    GameManager_Chess GM;

    [Header("Health")]
    HealthManager hp;

    [Header("Player")]
    public int number = 1;
    public Material color;



    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 6;
        GM = GameManager_Chess.Instance;

        rb = GetComponent<Rigidbody>();
        originalConstraints = rb.constraints;

        hp = GetComponent<HealthManager>();

        StartCoroutine(checkAttack());
        StartCoroutine(checkForFall());
    }

    // Update is called once per frame
    void Update()
    {
        inputMovement();
    }

    private void inputMovement()
    {
        if (canMove)
        {
            //Inputs de usuario
            if(number == 1)
            {
                dirX = Input.GetAxisRaw("Horizontal");
                dirZ = Input.GetAxisRaw("Vertical");
            }
            else
            {
                dirX = Input.GetAxisRaw("Horizontal2");
                dirZ = Input.GetAxisRaw("Vertical2");
            }


            if(dirX != 0 || dirZ != 0)
            {
                vMov.x = dirX;
                vMov.z = dirZ;

                transform.rotation = Quaternion.LookRotation(vMov);
                transform.Translate(vMov.normalized * speed * Time.deltaTime, Space.World);
            }

        }

    }

    IEnumerator checkForFall()
    {
        while (hp.getHealth() > 0)
        {
            if (!standing_square)
            {
                rb.useGravity = true;
                yield return new WaitForSeconds(1);
                if (!standing_square)
                {
                    canMove = false;
                    rb.constraints &= ~RigidbodyConstraints.FreezePositionY;

                    yield return new WaitForSeconds(GM.time_falling-1);

                    hp.takeDamage(1);

                    if (hp.getHealth() > 0)
                    {
                        rb.constraints = originalConstraints;
                        rb.velocity = Vector3.zero;
                        transform.position = new Vector3(0, 1.043f, 0);
                        canMove = true;
                        rb.useGravity = false;
                        yield return new WaitForSeconds(GM.cooldown_after_falling); //cooldown
                        rb.useGravity = true;
                    }
                }
            }
            yield return new WaitForEndOfFrame();

        }

    }

    IEnumerator checkAttack()
    {
        while (true)
        {
            if (number == 1)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (standing_square)
                    {
                        Material old = GetComponent<MeshRenderer>().material;
                        GetComponent<MeshRenderer>().material = color;
                        canMove = false;
                        standing_square.makeSquaresFall(transform.forward, color, true);
                        yield return new WaitForSeconds(GM.cooldown);
                        canMove = true;
                        GetComponent<MeshRenderer>().material = old;
                    }
                }
            }
            else
            {
                if (Input.GetButton("Fire2"))
                {
                    if (standing_square)
                    {
                        Material old = GetComponent<MeshRenderer>().material;
                        GetComponent<MeshRenderer>().material = color;
                        canMove = false;
                        standing_square.makeSquaresFall(transform.forward, color, true);
                        yield return new WaitForSeconds(GM.cooldown);
                        canMove = true;
                        GetComponent<MeshRenderer>().material = old;
                    }
                }
            }


            yield return new WaitForEndOfFrame();
        }

    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1, layerMask))
        {
            standing_square = hit.collider.gameObject.GetComponent<ChessSquare>();
        }
        else
        {
            standing_square = null;
        }
    }
}
