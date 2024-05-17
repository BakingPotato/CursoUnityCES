using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoCamara : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        //Physics.Raycast
        cam = this.GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(rayo.origin, rayo.direction * 20, Color.red, 0.2f);
            if (Physics.Raycast(rayo, out hit, 20))
            {
                Debug.LogWarning("Impacto con " + hit.transform.name);
            }
        }

    }
}
