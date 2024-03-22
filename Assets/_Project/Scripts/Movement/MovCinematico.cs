using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCinematico : MonoBehaviour
{
    public GameObject player;

    public Vector3 v3;

    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ejeX = Input.GetAxisRaw("Horizontal");
        float ejeZ = Input.GetAxisRaw("Vertical");

        Vector3 v3aux = Vector3.zero;
        v3aux.x = ejeX;
        v3aux.z = ejeZ;

        this.transform.Translate(v3aux * speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(player.transform.forward * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(player.transform.forward * -speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(player.transform.right * -speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(player.transform.right * -speed * Time.deltaTime);
        }

    }
}
