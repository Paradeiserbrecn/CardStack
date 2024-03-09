using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CardStack : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [HideInInspector] public List<Card> initialCards = new();
    [HideInInspector] public List<Card> currentCards = new();

    public float positionOffsetX = -2.5f;
    public float positionOffsetY = -2.5f;

    void Start()
    {
        var upperRightScreen = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        transform.position = new Vector3(upperRightScreen.x + positionOffsetX, upperRightScreen.y + positionOffsetY, transform.position.z);

        // TODO: Remove this whenever you add cardstack selection
        initialCards = PopulateDebugCards();

        ResetCurrentCards();
    }

    private List<Card> PopulateDebugCards()
    {
        IEnumerable<CardTypes> cardTypes = Enum.GetValues(typeof(CardTypes)).Cast<CardTypes>();
        List<Card> cards = new();
        foreach (CardTypes cardType in cardTypes)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            Card card = cardObject.GetComponentInChildren<Card>();

            card.CardType = cardType;
            cards.Add(card);
        }
        return cards;
    }

    private static readonly Random rng = new();

    public List<Card> ShuffleCurrentCards()
    {
        return currentCards = currentCards.OrderBy(_ => rng.Next()).ToList();
    }

    private List<Card> ResetCurrentCards()
    {
        currentCards = new List<Card>(initialCards);
        ShuffleCurrentCards();

        return currentCards;
    }

    #region removeCards
    public Card RemoveTopCard () => RemoveCard(currentCards.First());
    public Card RemoveBottomCard() => RemoveCard(currentCards.Last());
    public Card RemoveRandomCard() => RemoveCard(currentCards.ElementAt(rng.Next(currentCards.Count() - 1)));
    
    private Card RemoveCard(Card card)
    {
        currentCards.Remove(card);
        if (currentCards.Count == 0) { ResetCurrentCards(); }
        return card;
    }
    #endregion

}
