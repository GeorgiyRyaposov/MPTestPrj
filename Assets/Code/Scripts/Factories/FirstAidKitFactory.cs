using Code.Scripts.Components;
using Code.Scripts.Configs;
using Code.Scripts.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Scripts.Factories
{
    public class FirstAidKitFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly StageItems _stageItems;
        private readonly StageService _stageService;

        public FirstAidKitFactory(IObjectResolver objectResolver, 
            StageItems stageItems, StageService stageService)
        {
            _objectResolver = objectResolver;
            _stageItems = stageItems;
            _stageService = stageService;
        }
        
        public FirstAidKit Create()
        {
            var position = _stageService.GetRandomPosition();
            var instance = _objectResolver.Instantiate(_stageItems.FirstAidKitPrefab, position, Quaternion.identity);
            _objectResolver.Inject(instance);
            return instance;
        }
    }
}