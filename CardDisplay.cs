using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TripleShowdown;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Image cardImage;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image[] typeImages;
    public Image[] damageImages;

    public Image displayImage;


    private Color[] cardColors =
        {
        Color.red, //Attack
        Color.blue, //Guard
        Color.green //Multiply
        };
    private Color[] typeColors =
    {
        new Color(0.7f,0.166f,0f), //Attack
        new Color(0f,0f,0.53f), //Guard
        new Color(0f,0.44f,0f) //Multiply
        };
    void Start()
    {
        UpdateCardDisplay();
    }
    public void UpdateCardDisplay()
    {
        //Update the main card image color based on the first card type
        cardImage.color = cardColors[(int)cardData.cardType[0]];

        nameText.text = cardData.cardName;
        healthText.text = cardData.health.ToString();
        damageText.text = $"{cardData.damageMin}-{cardData.damageMax}";
        displayImage.sprite = cardData.cardSprite;

        //Update type images
        for (int i = 0; i< typeImages.Length; i++)
        {
            if (i < cardData.cardType.Count)
            {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typeColors[(int)cardData.cardType[i]];
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }
    }
}
