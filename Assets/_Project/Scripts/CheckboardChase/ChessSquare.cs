using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSquare : MonoBehaviour
{

    Vector3 OG_position;

    public GameObject box_up = null;
    public GameObject box_down = null;
    public GameObject box_right = null;
    public GameObject box_left = null;

    // layerMask Box
    int layerMask;
    RaycastHit hit;

    Rigidbody rb;
    RigidbodyConstraints originalConstraints;

    GameManager_Chess GM;

    Coroutine falling = null;
    [HideInInspector] public bool terminated = false;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 6;

        rb = GetComponent<Rigidbody>();
        originalConstraints = rb.constraints;

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


    public void makeSquaresFall(Vector3 direction, Material color, bool first = false, float extraTime = 0)
    {
        if (!first)
        {
            if (falling == null)
                falling = StartCoroutine(fall(extraTime, color));
        }

        if (direction == Vector3.forward)
        {
            if (box_up)
            {
                box_up.GetComponent<ChessSquare>().makeSquaresFall(direction, color, false, extraTime + 0.2f);
            }
        }

        if(direction == Vector3.back)
        {
            if (box_down)
            {
                box_down.GetComponent<ChessSquare>().makeSquaresFall(direction, color, false, extraTime + 0.2f);
            }
        }
        if (direction == Vector3.right)
        {
            if (box_right)
            {
                box_right.GetComponent<ChessSquare>().makeSquaresFall(direction, color, false, extraTime + 0.2f);
            }
        }

        if (direction == Vector3.left)
        {
            if (box_left)
            {
                box_left.GetComponent<ChessSquare>().makeSquaresFall(direction, color, false, extraTime + 0.2f);
            }
        }
    }

    IEnumerator fall(float extraTime, Material color)
    {
        Material old = GetComponent<MeshRenderer>().material;
        yield return new WaitForSeconds(extraTime);
        GetComponent<MeshRenderer>().material = color;
        yield return new WaitForSeconds(GM.time_to_fall);
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.useGravity = true;
        yield return new WaitForSeconds(GM.time_falling + extraTime);

        if (!terminated)
        {
            rb.useGravity = false;
            rb.constraints = originalConstraints;
            rb.velocity = Vector3.zero;
            GetComponent<MeshRenderer>().material = old;
            transform.position = OG_position;

            falling = null;
        }
    }

    public void instantFall()
    {
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.useGravity = true;
        Destroy(this.gameObject, GM.time_falling);
    }
}
