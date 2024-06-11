using System.Collections;
using UnityEngine;

public class RotatingCoins : MonoBehaviour
{
    public float speed = 10f;
    public float moveSpeed = 10f;
    public Vector3 direction = new Vector3(0, 1, 0);
    public bool moving = false;
    public Vector3 destinationA = Vector3.zero;
    public Vector3 destinationB = Vector3.zero;

    private void Start()
    {
        if (moving)
        {
            StartCoroutine(moveTowards());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * direction * Time.deltaTime);
    }

    IEnumerator moveTowards()
    {
        while (true)
        {
            while(Vector3.Distance(transform.position, destinationA) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinationA, moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            while (Vector3.Distance(transform.position, destinationB) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinationB, moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
