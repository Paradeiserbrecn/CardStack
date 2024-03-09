using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private HandScript hand;
    private ulong playerId;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -Camera.main.orthographicSize, 0);
        hand = GetComponentInChildren<HandScript>();

        //TODO Set ownerId with NetCode
        playerId = 123;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hand.DrawCardFromTop(playerId);
        }
    }
}
