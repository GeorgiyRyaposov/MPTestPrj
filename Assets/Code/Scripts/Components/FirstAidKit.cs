using Code.Scripts.Services;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Code.Scripts.Components
{
    public class FirstAidKit : NetworkBehaviour
    {
        [SerializeField] private Transform _mesh;
        [SerializeField] private float _rotationSpeed = 2;
        
        private PlayerService _playerService;
        private bool _injected;
        private float _angle;

        [Inject]
        private void Construct(PlayerService playerService)
        {
            _playerService = playerService;

            _injected = true;
        }

        private void Update()
        {
            _angle += Time.deltaTime * _rotationSpeed;
            _mesh.Rotate(Vector3.up, _angle, Space.World);
        }

        protected override void OnNetworkPostSpawn()
        {
            TriggerInjection();
        }

        private void TriggerInjection()
        {
            if (!_injected)
            {
                NetworkItemsInjector.Inject(this);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!NetworkManager.IsServer)
            {
                return;
            }
            
            if (other.CompareTag("Player"))
            {
                var playerNetworkObject = other.GetComponent<NetworkObject>();
                OnPlayerEnterServerRpc(playerNetworkObject.OwnerClientId, NetworkObjectId);
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void OnPlayerEnterServerRpc(ulong clientId, ulong objectId)
        {
            _playerService.HealPlayer(clientId);
            NetworkObject.Despawn();
        }
    }
}