using Code.Scripts.Common.StateMachines;
using Code.Scripts.Data;
using Code.Scripts.Services;

namespace Code.Scripts.GameStates
{
    public class StartMenuState : IState
    {
        private readonly ViewsState _viewsState;
        private readonly PlayerService _playerService;

        public StartMenuState(ViewsState viewsState, PlayerService playerService)
        {
            _viewsState = viewsState;
            _playerService = playerService;
        }

        public void Enter()
        {
            _playerService.Clear();
            _viewsState.IsMainMenuVisible.Value = true;
        }

        public void Exit()
        {
            _viewsState.IsMainMenuVisible.Value = false;
        }
    }
}