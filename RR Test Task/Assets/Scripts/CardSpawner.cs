using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CardSpawner : MonoBehaviour
{
    public int minNumberOfCards = 4, maxNumberOfCards = 6;
    public Card cardPrefab;
    public int minValue = -2, maxValue = 9;

    [Header("Card positioning")]
    [Tooltip("Overlap of cards along X axis")]
    public float spacing = -50f;
    [Tooltip("Distance between cards' pivots along Y axis")]
    public float height = 20f;
    [Tooltip("Angle between the first cards and the last")]
    public float totalTwist = -70f;
    public float cardWidth = 200f;

    List<Card> cardsAtHand = new List<Card>();
    int cardIndex = 0;

    void Start()
    {
        // выдать игроку 4-6 карт
        int numberOfCards = Random.Range(minNumberOfCards, maxNumberOfCards);

        for (int i = 0; i <= numberOfCards; i++)
        {
            Card newCard = Instantiate(cardPrefab, transform);
            cardsAtHand.Add(newCard);
        }

        SetPosition();
    }

    public void OnButtonClick()
    {
        if (cardsAtHand.Count <= 0) { return; }

        ChangeToRandomValue();

        cardIndex++;

        if (cardIndex >= cardsAtHand.Count)
        {
            cardIndex = 0;
        }
    }

    private void ChangeToRandomValue() // Change one random value of the card
    {
        int randomValue = Random.Range(minValue, maxValue);
        int randomParameter = Random.Range(0, 2);

        if (randomParameter == 0) 
        { 
            cardsAtHand[cardIndex].attack = randomValue;
            cardsAtHand[cardIndex].UpdateAttack();
        }
        if (randomParameter == 1) 
        { 
            cardsAtHand[cardIndex].health = randomValue;
            cardsAtHand[cardIndex].UpdateHealth();
        }
        if (randomParameter == 2) 
        { 
            cardsAtHand[cardIndex].mana = randomValue;
            cardsAtHand[cardIndex].UpdateMana();
        }

        if (cardsAtHand[cardIndex].health < 1)
        {
            Destroy(cardsAtHand[cardIndex].gameObject);
            cardsAtHand.RemoveAt(index: cardIndex);
            SetPosition();
        }
    }

    private void SetPosition()
    {
        // Calculate x of the first card
        float cardRealWidth = cardWidth + spacing;                  // space of the card on the screen (card width minus overlap)
        float totalX = cardsAtHand.Count * cardRealWidth + spacing;
        float startX = -1f * ((totalX - cardWidth) / 2f - spacing);

        // Calculate y of the first card
        float totalY = cardsAtHand.Count * height;
        float startY = -1f * (totalY / 2f - height / 2f);

        // Calculate Rotation of the first card
        float twistPerCard = totalTwist / cardsAtHand.Count;
        float startZ = -1f * ((totalTwist / 2f) - twistPerCard / 2f);

        for (int i = 0; i < cardsAtHand.Count; i++)
        {
            float twistForThisCard = startZ + (i * twistPerCard);
            float xPos = startX + (i * cardRealWidth);
            float yPos = (startY + (i * height)) * Mathf.Sign(twistForThisCard);

            if (cardsAtHand.Count > 1)
            {
                cardsAtHand[i].transform.DORotate(new Vector3(0f, 0f, twistForThisCard), 1f);
            }
            else
            {
                cardsAtHand[i].transform.DORotate(new Vector3(0f, 0f, 0f), 1f);
            }

            cardsAtHand[i].transform.DOLocalMove(new Vector3(xPos, yPos, 0f), 1f);
        }
    }
}
