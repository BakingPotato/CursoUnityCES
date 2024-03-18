using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMovementManager : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] float speed;
    Vector3  direction;
    Vector3  previousPos;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 RandomDir = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360));
        direction = RandomDir.normalized;
        rb.AddForce(direction * speed, ForceMode.Force);

        previousPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = (transform.position - previousPos).normalized;
        
        rb.velocity = speed * direction;

        previousPos = transform.position;
    }
}
