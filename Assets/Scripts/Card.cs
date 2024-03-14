using Unity.Netcode;
using Unity.VisualScripting;
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
    [HideInInspector] public NetworkVariable<bool> showCard;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    private NetworkVariable<CardTypes> _cardType;
    public CardTypes CardType {  get { return _cardType.Value; } set {  _cardType.Value = value; } }

    /*
    private ulong _ownerId;
    public ulong OwnerId
    {
        get => _ownerId;
        set
        {
            if (value == NetworkManager.Singleton.LocalClientId)
            {
                spriteRenderer.sprite = cardFaces[(int)CardType];
            }
            else
            {
                spriteRenderer.sprite = cardFaces[(int)CardTypes.CardBack];
            }
            _ownerId = value;
        }
    }
    */



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        showCard.Value = false;
        _cardType.Value = CardTypes.CardBack;
    }

    public bool setVisibility(bool showCard = true)
    {
        this.showCard.Value = showCard;
        if (showCard) spriteRenderer.sprite = cardFaces[(int)CardType];
        else spriteRenderer.sprite = cardFaces[(int)CardTypes.CardBack];
        return showCard;
    }

    private void OnMouseDown()
    {
        Debug.Log(CardType);
    }

}