using Unity.Netcode;
using UnityEngine;

public class RpcPlayer : NetworkBehaviour
{
    public NetworkVariable<int> spacePressed = new(0);
    

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IncreaseAndLogSpacesPressedRpc();
            }
        }
    }

    [Rpc(SendTo.Server)]
    void IncreaseAndLogSpacesPressedRpc ()
    {
        spacePressed.Value += 1;
        LogSpacesPressedRpc();
    }

    [Rpc(SendTo.ClientsAndHost)]
    void LogSpacesPressedRpc ()
    {
        Debug.Log($"Player {NetworkManager.LocalClientId} pressed space {spacePressed.Value} times");

    }
}
