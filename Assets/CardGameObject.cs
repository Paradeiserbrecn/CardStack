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
    #region Card
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
    }

    private void OnIsCardChanged(CardObject prev, CardObject current)
    {
        if (IsVisible) spriteRenderer.sprite = CardSprites[(int)current.cardType];
        else spriteRenderer.sprite = CardSprites[(int)CardTypes.CardBack];
    }
    #endregion
    #region isVisible

    [SerializeField] private NetworkVariable<bool> _isVisible = new(false);
    public bool IsVisible
    {
        get => _isVisible.Value;
        set
        {
            Debug.Log("Setting IsVisible for card: " + this.Card + " to " + value);
            SetIsVisibleOnServerRpc(value);
        }
    }

    [Rpc(SendTo.Server)]
    private void SetIsVisibleOnServerRpc(bool value)
    {
        Debug.Log("Setting visibility on server to: " + value);
        _isVisible.Value = value;
    }
    private void OnIsVisibleChanged(bool prev, bool current)
    {
        if (current) spriteRenderer.sprite = CardSprites[(int)Card.cardType];
        else spriteRenderer.sprite = CardSprites[(int)CardTypes.CardBack];
        Debug.Log("Updated sprite for everyone to: " + spriteRenderer.sprite.name + " because isVisible is set to: " + IsVisible);
    }
    #endregion

    void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
    public override void OnNetworkSpawn()
    {
        _isVisible.OnValueChanged = OnIsVisibleChanged;
        _card.OnValueChanged = OnIsCardChanged;
    }

    private void OnMouseDown()
    {
        if (IsOwner)
        {
            Debug.Log("OnMouseDown Captured");
            IsVisible = !IsVisible;
        }
    }
}