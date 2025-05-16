using Code.Scripts.InputListeners;

namespace Code.Scripts.Services
{
    public class InputService
    {
        private readonly InputActionsAsset _actions;
        
        public InputService(PlayerActionsListener playerActions)
        {
            _actions = new InputActionsAsset();
            _actions.Player.AddCallbacks(playerActions);
        }
        
        public void EnablePlayerActions(bool enable)
        {
            if (enable)
            {
                _actions.Player.Enable();
            }
            else
            {
                _actions.Player.Disable();
            }
        }
    }
}