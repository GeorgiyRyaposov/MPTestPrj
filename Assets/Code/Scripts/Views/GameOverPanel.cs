using System;
using Code.Scripts.Data;
using Code.Scripts.GameStates;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.Scripts.Views
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private Button _respawnButton;
        [SerializeField] private Button _disconnectButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private bool IsVisible => _canvasGroup.alpha > 0;

        private IGameStateChanger _gameStateChanger;
        private ViewsState _viewsState;

        [Inject]
        private void Construct(IGameStateChanger gameStateChanger, ViewsState viewsState)
        {
            _gameStateChanger = gameStateChanger;
            _viewsState = viewsState;
        }

        private void Start()
        {
            _viewsState.StateChanged += OnViewsChanged;
            
            _respawnButton.onClick.AddListener(Respawn);
            _disconnectButton.onClick.AddListener(Disconnect);
        }
        
        private void OnViewsChanged()
        {
            if (IsVisible != _viewsState.IsMainMenuVisible)
            {
                Show(_viewsState.IsGameOverVisible);
            }
        }
        
        private void Show(bool show)
        {
            _canvasGroup.alpha = show ? 1 : 0;
            _canvasGroup.interactable = show;
            _canvasGroup.blocksRaycasts = show;
        }

        private void Respawn()
        {
            _gameStateChanger.EnterGameplayState();
        }
        
        private void Disconnect()
        {
            _gameStateChanger.EnterStartMenuState();
        }
    }
}