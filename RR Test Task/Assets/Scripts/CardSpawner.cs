using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CardSpawner : MonoBehaviour
{
    [SerializeField] int minNumberOfCards = 4, maxNumberOfCards = 6;
    [SerializeField] Card cardPrefab;

    public List<Card> cardsAtHand = new List<Card>();
    public int cardIndex = 0;

    [SerializeField] int minValue = -2, maxValue = 9;
    float cardWidth = 200f;
    [SerializeField] float overlap = 100f;
    [SerializeField] float height = 50f;

    void Start()
    {
        int numberOfCards = Random.Range(minNumberOfCards, maxNumberOfCards);

        for (int i = 0; i <= numberOfCards; i++)
        {
            Card newCard = Instantiate(cardPrefab, transform);
            cardsAtHand.Add(newCard);
        }

        SetPosition();
    }

    private void SetPosition()
    {

        float offsetX = -1f * cardsAtHand.Count * (cardWidth - overlap) / 2f + overlap / 2f;
        float totalY = cardsAtHand.Count * height;
        float startY = -1f * (totalY / 2f - height / 2);

        for (int i = 0; i < cardsAtHand.Count; i++)
        {
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

    private void ChangeToRandomValue()
    {
        int randomValue = Random.Range(minValue, maxValue);
        int randomParameter = Random.Range(0, 2);

        if (randomParameter == 0) { cardsAtHand[cardIndex].attack = randomValue; }
        if (randomParameter == 1) { cardsAtHand[cardIndex].health = randomValue; }
        if (randomParameter == 2) { cardsAtHand[cardIndex].mana = randomValue; }

        if (cardsAtHand[cardIndex].health < 1)
        {
            Destroy(cardsAtHand[cardIndex].gameObject);
            cardsAtHand.RemoveAt(index: cardIndex);            
            SetPosition();
        }
    }

    public void OnButtonClick()
    {
        ChangeToRandomValue();

        cardIndex++;
        if (cardIndex >= cardsAtHand.Count)
        {
            cardIndex = 0;
        }
    }
}
