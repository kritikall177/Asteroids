using _Project._Code.Collision.CollisionComponents;
using UnityEngine;
using Zenject;

namespace _Project._Code.Collision.CollisionObjects.PlayerShip
{
    public class SpaceShip : MonoBehaviour, IPlayerComponent, ITeleportableComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private GameObject _laserGameObject;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public GameObject LaserGameObject => _laserGameObject;

        private SpaceShipDependencies _dependencies;

        [Inject]
        public void Construct(SpaceShipDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IDestructibleComponent>(out _))
            {
                _dependencies.HandleDestroyed();
            }
        }
    }
}