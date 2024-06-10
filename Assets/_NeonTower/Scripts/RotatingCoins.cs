using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCoins : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction = new Vector3(0, 1, 0);


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * direction * Time.deltaTime);
    }
}
