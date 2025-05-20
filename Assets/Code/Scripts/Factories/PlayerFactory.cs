using Code.Scripts.Components;
using Code.Scripts.Configs;
using Code.Scripts.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Scripts.Factories
{
    public class PlayerFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly PlayerPrefabs _playerPrefabs;
        private readonly BalanceConfig _balanceConfig;
        private readonly StageService _stageService;

        public PlayerFactory(IObjectResolver objectResolver, 
            PlayerPrefabs playerPrefabs, StageService stageService, 
            BalanceConfig balanceConfig)
        {
            _objectResolver = objectResolver;
            _playerPrefabs = playerPrefabs;
            _stageService = stageService;
            _balanceConfig = balanceConfig;
        }

        public ThirdPersonController Create()
        {
            var position = _stageService.GetRandomPosition();
            var instance = _objectResolver.Instantiate(_playerPrefabs.Prefab, position, Quaternion.identity);
            
            instance.GetComponent<Health>().SetInitialValue(_balanceConfig.InitialHealth);
            
            _objectResolver.Inject(instance);
            return instance;
        }
    }
}