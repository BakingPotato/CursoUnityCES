using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Vector3 direction = Vector3.up;
    public float speed = 5;

    public enum object_tags
    {
        Player,
        Enemy
    }

    public object_tags target;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);    
    }
}
