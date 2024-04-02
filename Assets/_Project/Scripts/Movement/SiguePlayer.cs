using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiguePlayer : MonoBehaviour
{
    public GameObject player;
    Vector3 distancia;
    // Start is called before the first frame update
    void Start()
    {
        distancia = player.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position - distancia;
    }
}
