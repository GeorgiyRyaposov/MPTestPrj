using Code.Scripts.Common.StateMachines;
using Code.Scripts.Data;
using VContainer.Unity;

namespace Code.Scripts.GameStates
{
    public interface IGameStateChanger 
    {
        void EnterGameplayState();
        void EnterStartMenuState();
        void EnterGameOverState();
    }
    
    public class GameStateMachine : IGameStateChanger, IStartable, ITickable
    {
        private readonly StartMenuState _startMenuState;
        private readonly GameplayState _gameplayState;
        private readonly GameOverState _gameOverState;
        private readonly StateMachine _stateMachine = new();

        public GameStateMachine(StartMenuState startMenuState, 
            GameplayState gameplayState, GameOverState gameOverState)
        {
            _startMenuState = startMenuState;
            _gameplayState = gameplayState;
            _gameOverState = gameOverState;
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

        public void EnterStartMenuState()
        {
            _stateMachine.Enter(_startMenuState);
        }

        public void EnterGameOverState()
        {
            _stateMachine.Enter(_gameOverState);
        }
    }
}