using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Blackjack : MonoBehaviour
{

    public UIM_Blackjack ui;

    [System.Serializable]
    public class Card
    {
        public Sprite image;
        public int value;
    }

    public List<Sprite> cards_images;
    List<Card> cards = new List<Card>();

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
        begginingSequence();

        ui.UpdatePMoney(player_money);
        fillCardList();

        //Cojemos dos cartas al inicio
        StartCoroutine(startGame());

    }

    private void begginingSequence()
    {
        ui.moreButtonState(false);
        ui.stayButtonState(false);

        player_bind = 1;
        IA_bind = 1;
        ui.UpdateIAMoney(IA_money);
        ui.UpdatePMoney(player_money);
        ui.UpdatePBind(player_bind);
        ui.UpdateIABind(IA_bind);
    }

    private IEnumerator startGame()
    {
        takeCard_Player();
        yield return new WaitForSeconds(1);

        takeCard_IA();
        yield return new WaitForSeconds(1);

        ui.moreButtonState(true);
        ui.stayButtonState(true);

    }

    private void fillCardList()
    {
        foreach (Sprite s in cards_images)
        {
            Card aux = new Card();
            aux.image = s;

            if (s.name.Contains("10"))
                aux.value = 10;
           else  if (s.name.Contains("1"))
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

        ui.giveCardToPlayer(actual_card.image, player_value);

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

        ui.giveCardToIA(actual_card.image, IA_value);

        if (IA_value >= 21)
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

        ui.moreButtonState(false);
        ui.stayButtonState(false);

        //Sequencia donde la IA coje hasta ganarte o perder
        StartCoroutine(IASoloTurn());
    }

    //Pedir una carta más
    public void moreButton()
    {
        //Se le da una carta más al jugador
        takeCard_Player();

        //Se comprueba si la ia puede pedir más
        if (player_value >= 21)
        {
            player_Stay = true;
            ui.moreButtonState(false);
            ui.stayButtonState(false);

            if (!IA_Stay)
                StartCoroutine(IASoloTurn());
        }
        else
        {
            if (!IA_Stay)
                takeCard_IA();
        }



        checkWinConditions();
    }

    IEnumerator IASoloTurn()
    {
        while (!IA_Stay)
        {
            takeCard_IA();
            checkWinConditions();
            yield return new WaitForSeconds(1);
        }
    }

    private void checkWinConditions()
    {
        if (IA_Stay)
        {
            if(player_value > 21 || IA_value > 21)
            {
                if (player_value > 21)
                {
                    IAWin();
                }

                if (IA_value > 21)
                {
                    PlayerWin();
                }
            }
            else
            {
                if (IA_value == player_value)
                {
                    Draw();
                }
                else if (IA_value > player_value)
                {
                    IAWin();
                }
                else if (IA_value < player_value)
                {
                    PlayerWin();
                }
            }

            ui.UpdateIAMoney(IA_money);
            ui.UpdatePMoney(player_money);
            ui.UpdatePBind(player_bind);
            ui.UpdateIABind(IA_bind);
        }
    }

    //EMPATE
    private void Draw()
    {
        player_money += player_bind;
        IA_money += IA_money;

        ui.drawText();
    }

    //IA WINS
    private void IAWin()
    {
        IA_money += IA_bind * 2;
        player_money -= player_bind;

        ui.IAWinsText();
    }

    //PLAYER WINS
    private void PlayerWin()
    {
        player_money += player_bind * 2;
        IA_money -= IA_bind;

        ui.playerWinsText();
    }
}
