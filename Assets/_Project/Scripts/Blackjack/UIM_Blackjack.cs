using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIM_Blackjack : MonoBehaviour
{
    public TMP_Text player_money;
    public TMP_Text player_bind;
    public TMP_Text player_value;

    public TMP_Text IA_money;
    public TMP_Text IA_bind;
    public TMP_Text IA_value;

    public GameObject card_prefab;
    public GameObject player_hand;
    public GameObject IA_hand;

    public GameObject IA_wins;
    public GameObject IA_stays;
    public GameObject player_wins;
    public GameObject draw;

    public Button moreButton;
    public Button stayButton;
    public Button refreshButton;


    public  void UpdatePMoney(int value)
    {
        player_money.text = value.ToString();
    }

    public void UpdatePBind(int value)
    {
        player_bind.text = value.ToString();
    }

    public void UpdateIAMoney(int value)
    {
        IA_money.text = value.ToString();
    }

    public void UpdateIABind(int value)
    {
        IA_bind.text = value.ToString();
    }

    public void giveCardToPlayer(Sprite card_image, int value)
    {
        GameObject card = Instantiate(card_prefab, player_hand.transform);

        card.GetComponent<Image>().sprite = card_image;

        player_value.text = value.ToString();
    }

    public void giveCardToIA(Sprite card_image, int value)
    {
        GameObject card = Instantiate(card_prefab, IA_hand.transform);

        card.GetComponent<Image>().sprite = card_image;

        IA_value.text = value.ToString();
    }

    public void playerWinsText()
    {
        player_wins.SetActive(true);
    }

    public void IAWinsText()
    {
        IA_wins.SetActive(true);
    }

    public void drawText()
    {
        draw.SetActive(true);
    }

    public void showIAStays()
    {
        IA_stays.SetActive(true);
    }

    public void moreButtonState(bool state)
    {
        moreButton.interactable = state;
    }

    public void stayButtonState(bool state)
    {
        stayButton.interactable = state;
    }

    public void refreshButtonState(bool state)
    {
        refreshButton.interactable = state;
    }

    public void resetHands()
    {
        foreach(Transform children in player_hand.transform)
        {
            Destroy(children.gameObject);
        }

        foreach (Transform children in IA_hand.transform)
        {
            Destroy(children.gameObject);
        }
    }

    public void resetStateTexts()
    {
        IA_stays.SetActive(false);
        player_wins.SetActive(false);
        IA_wins.SetActive(false);
        draw.SetActive(false);

        IA_value.text = "0";
        IA_value.text = "0";


    }
}
