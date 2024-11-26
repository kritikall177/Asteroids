using _Project._Code.CollisionComponents;
using _Project._Code.System.GameState;
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

        private IGameStateActionsInvoker _gameStateActionsInvoker;

        [Inject]
        public void Construct(IGameStateActionsInvoker gameStateActionsInvoker)
        {
            _gameStateActionsInvoker = gameStateActionsInvoker;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IDestructibleComponent>(out _))
            {
                _gameStateActionsInvoker.StopGame();
            }
        }
    }
}