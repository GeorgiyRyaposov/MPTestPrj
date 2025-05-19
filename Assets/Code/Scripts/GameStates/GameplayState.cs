using Code.Scripts.Common.StateMachines;
using Code.Scripts.Services;

namespace Code.Scripts.GameStates
{
    public class GameplayState : IState
    {
        private readonly InputService _inputService;
        private readonly PlayerService _playerService;
        private readonly ItemsService _itemsService;

        public GameplayState(InputService inputService, PlayerService playerService, ItemsService itemsService)
        {
            _inputService = inputService;
            _playerService = playerService;
            _itemsService = itemsService;
        }

        public void Enter()
        {
            _playerService.Start();
            _itemsService.SpawnItems();
            _inputService.EnablePlayerActions(true);
        }

        public void Exit()
        {
            _inputService.EnablePlayerActions(false);
            _itemsService.ClearItems();
        }
    }
}