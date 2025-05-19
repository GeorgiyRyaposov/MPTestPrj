using System;
using Code.Scripts.Data;
using Code.Scripts.GameStates;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.Scripts.Views
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _clientButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private bool IsVisible => _canvasGroup.alpha > 0;
        
        private NetworkManager _networkManager;
        private IGameStateChanger _gameStateChanger;
        private ViewsState _viewsState;

        [Inject]
        private void Construct(NetworkManager networkManager,
            IGameStateChanger gameStateChanger, ViewsState viewsState)
        {
            _networkManager = networkManager;
            _gameStateChanger = gameStateChanger;
            _viewsState = viewsState;
        }
        
        private void Start()
        {
            _viewsState.StateChanged += OnViewsChanged;
            
            _hostButton.onClick.AddListener(OnHostButtonClicked);
            _clientButton.onClick.AddListener(OnClientButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnDestroy()
        {
            if (_viewsState != null)
            {
                _viewsState.StateChanged -= OnViewsChanged;
            }
        }

        private void OnViewsChanged()
        {
            if (IsVisible != _viewsState.IsMainMenuVisible)
            {
                Show(_viewsState.IsMainMenuVisible);
            }
        }

        private void Show(bool show)
        {
            _canvasGroup.alpha = show ? 1 : 0;
            _canvasGroup.interactable = show;
            _canvasGroup.blocksRaycasts = show;
        }

        private void OnHostButtonClicked()
        {
            if (_networkManager.StartHost())
            {
                _gameStateChanger.EnterGameplayState();
            }
        }

        private void OnClientButtonClicked()
        {
            if (_networkManager.StartClient())
            {
                _gameStateChanger.EnterGameplayState();
            }
        }
        
        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}