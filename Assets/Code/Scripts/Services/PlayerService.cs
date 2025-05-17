using System;
using Code.Scripts.Factories;
using Unity.Netcode;
using UnityEngine;

namespace Code.Scripts.Services
{
    public class PlayerService : IDisposable
    {
        private readonly NetworkManager _networkManager;
        private readonly PlayerFactory _playerFactory;
        private bool _subscribedOnClientConnected;

        public PlayerService(NetworkManager networkManager, PlayerFactory playerFactory)
        {
            _networkManager = networkManager;
            _playerFactory = playerFactory;
        }
        
        public void Start()
        {
            if (_networkManager.IsServer)
            {
                SpawnPlayer(_networkManager.LocalClientId);
                SubscribeOnClientConnected();
            }
        }

        public void Dispose()
        {
            if (_networkManager)
            {
                _networkManager.OnClientConnectedCallback -= SpawnPlayer;
            }
        }
        
        private void SpawnPlayer(ulong clientId)
        {
            Debug.Log($"Spawning player {clientId}");
            var controller = _playerFactory.Create();
            controller.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        }
        

        private void SubscribeOnClientConnected()
        {
            if (_subscribedOnClientConnected)
            {
                return;
            }
            
            _networkManager.OnClientConnectedCallback += SpawnPlayer;
            _subscribedOnClientConnected = true;
        }
    }
}