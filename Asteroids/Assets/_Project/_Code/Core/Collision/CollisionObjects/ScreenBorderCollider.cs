using _Project._Code.Core.Collision.CollisionComponents;
using _Project._Code.Meta.DataConfig.Configs;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Collision.CollisionObjects
{
    public class ScreenBorderCollider : MonoBehaviour
    {
        [SerializeField] private EdgeCollider2D _edgeCollider;

        private IScreenWrapBuffer _screenWrapBuffer;

        private float _screenWidth;
        private float _screenHeight;

        private Camera _mainCamera;

        [Inject]
        public void Construct(IScreenWrapBuffer screenWrapBuffer)
        {
            _screenWrapBuffer = screenWrapBuffer;
        }

        private void Awake()
        {
            _mainCamera = Camera.main;

            UpdateCollider();

            Vector2 screenBottomLeft = _mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 screenTopRight = _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            _screenWidth = Mathf.Abs(screenTopRight.x - screenBottomLeft.x) / 2;
            _screenHeight = Mathf.Abs(screenTopRight.y - screenBottomLeft.y) / 2;
        }

        private void UpdateCollider()
        {
            Vector2 bottomLeft = _mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 topLeft = _mainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
            Vector2 topRight = _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            Vector2 bottomRight = _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));

            _edgeCollider.points = new Vector2[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent<ITeleportableComponent>(out _))
            {
                ScreenWrapping(collider);
            }
        }

        private void ScreenWrapping(Collider2D collision)
        {
            Vector3 newPos = collision.transform.position;

            if (newPos.x > _screenWidth - _screenWrapBuffer.ScreenWrapBuffer)
            {
                newPos.x = -_screenWidth;
            }
            else if (newPos.x < -_screenWidth + _screenWrapBuffer.ScreenWrapBuffer)
            {
                newPos.x = _screenWidth;
            }

            if (newPos.y > _screenHeight - _screenWrapBuffer.ScreenWrapBuffer)
            {
                newPos.y = -_screenHeight;
            }
            else if (newPos.y < -_screenHeight + _screenWrapBuffer.ScreenWrapBuffer)
            {
                newPos.y = _screenHeight;
            }

            collision.transform.position = newPos;
        }
    }
}