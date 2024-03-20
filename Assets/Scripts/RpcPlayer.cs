using System;
using Unity.Netcode;
using UnityEngine;

public class RpcPlayer : NetworkBehaviour
{
    public NetworkVariable<int> spacePressed = new(0);
    private CardStackRpc cardStack;
    private HandScript handScript;

    private void Start()
    {
        cardStack = GameObject.FindWithTag("CardStack").GetComponent<CardStackRpc>();
        handScript = GetComponentInChildren<HandScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //IncreaseAndLogSpacesPressedRpc();
                DrawCardFromTop();
            }
        }
    }

    private void DrawCardFromTop()
    {
        CardObject card = cardStack.RemoveTopCard();
        Debug.Log("Drew card: " + card);
        handScript.PlaceCardInHandRpc(card);
        
    }

    [Rpc(SendTo.Server)]
    void IncreaseAndLogSpacesPressedRpc ()
    {
        spacePressed.Value += 1;
        LogSpacesPressedRpc();
    }

    [Rpc(SendTo.ClientsAndHost)]
    void LogSpacesPressedRpc ()
    {
        Debug.Log($"Player {NetworkManager.LocalClientId} pressed space {spacePressed.Value} times");

    }
}
