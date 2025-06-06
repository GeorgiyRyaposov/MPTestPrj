﻿using Code.Scripts.Data;
using Code.Scripts.GameStates;
using UniRx;
using Unity.Netcode;
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
        
        private NetworkManager _networkManager;
        private IGameStateChanger _gameStateChanger;
        private ViewsState _viewsState;

        [Inject]
        private void Construct(IGameStateChanger gameStateChanger, 
            ViewsState viewsState, NetworkManager networkManager)
        {
            _gameStateChanger = gameStateChanger;
            _networkManager = networkManager;
            _viewsState = viewsState;
        }

        private void Start()
        {
            _viewsState.IsGameOverVisible.Subscribe(Show).AddTo(gameObject);
            
            _respawnButton.onClick.AddListener(Respawn);
            _disconnectButton.onClick.AddListener(Disconnect);
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
            _networkManager.Shutdown();
            _gameStateChanger.EnterStartMenuState();
        }
    }
}