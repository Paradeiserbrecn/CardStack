using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTypes
{
    TwoOfHearts = 0, ThreeOfHearts = 1, FourOfHearts = 2, FiveOfHearts = 3, SixOfHearts = 4, SevenOfHearts = 5, EightOfHearts = 6, NineOfHearts = 7, TenOfHearts = 8, JackOfHearts = 9, QueenOfHearts = 10, KingOfHearts = 11, AceOfHearts = 12,
    TwoOfDiamonds = 13, ThreeOfDiamonds = 14, FourOfDiamonds = 15, FiveOfDiamonds = 16, SixOfDiamonds = 17, SevenOfDiamonds = 18, EightOfDiamonds = 19, NineOfDiamonds = 20, TenOfDiamonds = 21, JackOfDiamonds = 22, QueenOfDiamonds = 23, KingOfDiamonds = 24, AceOfDiamonds = 25,
    TwoOfClubs = 26, ThreeOfClubs = 27, FourOfClubs = 28, FiveOfClubs = 29, SixOfClubs = 30, SevenOfClubs = 31, EightOfClubs = 32, NineOfClubs = 33, TenOfClubs = 34, JackOfClubs = 35, QueenOfClubs = 36, KingOfClubs = 37, AceOfClubs = 38,
    TwoOfSpades = 39, ThreeOfSpades = 40, FourOfSpades = 41, FiveOfSpades = 42, SixOfSpades = 43, SevenOfSpades = 44, EightOfSpades = 45, NineOfSpades = 46, TenOfSpades = 47, JackOfSpades = 48, QueenOfSpades = 49, KingOfSpades = 50, AceOfSpades = 51,
    JokerRed = 53, JokerBlack = 54,
    CardBack = 52,
}

public class Card : MonoBehaviour
{
    [SerializeField] private Sprite[] cardFaces;
    [HideInInspector] public bool showCard = false;
    SpriteRenderer spriteRenderer;
    public readonly CardTypes CardType;
    public ulong OwnerId;
    // Start is called before the first frame update
    public Card (CardTypes cardType, ulong? ownerId)
    {
        this.CardType = cardType;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardFaces[(int)CardTypes.CardBack];
        if (ownerId.HasValue) OwnerId = (ulong)ownerId;
    }

    public bool setVisibility(bool showCard = true) 
    {
        this.showCard = showCard;
        if (showCard) spriteRenderer.sprite = cardFaces[(int)CardType];
        else spriteRenderer.sprite = cardFaces[(int)CardTypes.CardBack];
        return showCard;
    }
}