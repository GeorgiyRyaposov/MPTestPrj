using System;
using System.Collections.Generic;
using Unity.Netcode;

namespace Code.Scripts.Components
{
    public class Health : NetworkBehaviour
    {
        private readonly NetworkVariable<int> _health = new ();
        
        private readonly List<Action<int, int>> _callbacks = new ();
        private int _initialHealth;

        public int Value => _health.Value;

        public override void OnNetworkSpawn()
        {
            if (NetworkManager.IsServer)
            {
                _health.Value = _initialHealth;
            }
            
            _health.OnValueChanged += OnHealthChanged;
        }

        public void SubscribeOnValueChanged(Action<int, int> action)
        {
            _callbacks.Add(action);
        }
        
        public void UnsubscribeOnValueChanged(Action<int, int> action)
        {
            _callbacks.Remove(action);
        }

        public void SetInitialValue(int health)
        {
            _initialHealth = health;
        }
        public void SetValue(int health)
        {
            _health.Value = health;
        }

        private void OnHealthChanged(int previousValue, int newValue)
        {
            for (int i = 0; i < _callbacks.Count; i++)
            {
                _callbacks[i](previousValue, newValue);
            }
        }
    }
}