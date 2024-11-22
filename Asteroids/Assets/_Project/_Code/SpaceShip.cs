using System;
using Code.Signals;
using UnityEngine;
using Zenject;

namespace Code
{
    public class SpaceShip : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Destructible"))
            {
                _signalBus.Fire<GameOverSignal>();
            }
        }
    }
}