﻿using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Code.Scripts.Components
{
    public class PlayerDespawner : NetworkBehaviour
    {
        public void Despawn(ulong clientId)
        {
            DespawnServerRPC(clientId);
        }

        [ServerRpc]
        private void DespawnServerRPC(ulong clientId)
        {
            var playerObject = NetworkManager.SpawnManager.PlayerObjects.FirstOrDefault(x => 
                x.OwnerClientId == clientId);
            
            if (playerObject)
            {
                playerObject.Despawn();
            }
            else
            {
                Debug.LogError($"Failed to despawn player {clientId}");
            }
        }
    }
}