using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using Random = System.Random;

public class CardStack : NetworkBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [HideInInspector] public NetworkVariable<List<Card>> initialCards = new();
    [HideInInspector] public NetworkVariable<List<Card>> currentCards = new();

    public float positionOffsetX = -2.5f;
    public float positionOffsetY = -2.5f;

    

    void Start()
    {
        var upperRightScreen = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        transform.position = new Vector3(upperRightScreen.x + positionOffsetX, upperRightScreen.y + positionOffsetY, transform.position.z);
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // TODO: Remove this whenever you add cardstack selection
            initialCards.Value = PopulateDebugCards();

            ResetCurrentCardsRpc();
        }
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
    
    [Rpc(SendTo.Server)]
    public void ShuffleCurrentCardsRpc()
    {
        currentCards.Value = currentCards.Value.OrderBy(_ => rng.Next()).ToList();
    }

    [Rpc(SendTo.Server)]
    private void ResetCurrentCardsRpc()
    {
        currentCards.Value = new List<Card>(initialCards.Value);
        ShuffleCurrentCardsRpc();
    }

    #region removeCards
    public Card RemoveTopCard () => RemoveCard(currentCards.Value.First());

    public Card RemoveBottomCard() => RemoveCard(currentCards.Value.Last());

    public Card RemoveRandomCard() => RemoveCard(currentCards.Value.ElementAt(rng.Next(currentCards.Value.Count() - 1)));

    private Card RemoveCard(Card card)
    {
        RemoveCardRpc(card);
        return card;
    }

    private void RemoveCardRpc(Card card)
    {
        //Debug.Log("removing card from cardstack: " + card.CardType);
        currentCards.Value.Remove(card);
        if (currentCards.Value.Count == 0) { ResetCurrentCardsRpc(); }
    }
    #endregion

}
