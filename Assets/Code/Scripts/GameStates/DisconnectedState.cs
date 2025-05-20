using Code.Scripts.Common.StateMachines;
using Code.Scripts.Data;

namespace Code.Scripts.GameStates
{
    public class DisconnectedState : IState
    {
        private readonly ViewsState _viewsState;

        public DisconnectedState(ViewsState viewsState)
        {
            _viewsState = viewsState;
        }

        public void Enter()
        {
            _viewsState.IsDisconnectedPanelVisible.Value = true;
        }

        public void Exit()
        {
            _viewsState.IsDisconnectedPanelVisible.Value = false;
        }
    }
}