using Code.Scripts.Common.StateMachines;
using Code.Scripts.Services;

namespace Code.Scripts.GameStates
{
    public class GameplayState : IState
    {
        private readonly InputService _inputService;
        private readonly PlayerService _playerService;

        public GameplayState(InputService inputService, PlayerService playerService)
        {
            _inputService = inputService;
            _playerService = playerService;
        }

        public void Enter()
        {
            _playerService.Start();
            _inputService.EnablePlayerActions(true);
        }

        public void Exit()
        {
            _inputService.EnablePlayerActions(false);
        }
    }
}