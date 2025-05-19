using System;
using System.Collections.Generic;
using Code.Scripts.Components;
using Code.Scripts.Configs;
using Code.Scripts.Factories;
using Unity.Netcode;
using UnityEngine;

namespace Code.Scripts.Services
{
    public class PlayerService : IDisposable
    {
        private readonly NetworkManager _networkManager;
        private readonly PlayerFactory _playerFactory;
        private readonly BalanceConfig _balanceConfig;
        private readonly Dictionary<ulong, ThirdPersonController> _players = new();
        private bool _subscribedOnClientConnected;

        public PlayerService(NetworkManager networkManager, 
            PlayerFactory playerFactory, BalanceConfig balanceConfig)
        {
            _networkManager = networkManager;
            _playerFactory = playerFactory;
            _balanceConfig = balanceConfig;
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
            Debug.Log($"{_networkManager.IsServer} : Spawning player {clientId}");
            var controller = _playerFactory.Create();
            if (_networkManager.IsServer)
            {
                controller.GetComponent<Health>().SetInitialValue(_balanceConfig.InitialHealth);
            }
            
            controller.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
            
            _players[clientId] = controller;
        }

        public void OnGrounded(ulong clientId, float verticalVelocity)
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
            if (!_players.TryGetValue(clientId, out var player))
            {
                Debug.Log($"{_networkManager.IsServer}: not found player {clientId}, available {string.Join(", ", _players.Keys)}");
                return;
            }
            
            var health = player.GetComponent<Health>();
            health.SetValue(health.HealthValue + delta);
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