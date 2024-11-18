using System;
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

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Destructible"))
            {
                _signalBus.Fire<GameOverSignal>();
            }
        }
    }
}