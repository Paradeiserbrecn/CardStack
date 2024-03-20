using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CardStackRpc : NetworkBehaviour
{
    private NetworkList<CardObject> cards;
    public int count = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        cards = new();
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Debug.Log("Populating cards");
            PopulateDebugCardsRpc();
        }
        Debug.Log("CardStack Spawned with elements: " + cards.Count);
    }

    [Rpc(SendTo.Server)]
    private void PopulateDebugCardsRpc()
    {
        IEnumerable<CardTypes> cardTypes = (IEnumerable<CardTypes>)Enum.GetValues(typeof(CardTypes));
        cards.Clear();
        
        if (cards.CanClientWrite(NetworkManager.LocalClientId))
            foreach (CardTypes cardType in cardTypes)
            {
                cards.Add(new CardObject(cardType));
            }
    }

    // Update is called once per frame
    void Update()
    {
        count = cards.Count;
    }

    private static readonly System.Random rng = new();

    public void AddAsTopCard(CardObject card) => AddCardAtIndexRpc(card, 0);

    public void AddAsBottomCard(CardObject card) => AddCardAtIndexRpc(card, cards.Count);

    public void AddAsRandomCard(CardObject card) => AddCardAtIndexRpc(card, rng.Next(cards.Count));


    [Rpc(SendTo.Server)]
    private void AddCardAtIndexRpc(CardObject card, int index)
    {
        Debug.Log("Adding card to cardstack at position: " + card + " " + index);
        cards.Insert(index, card);
    }

    #region removeCards
    public CardObject RemoveTopCard() => RemoveCard(cards[0]);

    public CardObject RemoveBottomCard() => RemoveCard(cards[cards.Count-1]);

    public CardObject RemoveRandomCard() => RemoveCard(cards[rng.Next(cards.Count - 1)]);

    private CardObject RemoveCard(CardObject card)
    {
        RemoveCardRpc(card);
        return card;
    }

    [Rpc(SendTo.Server)]
    private void RemoveCardRpc(CardObject card)
    {
        Debug.Log("removing card from cardstack: " + card);
        cards.Remove(card);
    }
    #endregion

}
