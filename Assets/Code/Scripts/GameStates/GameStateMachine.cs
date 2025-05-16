using Code.Scripts.Common.StateMachines;
using VContainer.Unity;

namespace Code.Scripts.GameStates
{
    public class GameStateMachine : IStartable, ITickable
    {
        private readonly StartMenuState _startMenuState;
        private readonly GameplayState _gameplayState;
        private readonly StateMachine _stateMachine = new();

        public GameStateMachine(StartMenuState startMenuState, GameplayState gameplayState)
        {
            _startMenuState = startMenuState;
            _gameplayState = gameplayState;
        }

        public void Start()
        {
            _stateMachine.SetInitialState(_startMenuState);
        }

        public void Tick()
        {
            _stateMachine.Update();
        }

        public void EnterGameplayState()
        {
            _stateMachine.Enter(_gameplayState);
        }

        public void ExitGameplayState()
        {
            _stateMachine.Enter(_startMenuState);
        }
    }
}