using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaBolas : MonoBehaviour
{
    public int maxBolas = 5;
    
    public float secsToThrow = 3f;
    public GameObject bola;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Temporizador
        if (secsToThrow > 0)
        {
            secsToThrow -= Time.deltaTime;
        } else
        {
            secsToThrow = 3f;
            if (GameManager_Palas.gm.bolasOut < maxBolas)
            {
                Instantiate(bola, new Vector3(Random.Range(-3.5f, 3.5f), 2.8f, 0),
                Quaternion.identity);
                GameManager_Palas.gm.bolasOut++;
            }
            
        }
    }
}
