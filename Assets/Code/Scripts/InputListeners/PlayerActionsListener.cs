using Code.Scripts.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.InputListeners
{
    public class PlayerActionsListener : InputActionsAsset.IPlayerActions
    {
        private readonly InputState _inputState;

        public PlayerActionsListener(InputState inputState)
        {
            _inputState = inputState;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            _inputState.Move = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _inputState.Look = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            _inputState.Jump = context.ReadValueAsButton();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            _inputState.Sprint = context.ReadValueAsButton();
        }
    }
}