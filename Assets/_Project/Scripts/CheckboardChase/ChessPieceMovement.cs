using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMovement : MonoBehaviour
{
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


    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 6;
        GM = GameManager_Chess.Instance;

        StartCoroutine(checkAttack());
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
            dirX = Input.GetAxisRaw("Horizontal");
            dirZ = Input.GetAxisRaw("Vertical");

            if(dirX != 0 || dirZ != 0)
            {
                vMov.x = dirX;
                vMov.z = dirZ;

                transform.rotation = Quaternion.LookRotation(vMov);
                transform.Translate(vMov.normalized * speed * Time.deltaTime, Space.World);
            }

        }

    }

    IEnumerator checkAttack()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (standing_square)
                {
                    canMove = false;
                    standing_square.makeSquaresFall(transform.forward, true);
                    yield return new WaitForSeconds(GM.cooldown);
                    canMove = true;
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
    }
}
