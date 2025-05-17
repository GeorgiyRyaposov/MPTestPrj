using Code.Scripts.Components;
using Code.Scripts.Configs;
using VContainer;
using VContainer.Unity;

namespace Code.Scripts.Factories
{
    public class PlayerFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly PlayerPrefabs _playerPrefabs;

        public PlayerFactory(IObjectResolver objectResolver, PlayerPrefabs playerPrefabs)
        {
            _objectResolver = objectResolver;
            _playerPrefabs = playerPrefabs;
        }

        public ThirdPersonController Create()
        {
            var instance = _objectResolver.Instantiate(_playerPrefabs.Prefab);
            _objectResolver.Inject(instance);
            return instance;
        }
    }
}