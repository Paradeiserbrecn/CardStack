using Unity.Netcode;
using UnityEngine;

public class Rat : NetworkBehaviour
{
    public float speed = 0.5f;
    public NetworkVariable<Vector2> Position = new NetworkVariable<Vector2>();
    // Start is called before the first frame update

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Move();
        }
    }

    private void Move()
    {
        SubmitPostRequestServerRpc();
    }

    [Rpc(SendTo.Server)]
    private void SubmitPostRequestServerRpc(RpcParams rpcParams = default)
    {
        var randomPosition = GetRandomPositionOnScreen();
        transform.position = randomPosition;
        Position.Value = randomPosition;
    }


    static Vector2 GetRandomPositionOnScreen()
    {
        return new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Position.Value;
    }

}
