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
    public bool terminated = false;
    public bool blocked = true;
    public bool limit = false;

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

    public bool isFalling() { return falling != null; }


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


    public void makeSquaresFall(Vector3 direction, Material color, ChessSquare first, bool isFirst = false, float extraTime = 0)
    {

        ChessSquare next_box = null;
        ChessSquare next_nextbox = null;
        ChessSquare first_box = first;

        if (direction == Vector3.forward)
        {
            if (box_up)
            {
                next_box = box_up.GetComponent<ChessSquare>();
                if (next_box.box_up)
                    next_nextbox = next_box.box_up.GetComponent<ChessSquare>();
            }
        }

        if(direction == Vector3.back)
        {
            if (box_down)
            {
                next_box = box_down.GetComponent<ChessSquare>();
                if (next_box.box_down)
                    next_nextbox = next_box.box_down.GetComponent<ChessSquare>();
            }
        }

        if (direction == Vector3.right)
        {
            if (box_right)
            {
                next_box = box_right.GetComponent<ChessSquare>();
                if (next_box.box_right)
                    next_nextbox = next_box.box_right.GetComponent<ChessSquare>();
            }
        }


        if (direction == Vector3.left)
        {
            if (box_left)
            {
                next_box = box_left.GetComponent<ChessSquare>();
                if (next_box.box_left)
                    next_nextbox = next_box.box_left.GetComponent<ChessSquare>();
            }
        }

        //Si hay siguiente
        if (next_box)
        {
            if(!next_box.isFalling() && !next_box.terminated)
            {
                checkNextBox(direction, color, first, isFirst, extraTime, next_box);
            }
            else if(next_nextbox)
            {
                checkNextBox(direction, color, first, isFirst, extraTime, next_nextbox);
            }
        }
        else if(!isFirst)
        {
            if (!isFalling())
                falling = StartCoroutine(fall(extraTime, color, null));
            first_box.blocked = false;
        }
    }

    private void checkNextBox(Vector3 direction, Material color, ChessSquare first, bool isFirst, float extraTime, ChessSquare next_box)
    {
        ChessSquare first_box;
        //Si no es el primero empezamos la rutina de caer y pasamos el siguiente respetando el first que nos paso el último
        if (!isFirst)
        {
            if (!isFalling())
                falling = StartCoroutine(fall(extraTime, color, next_box));

            first_box = first;
        }
        else
        {
            first_box = next_box;
        }

        if (!next_box.isFalling() || !next_box.terminated)
        {
            if (isFirst && (first.isFalling() || first.terminated))
            {
                next_box.makeSquaresFall(direction, color, first_box, true, extraTime + 0.2f);
            }
            else
            {
                next_box.makeSquaresFall(direction, color, first_box, false, extraTime + 0.2f);
            }

        }
        else
        {
            first_box.blocked = false;
        }
    }

    IEnumerator fall(float extraTime, Material color, ChessSquare next)
    {
        Material old = GetComponent<MeshRenderer>().material;
        yield return new WaitForSeconds(extraTime);
        GetComponent<MeshRenderer>().material = color;

        while (blocked)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);
        if(next)
            next.blocked = false;

        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.useGravity = true;

        extraTime = Mathf.Max(0, extraTime - 0.15f);
        yield return new WaitForSeconds(GM.time_falling + extraTime);

        //Si no ha sido eliminado lo colocamos en su posición inicial
        if (!terminated)
        {
            blocked = true;
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
        Destroy(this.gameObject, GM.time_falling+3);
    }
}
