using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    [SerializeField] private Object prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Rpc(SendTo.Server)]
    internal void PlaceCardInHandRpc(CardObject card)
    {
        CardGameObject cardGameObject = (CardGameObject.Instantiate(prefab) as GameObject).GetComponent<CardGameObject>();
        cardGameObject.GetComponent<NetworkObject>().Spawn();
        cardGameObject.Card = card;
        Debug.Log("Trying to spawn Networkobject: " + cardGameObject.Card);
        //card.transform.localPosition = new Vector3(card.transform.localPosition.x, -math.pow(card.transform.localPosition.x * .25f, 2) - transform.localPosition.y + offsetY);
        //card.transform.up = transform.localPosition - card.transform.localPosition;
    }
}
