using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperaBola : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -5)
        {
            if (GameManager_Palas.gm.bolasOut == 1)
            {
                Debug.LogWarning("Has aguantado: " + GameManager_Palas.gm.nToques + " toques");
                GameManager_Palas.gm.nToques = 0; //Reinicio cuenta porque se ha caido la bola
            }

            GameManager_Palas.gm.bolasOut--;
            Destroy(this.gameObject);
            /*
            this.transform.position = new Vector3(Random.Range(-3.5f,3.5f),2.8f,0);
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            */
        }
    }
    //-------------------------------------------------
    private void OnCollisionEnter(Collision otro)
    {//This es la bola, other el otro con el que da, en este caso, solo la pala
        
        if (otro.gameObject.CompareTag("Player"))
            GameManager_Palas.gm.nToques++;
    }
}
