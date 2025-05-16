using Code.Scripts.GameStates;

namespace Code.Scripts.Common.StateMachines
{
    public class StateMachine
    {
        public IState ActiveState => _activeState;
        public IState PrevState => _prevState;
        
        private IState _prevState;
        private IState _activeState;
        
        public void SetInitialState(IState state)
        {
            _activeState = state;
            _activeState.Enter();
        }

        public void Enter(IState state)
        {
            if (_activeState == state)
            {
                return;
            }
            
            _prevState = _activeState;
            _activeState = state;

            //Debug.Log($"Change {prevState.GetType()} -> {state.GetType()}");
            
            _prevState.Exit();
            _activeState.Enter();
        }
        
        public void Update()
        {
            _activeState.OnUpdate();
        }
    }
}