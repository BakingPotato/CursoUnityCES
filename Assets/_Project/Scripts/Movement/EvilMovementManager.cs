using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMovementManager : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] float speed;
    public Vector3 direction = Vector3.zero;
    Vector3  previousPos = Vector3.zero;

    bool move = false;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(startMoving());
    }

    IEnumerator startMoving()
    {
        if(direction == Vector3.zero)
        {
            yield return new WaitForSeconds(0.9f);
            Vector3 RandomDir = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360));
            direction = RandomDir.normalized;
        }

        rb.AddForce(direction * speed, ForceMode.Force);

        previousPos = transform.position;

        move = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        {
            direction = (transform.position - previousPos).normalized;

            rb.velocity = speed * direction;

            previousPos = transform.position;
        }

    }
}
