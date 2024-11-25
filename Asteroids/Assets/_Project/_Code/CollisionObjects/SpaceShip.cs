using _Project._Code.CollisionComponents;
using _Project._Code.Signals;
using UnityEngine;
using Zenject;

namespace _Project._Code.CollisionObjects
{
    public class SpaceShip : MonoBehaviour, IPlayerComponent, ITeleportableComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private GameObject _laserGameObject;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public GameObject LaserGameObject => _laserGameObject;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IDestructibleComponent>(out _))
            {
                _signalBus.Fire<GameOverSignal>();
            }
        }
    }
}