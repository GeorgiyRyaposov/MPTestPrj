using System.Collections.Generic;
using Code.Scripts.Components;
using Code.Scripts.Configs;
using Code.Scripts.Factories;
using Unity.Netcode;

namespace Code.Scripts.Services
{
    public class ItemsService
    {
        private readonly FirstAidKitFactory _firstAidKitFactory;
        private readonly BalanceConfig _balanceConfig;
        private readonly NetworkManager _networkManager;
        
        private readonly List<FirstAidKit> _aidKits = new ();

        public ItemsService(FirstAidKitFactory firstAidKitFactory, BalanceConfig balanceConfig, NetworkManager networkManager)
        {
            _firstAidKitFactory = firstAidKitFactory;
            _balanceConfig = balanceConfig;
            _networkManager = networkManager;
        }

        public void SpawnItems()
        {
            if (!_networkManager.IsServer)
            {
                return;
            }
            
            for (int i = 0; i < _balanceConfig.FirstAidKitCount; i++)
            {
                var instance = _firstAidKitFactory.Create();
                instance.NetworkObject.Spawn();
                _aidKits.Add(instance);
            }
        }

        public void ClearItems()
        {
            if (!_networkManager.IsServer)
            {
                return;
            }
            
            foreach (var aidKit in _aidKits)
            {
                if (aidKit)
                {
                    aidKit.NetworkObject.Despawn();
                }
            }
            
            _aidKits.Clear();
        }
    }
}