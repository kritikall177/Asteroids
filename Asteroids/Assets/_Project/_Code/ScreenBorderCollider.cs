using UnityEngine;

namespace _Project._Code
{
    public class ScreenBorderCollider : MonoBehaviour
    {
        [SerializeField] private EdgeCollider2D _edgeCollider;
    
        [SerializeField] private float _screenWrapBuffer = 1f;

        private float _screenWidth;
        private float _screenHeight;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
            
            UpdateCollider();
        
            Vector2 screenBottomLeft = _mainCamera.ScreenToWorldPoint(new Vector2(0, 0)); // Используем кешированную камеру
            Vector2 screenTopRight = _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); // Используем кешированную камеру
        
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
            if (collider.gameObject.layer == LayerMask.NameToLayer("Teleportable"))
            {
                ScreenWrapping(collider);
            }
        }

        private void ScreenWrapping(Collider2D collision)
        {
            Vector3 newPos = collision.transform.position;
        
            if (newPos.x > _screenWidth - _screenWrapBuffer)
            {
                newPos.x = -_screenWidth;
            }
            else if (newPos.x < -_screenWidth + _screenWrapBuffer)
            {
                newPos.x = _screenWidth;
            }
        
            if (newPos.y > _screenHeight - _screenWrapBuffer)
            {
                newPos.y = -_screenHeight;
            }
            else if (newPos.y < -_screenHeight + _screenWrapBuffer)
            {
                newPos.y = _screenHeight;
            }

            collision.transform.position = newPos;
        }
    }
}
