using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CardStackRpc : NetworkBehaviour
{
    private readonly NetworkList<CardObject> cards = new();
    // Start is called before the first frame update
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
                cards.Add((int)cardType);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnDestroy()
    {
        cards.Dispose();
    }


}
