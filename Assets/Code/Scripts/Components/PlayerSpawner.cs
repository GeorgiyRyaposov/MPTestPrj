using Code.Scripts.Factories;
using Code.Scripts.Messages;
using UniRx;
using Unity.Netcode;
using VContainer;

namespace Code.Scripts.Components
{
    public class PlayerSpawner : NetworkBehaviour
    {
        private PlayerFactory _playerFactory;
        private bool _injected;
        
        [Inject]
        private void Construct(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;

            _injected = true;
        }

        private void Awake()
        {
            MessageBroker.Default.Receive<SpawnPlayerMessage>()
                .Subscribe(x => SpawnPlayer(x.ClientId))
                .AddTo(gameObject);
        }

        public override void OnNetworkSpawn()
        {
            TriggerInjection();
        }

        public void SpawnPlayer(ulong clientId)
        {
            SpawnForClientServerRpc(clientId);
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void SpawnForClientServerRpc(ulong clientId)
        {
            //Debug.Log($"{NetworkManager.IsServer} : Spawning player {clientId}");
            var controller = _playerFactory.Create();
            controller.NetworkObject.SpawnAsPlayerObject(clientId);
        }
        
        private void TriggerInjection()
        {
            if (!_injected)
            {
                NetworkItemsInjector.Inject(this);
            }
        }
    }
}