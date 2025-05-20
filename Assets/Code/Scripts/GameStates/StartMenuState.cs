using Code.Scripts.Common.StateMachines;
using Code.Scripts.Data;

namespace Code.Scripts.GameStates
{
    public class StartMenuState : IState
    {
        private readonly ViewsState _viewsState;

        public StartMenuState(ViewsState viewsState)
        {
            _viewsState = viewsState;
        }

        public void Enter()
        {
            _viewsState.IsMainMenuVisible.Value = true;
        }

        public void Exit()
        {
            _viewsState.IsMainMenuVisible.Value = false;
        }
    }
}