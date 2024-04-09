using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float seconds = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(autoDestroy());
    }

   IEnumerator autoDestroy()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
