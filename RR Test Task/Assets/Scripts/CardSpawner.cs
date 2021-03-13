using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CardSpawner : MonoBehaviour
{
    [SerializeField] int minNumberOfCards = 4, maxNumberOfCards = 6;
    [SerializeField] Card cardPrefab;
    [SerializeField] GameObject target;

    public List<Card> cardsAtHand = new List<Card>();
    public int cardIndex = 0;

    [SerializeField] int minValue = -2, maxValue = 9;

    void Start()
    {
        int numberOfCards = Random.Range(minNumberOfCards, maxNumberOfCards);

        for (int i = 0; i <= numberOfCards; i++)
        {
            Card newCard = Instantiate(cardPrefab, transform);
            cardsAtHand.Add(newCard);
        }

        SetRotation();
    }

    private void SetRotation()
    {
        for (int i = 0; i < cardsAtHand.Count; i++)
        {
            float totalTwist = -50f;
            float twistPerCard = totalTwist / cardsAtHand.Count;
            float startTwist = -1f * (totalTwist / 2f);
            float twistForThisCard = startTwist + (i * twistPerCard);
            cardsAtHand[i].transform.DORotate(new Vector3(0f, 0f, twistForThisCard), 1f);
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
            
            SetRotation();
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
