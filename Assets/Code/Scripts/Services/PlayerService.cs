using Code.Scripts.Factories;
using Unity.Netcode;

namespace Code.Scripts.Services
{
    public class PlayerService
    {
        private readonly NetworkManager _networkManager;
        private readonly PlayerFactory _playerFactory;

        public PlayerService(NetworkManager networkManager, PlayerFactory playerFactory)
        {
            _networkManager = networkManager;
            _playerFactory = playerFactory;
        }

        public void SpawnPlayer()
        {
            var controller = _playerFactory.Create();
            controller.NetworkObject.SpawnAsPlayerObject(_networkManager.LocalClientId);
        }
    }
}