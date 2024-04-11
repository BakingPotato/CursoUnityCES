using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSquare : MonoBehaviour
{

    Vector3 OG_position;

    GameObject box_up = null;
    GameObject box_down = null;
    GameObject box_right = null;
    GameObject box_left = null;

    // layerMask Box
    int layerMask;
    RaycastHit hit;

    Rigidbody rb;

    GameManager_Chess GM;

    Coroutine falling = null;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 6;

        rb = GetComponent<Rigidbody>();
        OG_position = transform.position;

        GM = GameManager_Chess.Instance;
    }

    private void Update()
    {
        GetAdjacentSquares();
    }

    void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1, Color.red);
    }

    private void GetAdjacentSquares()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, layerMask))
        {
            box_up = hit.collider.gameObject;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1, layerMask))
        {
            box_down = hit.collider.gameObject;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1, layerMask))
        {
            box_right = hit.collider.gameObject;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1, layerMask))
        {
            box_left = hit.collider.gameObject;
        }
    }


    public void makeSquaresFall(Vector3 direction, bool first, float extraTime = 0)
    {
        if (!first)
        {
            if (falling == null)
                falling = StartCoroutine(fall(extraTime));
        }

        if (direction == Vector3.forward)
        {
            if (box_up)
            {
                box_up.GetComponent<ChessSquare>().makeSquaresFall(direction, false, extraTime + 0.2f);
            }
        }

        if(direction == Vector3.back)
        {
            if (box_down)
            {
                box_down.GetComponent<ChessSquare>().makeSquaresFall(direction, false, extraTime + 0.2f);
            }
        }

        if (direction == Vector3.right)
        {
            if (box_right)
            {
                box_right.GetComponent<ChessSquare>().makeSquaresFall(direction, false, extraTime + 0.2f);
            }
        }

        if (direction == Vector3.left)
        {
            if (box_left)
            {
                box_left.GetComponent<ChessSquare>().makeSquaresFall(direction, false, extraTime + 0.2f);
            }
        }
    }

    IEnumerator fall(float extraTime)
    {
        yield return new WaitForSeconds(GM.time_to_fall + extraTime);
        rb.useGravity = true;
        yield return new WaitForSeconds(GM.time_falling + extraTime);

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        transform.position = OG_position;

        falling = null;
    }




}
