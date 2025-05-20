using System;
using System.Linq;
using Code.Scripts.Components;
using Code.Scripts.Configs;
using Code.Scripts.Messages;
using UniRx;
using Unity.Netcode;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Code.Scripts.Services
{
    public class PlayerService : IDisposable, IInitializable
    {
        private readonly NetworkManager _networkManager;
        private readonly BalanceConfig _balanceConfig;
        private readonly PlayerPrefabs _playerPrefabs;

        public PlayerService(NetworkManager networkManager, 
            BalanceConfig balanceConfig, PlayerPrefabs playerPrefabs)
        {
            _networkManager = networkManager;
            _balanceConfig = balanceConfig;
            _playerPrefabs = playerPrefabs;
        }
        
        public void Initialize()
        {
            if (_networkManager.IsServer)
            {
                _networkManager.OnClientConnectedCallback += OnClientConnectedCallback;
            }

            _networkManager.OnClientDisconnectCallback += OnClientDisconnect;
        }
        
        public void Start()
        {
            if (_networkManager.IsServer)
            {
                var playerSpawner = Object.Instantiate(_playerPrefabs.PlayerSpawnerPrefab);
                playerSpawner.NetworkObject.Spawn();
                
                _networkManager.OnClientConnectedCallback += OnClientConnectedCallback;
            }
            
            MessageBroker.Default.Publish(new SpawnPlayerMessage { ClientId = _networkManager.LocalClientId });
        }

        private void OnClientConnectedCallback(ulong clientId)
        {
            MessageBroker.Default.Publish(new SpawnPlayerMessage { ClientId = clientId });
        }

        public void Dispose()
        {
            if (_networkManager)
            {
                _networkManager.OnClientConnectedCallback -= OnClientConnectedCallback;
                _networkManager.OnClientDisconnectCallback -= OnClientDisconnect;
            }
        }

        private void OnClientDisconnect(ulong clientId)
        {
            if (_networkManager.LocalClientId == clientId)
            {
                MessageBroker.Default.Publish(new GameDisconnectedMessage());
            }
        }
        
        public void DespawnPlayer()
        {
            var clientId = _networkManager.LocalClientId;
            if (TryGetPlayerObject(clientId, out var playerObject))
            {
                playerObject.GetComponent<PlayerDespawner>().Despawn(clientId);
            }
            else
            {
                Debug.LogError($"[DespawnPlayer] Failed to find player {clientId}");
            }
        }

        public void OnCharacterGrounded(ulong clientId, float verticalVelocity)
        {
            if (verticalVelocity < _balanceConfig.VelocityFallDamage)
            {
                ApplyFallDamage(clientId);
            }
        }

        private void ApplyFallDamage(ulong clientId)
        {
            AddHealthToPlayer(clientId, -_balanceConfig.FallDamageValue);
        }

        public void HealPlayer(ulong clientId)
        {
            if (_networkManager.IsServer)
            {
                AddHealthToPlayer(clientId, _balanceConfig.HealValue);
            }
        }
        
        private void AddHealthToPlayer(ulong clientId, int delta)
        {
            if (!TryGetPlayerObject(clientId, out var player))
            {
                Debug.Log($"{_networkManager.IsServer}: not found player {clientId}");
                return;
            }
            
            var health = player.GetComponent<Health>();
            health.SetValue(health.Value + delta);
        }
        
        private bool TryGetPlayerObject(ulong clientId, out NetworkObject playerObject)
        {
            playerObject = _networkManager.SpawnManager.PlayerObjects.FirstOrDefault(x => x.OwnerClientId == clientId);
            return playerObject != null;
        }
    }
}