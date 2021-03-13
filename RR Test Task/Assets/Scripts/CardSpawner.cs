using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] int minNumberOfCards = 4, maxNumberOfCards = 6;
    [SerializeField] Card cardPrefab;
    [SerializeField] GameObject target;

    [SerializeField] List<Card> cardsAtHand = new List<Card>();
    int cardIndex = 0;

    void Start()
    {
        int numberOfCards = Random.Range(minNumberOfCards, maxNumberOfCards);

        for (int i = 0; i <= numberOfCards; i++)
        {
            Card newCard = Instantiate(cardPrefab, transform);
            cardsAtHand.Add(newCard);
        }  
    }

    public void OnButtonClick()
    {
        cardsAtHand[cardIndex].ChangeToRandomValue();
        cardIndex++;
        if (cardIndex >= cardsAtHand.Count)
        {
            cardIndex = 0;
        }
    }
}
