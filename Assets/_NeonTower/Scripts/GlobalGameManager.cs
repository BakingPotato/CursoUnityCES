using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameManager : MonoBehaviour
{
    public int actual_floor = 1;
    public int health = 3;
    public bool invincible = false;

    public float duration = 0.08f;
    bool _isFrozen;
    float _pendingFreezeDuration = 0f;

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

    public int take_health()
    {
        if (!invincible)
        {
            StartCoroutine(invencibility());
            health--;
        }
        return health;
    }

    public void nextFloor()
    {
        StartCoroutine(switchNextFloor());
    }

    IEnumerator switchNextFloor()
    {
        yield return new WaitForSeconds(1.5f);
        if (actual_floor < 21)
        {
            actual_floor++;
            SceneManager.LoadScene(actual_floor);
        }
        else
        {
            SceneManager.LoadScene("theTop");
        }
    }

    IEnumerator invencibility()
    {
        yield return StartCoroutine(DoFreeze());
        PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        pm.startBlinking();
        invincible = true;
        yield return new WaitForSeconds(1.5f);
        pm.stopBlinking();
        invincible = false;
    }

    IEnumerator DoFreeze()
    {
        var original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;

    }


}
