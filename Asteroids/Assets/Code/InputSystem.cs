using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code
{
    public class InputSystem : IInputSystem, InputSystem_Actions.IPlayerActions
    {
        private readonly InputSystem_Actions _inputSystemActions;
        public event Action<Vector2> OnMoveEvent;
        public event Action<Vector2> OnLookEvent;
        
        
        
        public InputSystem()
        {
            _inputSystemActions = new InputSystem_Actions();
            _inputSystemActions.Enable();
            _inputSystemActions.Player.SetCallbacks(this);
            _inputSystemActions.Player.Enable();
        }
        
        public void OnMove(InputAction.CallbackContext context) => 
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());

        public void OnLook(InputAction.CallbackContext context) => 
            OnLookEvent?.Invoke(context.ReadValue<Vector2>());

        public void OnAttack(InputAction.CallbackContext context)
        {
            
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            
        }

        public void OnNext(InputAction.CallbackContext context)
        {
           
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
           
        }
    }

    public interface IInputSystem
    {
        public event Action<Vector2> OnMoveEvent;
        public event Action<Vector2> OnLookEvent;
    }
}