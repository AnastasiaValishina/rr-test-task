using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CardSpawner : MonoBehaviour
{
    public int minNumberOfCards = 4, maxNumberOfCards = 6;
    public Card cardPrefab;
    public int minValue = -2, maxValue = 9;

    List<Card> cardsAtHand = new List<Card>();
    int cardIndex = 0;
    float overlap = 100f;
    float height = 20f;
    float cardWidth = 200f;

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

    private void ChangeToRandomValue() // изменить один рандомнай параметр на карте
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
        // вычислить положение первой катры по x
        float offsetX = -1f * cardsAtHand.Count * (cardWidth - overlap) / 2f + overlap / 2f; 

        // вычислить положение первой карты по y
        float totalY = cardsAtHand.Count * height;
        float startY = -1f * (totalY / 2f - height / 2);

        for (int i = 0; i < cardsAtHand.Count; i++)
        {
            // вычислить Rotation
            float totalTwist = -50f;
            float twistPerCard = totalTwist / cardsAtHand.Count;
            float startTwist = -1f * (totalTwist / 2f);
            float twistForThisCard = startTwist + (i * twistPerCard);

            if (cardsAtHand.Count > 1)
            {
                cardsAtHand[i].transform.DORotate(new Vector3(0f, 0f, twistForThisCard), 1f);
            }
            else
            {
                cardsAtHand[i].transform.DORotate(new Vector3(0f, 0f, 0f), 1f);
            }

            float xPos = offsetX;
            float yPos = startY * Mathf.Sign(twistForThisCard);

            cardsAtHand[i].transform.DOLocalMove(new Vector3(xPos, yPos, 0f), 1f);

            offsetX += overlap;
            startY += height;
        }
    }
}
