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

    static void SubmitNewPosition()
    {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
        {
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            var player = playerObject.GetComponent<Rat>();
            player.Move();
        }
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
