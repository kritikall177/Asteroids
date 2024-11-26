using System;
using UnityEngine;

namespace _Project._Code.System.InputSystem
{
    public interface IInputSystem   
    {
        public event Action<bool> OnMoveEvent;
        public event Action<Vector2> OnLookEvent;
        public event Action<bool> OnAttackEvent;
        public event Action<bool> OnHeavyAttackEvent;
    }
}