using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Random = System.Random;

public class CardStack : MonoBehaviour
{
    public static List<Card> initialCards = new();
    public static List<Card> currentCards = new();

    void Start()
    {
        // TODO: Remove this whenever you add cardstack selection
        initialCards = PopulateDebugCards();

        ResetCurrentCards();
    }

    private static List<Card> PopulateDebugCards()
    {
        IEnumerable<CardTypes> cardTypes = Enum.GetValues(typeof(CardTypes)).Cast<CardTypes>();
        List<Card> cards = new();
        foreach (CardTypes cardType in cardTypes)
        {
            Card card = new(cardType, Convert.ToUInt64(-1));
            cards.Add(card);
        }
        return cards;
    }

    private static readonly Random rng = new();

    public static List<Card> ShuffleCurrentCards()
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
    public Card RemoveTopCard()
    {
        Card card = currentCards.First();
        currentCards.Remove(card);
        return card;
    }

    public Card RemoveBottomCard()
    {
        Card card = currentCards.Last();
        currentCards.Remove(card);
        return card;
    }

    public Card RemoveRandomCard()
    {
        Card card = currentCards.ElementAt(rng.Next(currentCards.Count()-1));
        currentCards.Remove(card);
        return card;
    }
    #endregion

}
