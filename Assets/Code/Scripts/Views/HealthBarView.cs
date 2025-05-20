using Code.Scripts.Components;
using TMPro;
using UnityEngine;

namespace Code.Scripts.Views
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthBarLabel;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _root;
        [SerializeField] private Health _health;
        
        private Transform _cameraRoot;

        private void Start()
        {
            var main = Camera.main;
            _canvas.worldCamera = main;
            _cameraRoot = main.transform;
            
            _health.SubscribeOnValueChanged(OnHealthChanged);
            SetHealthValue(_health.Value);
        }

        private void OnDestroy()
        {
            Unsubscribe(_health);
        }

        private void Unsubscribe(Health health)
        {
            if (health)
            {
                health.UnsubscribeOnValueChanged(OnHealthChanged);
            }
        }

        private void OnHealthChanged(int previousValue, int newValue)
        {
            SetHealthValue(newValue);
        }

        private void SetHealthValue(int value)
        {
            _healthBarLabel.text = $"HP: {value}";
        }

        private void Update()
        {
            _root.forward = _cameraRoot.forward;
        }
    }
}