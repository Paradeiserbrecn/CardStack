using Unity.Mathematics;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class RpcPlayer : NetworkBehaviour
{
    public NetworkVariable<int> spacePressed = new(0);
    [SerializeField] private Object prefab;
    private CardStackRpc cardStack;
    private HandScript handScript;

    private void Start()
    {
        cardStack = GameObject.FindWithTag("CardStack").GetComponent<CardStackRpc>();
        handScript = GetComponentInChildren<HandScript>();
        transform.position = new Vector2(0, -Camera.main.orthographicSize);
    }
    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DrawCardFromTop();
            }
        }
    }

    private void DrawCardFromTop()
    {
        CardObject card = cardStack.RemoveTopCard();
        Debug.Log("Drew card: " + card);
        DrawCardToHandOnServerRpc(card, OwnerClientId);

    }


    [Rpc(SendTo.Server)]
    internal void DrawCardToHandOnServerRpc(CardObject card, ulong ownerClientId)
    {
        GameObject prefabGameObject = CardGameObject.Instantiate(prefab, this.transform).GameObject();
        CardGameObject cardGameObject = prefabGameObject.GetComponent<CardGameObject>();
        cardGameObject.GetComponent<NetworkObject>().SpawnWithOwnership(ownerClientId);
        cardGameObject.Card = card;
        Debug.Log("Trying to spawn Networkobject: " + cardGameObject.Card);
        //PutCardInHandRpc(cardGameObject.gameObject);
    }

    //TODO: Figure out how to pass the created gameobject to the client so they can put the card in hand and do operations on it

    //[Rpc(SendTo.ClientsAndHost)]
    //private void PutCardInHandRpc(GameObject card) => handScript.addCardToList(card);
}
