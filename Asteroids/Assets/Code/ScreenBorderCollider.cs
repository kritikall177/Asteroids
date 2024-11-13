using UnityEngine;

public class ScreenBorderCollider : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D _edgeCollider;
    [SerializeField] private float _screenWrapBuffer = 1f;

    private float _screenWidth;
    private float _screenHeight;

    private void Awake()
    {
        UpdateCollider();
        
        Vector2 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        _screenWidth = Mathf.Abs(screenTopRight.x - screenBottomLeft.x) / 2;
        _screenHeight = Mathf.Abs(screenTopRight.y - screenBottomLeft.y) / 2;
    }

    private void UpdateCollider()
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        _edgeCollider.points = new Vector2[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Teleportable"))
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
