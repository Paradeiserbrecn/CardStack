using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] public static Object card;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Card DrawCard()
    {
        GameObject newCardObject = Instantiate(card) as GameObject;
        Card newCard = newCardObject.GetComponent<Card>();
        


        return newCard;
    }
}
