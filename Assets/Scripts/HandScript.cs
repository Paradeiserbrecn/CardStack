using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    private CardStack cardStack;
    private List<Card> cardsInHand;
    private GrabberScript grabber;

    // Start is called before the first frame update
    void Start()
    {
        cardStack = FindObjectOfType<CardStack>();
        cardsInHand = new();
        grabber = GetComponentInChildren<GrabberScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCardFromTop();
        }
    }

    private void DrawCardFromTop()
    {
        Card cardToAdd = cardStack.RemoveTopCard();
        cardsInHand.Add(cardToAdd);
        cardToAdd.spriteRenderer.sortingOrder = cardsInHand.Count;
        cardToAdd.spriteRenderer.sortingLayerID = SortingLayer.layers[1].id;

        grabber.SpreadCards(cardsInHand);

    }
}
