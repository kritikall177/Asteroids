using UnityEngine;

namespace Code
{
    public class AggroZoneSystem : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider2D;
        [SerializeField] private FlyingSaucer _flyingSaucer;
        [SerializeField] private float _triggerRadius = 3f;

        private void Start()
        {
            _collider2D.radius = _triggerRadius;
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player")) 
                _flyingSaucer.SetTarget(collider.gameObject.transform);
        }
    }
}