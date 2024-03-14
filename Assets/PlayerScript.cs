using Unity.Netcode;
using UnityEngine;

public class PlayerScript : NetworkBehaviour
{
    private HandScript hand;
    private ulong clientId;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -Camera.main.orthographicSize, 0);
        hand = GetComponentInChildren<HandScript>();

        //TODO Set ownerId with NetCode
        clientId = NetworkManager.LocalClientId;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hand.DrawCardFromTopRpc(clientId);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer && IsOwner) //Only send an RPC to the server on the client that owns the NetworkObject that owns this NetworkBehaviour instance
        {
            TestServerRpc(0, NetworkObjectId);
        }
    }

    [Rpc(SendTo.Server)]
    void TestServerRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Server Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        TestClientRpc(value, sourceNetworkObjectId);
    }

    [Rpc(SendTo.ClientsAndHost)]
    void TestClientRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Client Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        if (IsOwner) //Only send an RPC to the server on the client that owns the NetworkObject that owns this NetworkBehaviour instance
        {
            TestServerRpc(value + 1, sourceNetworkObjectId);
        }
    }
}
