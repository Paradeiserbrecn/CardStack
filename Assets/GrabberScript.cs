using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : MonoBehaviour
{
    [SerializeField] private float rotationPerCard = 10f;
    [HideInInspector] public Transform spacer;
    private float rotationChange;


    // Start is called before the first frame update
    void Start()
    {
        spacer = transform.GetChild(0);
    }

    internal void SpreadCards(List<Card> cardsInHand)
    {
        transform.localRotation = Quaternion.identity; 
        //transform.Rotate(Vector3.forward, rotationPerCard/2 * cardsInHand.Count);

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].RotateAroundGrabber(this, i, rotationPerCard);
        }
        /*
        foreach (Card card in cardsInHand)
        {
            //Todo: this is not optimized in the slightest, every time you add a new card to your hand you iterate over all cards, and set their positions
            // I am, however, way too stupid to make this better as of right now and wanna put my focus elsewhere till i figure out a better solution
            card.transform.SetParent(spacer, false);
            card.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.Rotate(Vector3.forward, -rotationPerCard);
            card.transform.SetParent(transform.parent, true);
        }
        */
    }
}
