using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project._Code.System
{
    public class InputSystem : IInputSystem, InputSystem_Actions.IPlayerActions
    {
        private readonly InputSystem_Actions _inputSystemActions;
        
        public event Action<bool> OnMoveEvent;
        public event Action<Vector2> OnLookEvent;
        public event Action<bool> OnAttackEvent;
        public event Action<bool> OnHeavyAttackEvent;
        
        
        
        public InputSystem()
        {
            _inputSystemActions = new InputSystem_Actions();
            _inputSystemActions.Enable();
            _inputSystemActions.Player.SetCallbacks(this);
            _inputSystemActions.Player.Enable();
        }
        
        public void OnMove(InputAction.CallbackContext context) => 
            OnMoveEvent?.Invoke(context.ReadValueAsButton());

        public void OnLook(InputAction.CallbackContext context) => 
            OnLookEvent?.Invoke(context.ReadValue<Vector2>());

        public void OnAttack(InputAction.CallbackContext context) => 
            OnAttackEvent?.Invoke(context.ReadValueAsButton());

        public void OnHeavyAttack(InputAction.CallbackContext context) => 
            OnHeavyAttackEvent?.Invoke(context.ReadValueAsButton());
    }
}