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


    public void makeSquaresFall(Vector3 direction, Material color, bool isFirst = false, int pos = 0, float extraTime = 0)
    {

        ChessSquare next_box = null;
        float time_to_fall = 0.0f;
        int distance = 0;

        if (extraTime == 0)
        {
            distance = calculateDistance(direction);
            time_to_fall = GM.time_to_fall[distance-1];
            Debug.Log(this.gameObject.name + ", distancia: " + distance);
        }
        else
        {
            time_to_fall = extraTime + GM.time_to_fall_increment;
        }

        if (direction == Vector3.forward)
        {
            if (box_up)
            {
                next_box = box_up.GetComponent<ChessSquare>();
            }
        }

        if (direction == Vector3.back)
        {
            if (box_down)
            {
                next_box = box_down.GetComponent<ChessSquare>();
            }
        }

        if (direction == Vector3.right)
        {
            if (box_right)
            {
                next_box = box_right.GetComponent<ChessSquare>();
            }
        }

        if (direction == Vector3.left)
        {
            if (box_left)
            {
                next_box = box_left.GetComponent<ChessSquare>();
            }
        }

        if (!isFirst)
        {
            if (!isFalling())
                falling = StartCoroutine(fall(time_to_fall, pos, color));
        }

        //Si hay siguiente
        if (next_box)
        {
            checkNextBox(direction, color, time_to_fall, next_box, pos);
        }
    }

    private int calculateDistance(Vector3 direction)
    {
        int count = 0;
        GameObject next = this.gameObject;
        while (next != null)
        {
            if (direction == Vector3.forward)
                next = next.GetComponent<ChessSquare>().box_up;

            if (direction == Vector3.back)
                next = next.GetComponent<ChessSquare>().box_down;

            if (direction == Vector3.right)
                next = next.GetComponent<ChessSquare>().box_right;

            if (direction == Vector3.left)
                next = next.GetComponent<ChessSquare>().box_left;

            count++;
        }

        return count;
    }

    private void checkNextBox(Vector3 direction, Material color, float extraTime, ChessSquare next_box, int pos)
    {
        if (!next_box.isFalling() || !next_box.terminated)
        {
            next_box.makeSquaresFall(direction, color, false, pos + 1, extraTime);
        }
    }

    IEnumerator fall(float time_to_fall, int pos, Material color)
    {
        Material old = GetComponent<MeshRenderer>().material;

        yield return new WaitForSeconds(GM.time_to_fall_increment * pos);
        GetComponent<MeshRenderer>().material = color;

        yield return new WaitForSeconds(time_to_fall);

        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.useGravity = true;

        yield return new WaitForSeconds(GM.time_falling);


        //Si no ha sido eliminado lo colocamos en su posición inicial
        if (!terminated)
        {
            rb.useGravity = false;
            falling = null;
            rb.constraints = originalConstraints;
            rb.velocity = Vector3.zero;
            GetComponent<MeshRenderer>().material = old;
            transform.position = OG_position;
        }
    }

    public void instantFall()
    {
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.useGravity = true;
        Destroy(this.gameObject, GM.time_falling+3);
    }
}
