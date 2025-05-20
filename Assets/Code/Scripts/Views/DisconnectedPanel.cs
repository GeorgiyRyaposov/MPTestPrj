using Code.Scripts.Data;
using Code.Scripts.GameStates;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.Scripts.Views
{
    public class DisconnectedPanel : MonoBehaviour
    {
        [SerializeField] private Button _toStartMenuButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private IGameStateChanger _gameStateChanger;
        private ViewsState _viewsState;

        [Inject]
        private void Construct(IGameStateChanger gameStateChanger, 
            ViewsState viewsState)
        {
            _gameStateChanger = gameStateChanger;
            _viewsState = viewsState;
        }

        private void Start()
        {
            _viewsState.IsDisconnectedPanelVisible.Subscribe(Show).AddTo(gameObject);
            
            _toStartMenuButton.onClick.AddListener(ToStartMenu);
        }
        
        private void Show(bool show)
        {
            _canvasGroup.alpha = show ? 1 : 0;
            _canvasGroup.interactable = show;
            _canvasGroup.blocksRaycasts = show;
        }
        
        private void ToStartMenu()
        {
            _gameStateChanger.EnterStartMenuState();
        }
    }
}