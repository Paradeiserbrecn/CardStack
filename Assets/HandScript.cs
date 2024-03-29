using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    // Start is called before the first frame update
    private List<CardGameObject> cards;

    private float offsetY = 0;
    void Start()
    {
        offsetY = -Camera.main.orthographicSize;
        gameObject.transform.localPosition = new Vector2(0, offsetY);   
    }
    private void Update()
    {
        foreach (CardGameObject card in cards)
        {
            card.transform.localPosition = new Vector3(card.transform.localPosition.x, -math.pow(card.transform.localPosition.x * .25f, 2) - transform.localPosition.y + offsetY);
            card.transform.up = transform.localPosition - card.transform.localPosition;
        }
    }
    public void addCardToList(GameObject card)
    {
        cards.Add(card.GetComponent<CardGameObject>());
    }
}