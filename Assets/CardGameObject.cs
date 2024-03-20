using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class CardGameObject : NetworkBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] CardSprites;
    [SerializeField] private NetworkVariable<CardObject> _card = new(new(CardTypes.CardBack));
    public CardObject Card
    {
        get => _card.Value;
        set
        {
            Debug.Log("Setting card for card: " + this);
            SetCardRpc(value);
        }
    }
    [Rpc(SendTo.Server)]
    private void SetCardRpc(CardObject value)
    {
        Debug.Log("Setting card on server");

        _card.Value = value;
        SetSpriteRpc();
    }

    [SerializeField] private bool _isVisible = false;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            Debug.Log("Setting IsVisible for card: " + this);
            SetIsVisibleRpc(value);
        }
    }

    [Rpc(SendTo.Server)]
    private void SetIsVisibleRpc(bool value)
    {
        Debug.Log("Setting visibility on server");
        _isVisible = value;
        SetSpriteRpc();
    }

    [Rpc(SendTo.Everyone)]
    private void SetSpriteRpc()
    {
        Debug.Log("Setting visibility for everyone");

        if (_isVisible) spriteRenderer.sprite = CardSprites[(int)Card.cardType];
        else spriteRenderer.sprite = CardSprites[(int)CardTypes.CardBack];
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown Captured");
        IsVisible = !IsVisible;
    }
}