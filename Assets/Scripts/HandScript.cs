using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    [SerializeField] private float offsetY = 1f;
    private CardStack cardStack;
    private List<Card> cardsInHand;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, Camera.main.orthographicSize * -1);
        cardStack = FindObjectOfType<CardStack>();
        cardsInHand = new();
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        foreach (Card card in cardsInHand)
        {
            card.transform.localPosition = new Vector3(card.transform.localPosition.x, -math.pow(card.transform.localPosition.x*.25f, 2)-transform.localPosition.y+offsetY);
            card.transform.up = transform.localPosition - card.transform.localPosition;
        }
    }

    internal void DrawCardFromTop(ulong playerId)
    {
        Card cardToAdd = cardStack.RemoveTopCard();
        cardsInHand.Add(cardToAdd);
        cardToAdd.spriteRenderer.sortingOrder = cardsInHand.Count;
        cardToAdd.transform.SetParent(transform, false);

        cardToAdd.OwnerId = playerId;
    }

}
