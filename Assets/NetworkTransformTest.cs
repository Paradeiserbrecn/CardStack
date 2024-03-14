using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkTransformTest : NetworkBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            float theta = Time.frameCount / 10.0f;
            transform.position = new Vector2(transform.position.x+(float)Math.Cos(theta), transform.position.y+(float)Math.Sin(theta));
        }
    }
}
