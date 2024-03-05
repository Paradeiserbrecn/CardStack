using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardTypes
    {
        TwoOfHearts = 1, ThreeOfHearts = 2, FourOfHearts = 3, FiveOfHearts = 4, SixOfHearts = 5, SevenOfHearts = 6, EightOfHearts = 7, NineOfHearts = 8, TenOfHearts = 9, JackOfHearts = 10, QueenOfHearts = 11, KingOfHearts = 12, AceOfHearts = 13,
        TwoOfDiamonds = 14, ThreeOfDiamonds = 15, FourOfDiamonds = 16, FiveOfDiamonds = 17, SixOfDiamonds = 18, SevenOfDiamonds = 19, EightOfDiamonds = 20, NineOfDiamonds = 21, TenOfDiamonds = 22, JackOfDiamonds = 23, QueenOfDiamonds = 24, KingOfDiamonds = 25, AceOfDiamonds = 26,
        TwoOfClubs = 27, ThreeOfClubs = 28, FourOfClubs = 29, FiveOfClubs = 30, SixOfClubs = 31, SevenOfClubs = 32, EightOfClubs = 33, NineOfClubs = 34, TenOfClubs = 35, JackOfClubs = 36, QueenOfClubs = 37, KingOfClubs = 38, AceOfClubs = 39,
        TwoOfSpades  = 40, ThreeOfSpades  = 41, FourOfSpades  = 42, FiveOfSpades  = 43, SixOfSpades  = 44, SevenOfSpades  = 45, EightOfSpades  = 46, NineOfSpades  = 47, TenOfSpades  = 48, JackOfSpades  = 49, QueenOfSpades  = 50, KingOfSpades  = 51,AceOfSpades  = 52,
        JokerRed = 53, JokerBlack = 54,
    }
    SpriteRenderer renderer;
    Texture2D texture;
    public readonly CardTypes cardType;
    // Start is called before the first frame update
    
    public Card (CardTypes cardType)
    {
        this.cardType = cardType;
        renderer = GetComponent<SpriteRenderer>();
        // TODO: Use the card texture for the renderer
    }

    // Update is called once per frame
    void Update()
    {

    }
}