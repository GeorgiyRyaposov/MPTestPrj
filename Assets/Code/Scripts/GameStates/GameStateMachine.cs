using Code.Scripts.Common.StateMachines;
using Code.Scripts.Messages;
using UniRx;
using VContainer.Unity;

namespace Code.Scripts.GameStates
{
    public interface IGameStateChanger 
    {
        void EnterGameplayState();
        void EnterStartMenuState();
    }
    
    public class GameStateMachine : IGameStateChanger, IStartable, ITickable
    {
        private readonly StartMenuState _startMenuState;
        private readonly GameplayState _gameplayState;
        private readonly GameOverState _gameOverState;
        private readonly DisconnectedState _disconnectedState;
        private readonly StateMachine _stateMachine = new();

        public GameStateMachine(StartMenuState startMenuState, 
            GameplayState gameplayState, GameOverState gameOverState, 
            DisconnectedState disconnectedState)
        {
            _startMenuState = startMenuState;
            _gameplayState = gameplayState;
            _gameOverState = gameOverState;
            _disconnectedState = disconnectedState;
        }

        public void Start()
        {
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(_ => EnterGameOverState());
            MessageBroker.Default.Receive<GameDisconnectedMessage>().Subscribe(_ => EnterDisconnectedState());
            
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

        private void EnterGameOverState()
        {
            _stateMachine.Enter(_gameOverState);
        }
        
        private void EnterDisconnectedState()
        {
            _stateMachine.Enter(_disconnectedState);
        }
    }
}