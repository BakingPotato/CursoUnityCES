using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Blackjack : MonoBehaviour
{

    [System.Serializable]
    public class Card
    {
        public Sprite image;
        public int value;
    }

    public List<Sprite> cards_images;
    List<Card> cards;

    List<Card> player_cards = new List<Card>();
    List<Card> ia_cards = new List<Card>();
    int player_value = 0;
    int IA_value = 0;

    int player_money = 10;
    int player_bind = 0;
    int IA_money = 10;
    int IA_bind = 0;
    bool player_Stay = false;
    bool IA_Stay = false;

    // Start is called before the first frame update
    void Start()
    {
        fillCardList();

        //Cojemos dos cartas al inicio
        takeCard_Player();
        takeCard_Player();

        player_bind = 1;

        takeCard_IA();

        IA_bind = 1;
    }

    private void fillCardList()
    {
        foreach (Sprite s in cards_images)
        {
            Card aux = new Card();
            aux.image = s;

            if (s.name.Contains("1"))
                aux.value = 1;
            else if (s.name.Contains("2"))
                aux.value = 2;
            else if (s.name.Contains("3"))
                aux.value = 3;
            else if (s.name.Contains("4"))
                aux.value = 4;
            else if (s.name.Contains("5"))
                aux.value = 5;
            else if (s.name.Contains("6"))
                aux.value = 6;
            else if (s.name.Contains("7"))
                aux.value = 7;
            else if (s.name.Contains("8"))
                aux.value = 8;
            else if (s.name.Contains("9"))
                aux.value = 9;
            else
                aux.value = 10;

            cards.Add(aux);
        }
    }


    private void takeCard_Player()
    {
        Card actual_card = takeCard();
        player_value += actual_card.value;
        player_cards.Add(actual_card);
    }

    private void takeCard_IA()
    {
        //Si estamos cerca de 21, usamos este algoritmo para decidir si vuelve a coger o no
        if (!player_Stay)
        {
            if (21 - IA_value <= 6)
            {
                int random = UnityEngine.Random.Range(0, 2);
                if (random == 1)
                {
                    IA_Stay = true;
                    return;
                }
            }
        }
        else
        {
            //Si el jugador se paso, la IA gana
            if(player_value > 21)
            {
                IA_Stay = true;
                return;
            }
            else if(player_value < 21)
            {
                //Si la IA ya supera el valor del jugador, hace stay y gana
                if(21 - player_value > 21 - IA_value)
                {
                    IA_Stay = true;
                    return;
                }
            }
        }


        Card actual_card = takeCard();
        IA_value += actual_card.value;
        ia_cards.Add(actual_card);

        if(IA_value >= 21)
            IA_Stay = true;
    }


    Card takeCard()
    {
        int random = UnityEngine.Random.Range(0, cards.Count);
        Card taken_card = cards[random];
        cards.RemoveAt(random);

        return taken_card;
    }

    //Plantarse
    public void stayButton()
    {
        player_Stay = true;

        //Sequencia donde la IA coje hasta ganarte o perder
    }

    //Pedir una carta más
    public void moreButton()
    {
        //Se le da una carta más al jugador
        takeCard_Player();

        //Se comprueba si la ia puede pedir más
        if (player_value == 21)
        {
            player_Stay = true;
        }
        else
        {
            if (player_value > 21)
            {
                player_Stay = true;
            }
            //desactivamos el boton more

            if (!IA_Stay)
                takeCard_IA();
        }

        checkWinConditions();
    }

    private void checkWinConditions()
    {
        if (player_Stay && IA_Stay)
        {
            if (player_value > 21)
            {
                IAWin();
            }
            else if (IA_value > 21)
            {
                PlayerWin();
            }
            else //Jugador e IA estan ambas por debajo de 21, asique todo depende de cual es mayor
            {
                if (IA_value == player_value)
                {
                    Draw();
                }
                else if (IA_value > player_value)
                {
                    IAWin();
                }
                else
                {
                    PlayerWin();
                }
            }
        }
    }

    //EMPATE
    private void Draw()
    {
        player_money += player_bind;
        IA_money += IA_money;
    }

    //IA WINS
    private void IAWin()
    {
        IA_money += player_bind * 2;
        player_money -= player_bind * 2;
    }

    //PLAYER WINS
    private void PlayerWin()
    {
        player_money += player_bind * 2;
        IA_money -= player_bind * 2;
    }
}
