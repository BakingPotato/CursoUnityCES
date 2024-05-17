using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulaBD : MonoBehaviour
{

    public Text txtVida;
    public Text txtMana;
    public Text txtAtq;
    public Image cartaPic;

    private int v, a, m;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //---------------------------------------------------------------------
    public void SacarCarta()
    {
        a = Random.Range(1,16);
        v = Random.Range(1, 11);
        m = Random.Range(1, 7);

        int rndCard = Random.Range(1, 16);
        cartaPic.sprite = Resources.Load<Sprite>("carta"+rndCard);
        PintarEnUI(m, v, a);
    }
    //---------------------------------------------------------------------
    public int ConsultaVida()
    {
        return v;
    }
    //---------------------------------------------------------------------
    public int ConsultaAtaque()
    {
        return a;
    }
    //---------------------------------------------------------------------
    public int ConsultaMana()
    {
        return m;
    }
    //---------------------------------------------------------------------
    public void PintarEnUI(int energy, int life, int att)
    {
        txtAtq.text = att.ToString();
        txtMana.text = energy.ToString();
        txtVida.text = life.ToString();
    }
}
