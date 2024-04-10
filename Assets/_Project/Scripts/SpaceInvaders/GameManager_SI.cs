using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager_SI : MonoBehaviour
{
    //El gameManager gestiona todos los aspectos del juego
    //Normalmente es el que configura la escena de inicio
    //Conviene convertirlo en instancia �nica, y quiz�s persistente
    public static GameManager_SI instance;


    [Header("Enemy")]
    public GameObject[] enemies = new GameObject[2];

    public Transform initial_enemy_position;
    Transform actual_position;

    [HideInInspector]public bool moveRight = true;

    public GameObject alien_pea;
    GameObject enemy_parent;

    [Header("Balance")]
    public float enemy_speed = 0.2f;
    float actual_enemy_speed;
    public float enemy_speed_upgrade = 0.05f;
    public float enemy_shoot_cooldown = 1f;

    [Header("UI")]
    //Texto de puntuación
    public TextMeshProUGUI scoreText;
    //Texto de puntuación alta
    public TextMeshProUGUI hiScoreText;

    float score = 0;
    float hiScore = 0;

    Coroutine moveADown = null;

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hiScore = ((int)PlayerPrefs.GetFloat("HiScore", 0));

        hiScoreText.text = hiScore.ToString();

        enemy_parent = initial_enemy_position.transform.parent.gameObject;

        StartGame();
        StartCoroutine(checkGame());
        StartCoroutine(alienShoot());
    }

    private void StartGame()
    {
        enemy_parent.transform.position = Vector3.zero;
        actual_enemy_speed = enemy_speed + 0.75f;
        actual_position = initial_enemy_position;
        spawnAliens();
    }


    private void spawnAliens()
    {
        GameObject actual_enemy;
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 11; i++)
            {
                actual_enemy = Instantiate(enemies[i % 2], enemy_parent.transform);
                actual_enemy.transform.position = actual_position.transform.position;
                actual_position.position += new Vector3(0.5f, 0, 0);
            }
            actual_position.position += new Vector3(-0.5f * 11, -0.5f, 0);
        }
        actual_position.position += new Vector3(0, 0.5f * 5, 0);
    }

    public void moveAliensDown()
    {
        if(moveADown == null)
        {
            moveADown = StartCoroutine(moveDown());
        }
    }

    IEnumerator moveDown()
    {
        actual_enemy_speed += enemy_speed_upgrade;
        moveRight = !moveRight;
        enemy_parent.transform.position += new Vector3(0, -0.2f, 0);
        Debug.Log("MoveAliensDown");

        yield return new WaitForSeconds(0.1f);

        moveADown = null;
    }

    public void ResetGame()
    {
        if(score > PlayerPrefs.GetFloat("HiScore", 0))
        {
            PlayerPrefs.SetFloat("HiScore", score);
        }
        SceneManager.LoadScene("SpaceInvaders");
    }

    public void updateScore(float points)
    {
        score += points;
        if(score > hiScore)
        {
            hiScore = score;
            hiScoreText.text = hiScore.ToString();
        }
        scoreText.text = score.ToString();
    }

    IEnumerator alienShoot()
    {
        Transform actual_enemy;
        while (true)
        {
            if (enemy_parent.transform.childCount > 1)
            {
                actual_enemy = enemy_parent.transform.GetChild(Random.Range(1, enemy_parent.transform.childCount));
                Instantiate(alien_pea, actual_enemy.position, Quaternion.identity);
                yield return new WaitForSeconds(enemy_shoot_cooldown);
            }
        }

    }

    IEnumerator checkGame()
    {
        while (true)
        {
            while (enemy_parent.transform.childCount > 1)
            {
                yield return new WaitForEndOfFrame();
            }

            updateScore(100);
            StartGame();

        }

    }

}
