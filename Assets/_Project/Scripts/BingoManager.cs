using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BingoManager : MonoBehaviour
{

    [Header("Tiempo")]
    public float waitTime = 6;
    bool stop = false;

    [Header("UI")]
    public TMP_Text last_number;
    public TMP_Text old_numbers;
    public GameObject pauseButton;
    public GameObject continueButton;
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

    private void fillBingoList()
    {
        for (int i = 1; i < 100; i++)
        {
            bingo_numbers.Add(i);
        }

        for (int i = 0; i < 99; i++)
        {
            bingo_numbers_array[i] = i + 1;
        }

        array_limit = 98;
    }

    IEnumerator bingoSequence()
    {
        //popBingoNumber();
        popBingoNumber_Array();

        //while (bingo_numbers.Count > 0)
        while (array_limit > 0)
        {
            yield return new WaitForSeconds(waitTime);

            //Si se paro la partida, esperamos para que continue
            yield return new WaitUntil(() => {
                return stop == false;
            });

            updateOldNumbers();
            //popBingoNumber();
            popBingoNumber_Array();


        }
    }

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

    public void pause()
    {
        stop = true;
        pauseButton.SetActive(false);
        continueButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void continueBingo()
    {
        stop = false;
        pauseButton.SetActive(true);
        continueButton.SetActive(false);
        exitButton.SetActive(false);
    }

    public void initializeBingo()
    {
        old_numbers.text = "";
        last_number.text = "";

        //Rellenamos la lista bingo con 99 números del 1 al 99
        fillBingoList();

        RandomizeArray(bingo_numbers_array);

        sequence = StartCoroutine(bingoSequence());
    }

    public void restart()
    {
        StopCoroutine(sequence);
        sequence = null;
        continueBingo();
        initializeBingo();
    }
}
