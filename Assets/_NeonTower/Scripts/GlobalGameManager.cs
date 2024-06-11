using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameManager : MonoBehaviour
{
    public int actual_floor = 1;

    private static GlobalGameManager _instance;

    public static GlobalGameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Null GM");
            return _instance;
        }
    }

    private void Awake()
    {
        if (GlobalGameManager.Instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void nextFloor()
    {
        StartCoroutine(switchNextFloor());
    }

    IEnumerator switchNextFloor()
    {
        yield return new WaitForSeconds(2.5f);
        if (actual_floor < 10)
        {
            actual_floor++;
            SceneManager.LoadScene(actual_floor+1);
        }
        else
        {
            SceneManager.LoadScene("theTop");
        }
    }
}
