using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BingoManager : MonoBehaviour
{

    [Header("Tiempo")]
    [Tooltip("Tiempo de espera entre mostrar números")]
    public float waitTime = 6;
    bool stop = false;

    [Header("UI")]
    [Tooltip("Texto que muestra el número actual")]
    public TMP_Text last_number;

    [Tooltip("Texto que muestra los números anteriores")]
    public TMP_Text old_numbers;

    [Tooltip("Botón de pausa")]
    public GameObject pauseButton;

    [Tooltip("Botón de continuar")]
    public GameObject continueButton;

    [Tooltip("Botón de resetear el juego")]
    public GameObject exitButton;

    [Header("Números")]
    List<int> bingo_numbers = new List<int>();
    int [] bingo_numbers_array = new int[99];
    int actual_number;
    int array_limit;

    Coroutine sequence;

    // Start is called before the first frame update
    void Start()
    {
        initializeBingo();
    }

    //Inicializa el juego
    public void initializeBingo()
    {
        old_numbers.text = "";
        last_number.text = "";

        //Rellenamos la lista bingo con 99 números del 1 al 99
        fillBingoList();

        RandomizeArray(bingo_numbers_array);

        sequence = StartCoroutine(bingoSequence());
    }

    //Rellena la lista y el array de números
    private void fillBingoList()
    {
        //Añade los números del 1 al 99 a la lista
        for (int i = 1; i < 100; i++)
        {
            bingo_numbers.Add(i);
        }

        //Recorre el array introduciendo números, siendo i el número. Como i empieza en 0, le sumaremos 1 para meter números desde el 1 al 99
        for (int i = 0; i < 99; i++)
        {
            bingo_numbers_array[i] = i + 1;
        }

        //Asignamos 98 al limite del array
        //Esto nos sirve para la versión array del código
        array_limit = 98;
    }

    //Secuencia del juego
    IEnumerator bingoSequence()
    {
        //Sacamos un número

        //popBingoNumber();
        popBingoNumber_Array();

        //Mientras la lista no este vacia, repetimos el bucle
        //while (bingo_numbers.Count > 0)

        //Mientras array_limit no sea 0, repetimos el bucle
        while (array_limit > 0)
        {
            ///Espera el tiempo dado antes de seguir
            yield return new WaitForSeconds(waitTime);

            //Si se paro la partida, esperamos para que continue, que sera si stop es igual a false
            yield return new WaitUntil(() => {
                return stop == false;
            });

            updateOldNumbers();
            //popBingoNumber();
            popBingoNumber_Array();
        }
    }

    //Saca un número al azar de la lista, lo muestra por pantalla y lo elimina
    private void popBingoNumber()
    {
        //Sacamos el núevo número y lo borramos de la lista
        int random = UnityEngine.Random.Range(0, bingo_numbers.Count);
        actual_number = bingo_numbers[random];
        last_number.text = actual_number.ToString();
        bingo_numbers.RemoveAt(random);
    }

    private void popBingoNumber_Array()
    {
        actual_number =  bingo_numbers_array[array_limit];
        array_limit--;
        last_number.text = actual_number.ToString();
    }

    //Actualiza la lista de números viejos y los muestra por pantalla
    private void updateOldNumbers()
    {
        string new_old_numbers; 
        if (old_numbers.text == "")
            new_old_numbers = actual_number.ToString();
        else
            new_old_numbers =  actual_number.ToString() + " - " + old_numbers.text;

        //Actualizamos la lista de números sacados
        old_numbers.text = new_old_numbers;
    }

    public void RandomizeArray(int[] array)
    {
        System.Random rand = new System.Random();

        // For each spot in the array, pick
        // a random item to swap into that spot.
        for (int i = 0; i < array.Length - 1; i++)
        {
            int j = rand.Next(i, array.Length);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    //Pausa el juego
    public void pause()
    {
        stop = true;
        pauseButton.SetActive(false);
        continueButton.SetActive(true);
        exitButton.SetActive(true);
    }

    //Continua el juego
    public void continueBingo()
    {
        stop = false;
        pauseButton.SetActive(true);
        continueButton.SetActive(false);
        exitButton.SetActive(false);
    }

    public void restart()
    {
        StopCoroutine(sequence);
        sequence = null;
        continueBingo();
        initializeBingo();
    }
}
