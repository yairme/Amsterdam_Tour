using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Camera Zoom")]
    [SerializeField] private float ZoomOutMin;
    [SerializeField] private float ZoomOutMax;
    [SerializeField] private float ZoomSpeed;

    [Header("Map")]
    [SerializeField] private GameObject MapGameObject;
    [SerializeField] private Transform StartPoint;

    private float LeftLimit;
    private float RightLimit;
    private float TopLimit;
    private float BottomLimit;
    private float CameraDistanceLimit = 0; // This will never change, this will stay at 0.
    private float CameraBottomLimit = -10; // This is how far the camera is from the ground, and won't change.
    private float StartDistance = 300;
    private Vector3 touchStart;

    private void Awake() 
    { 
        Camera.main.orthographicSize = StartDistance; // Set the camera a good distance away from the map.
        SetCameraCenter(StartPoint.position);
        SpriteBounds(MapGameObject); 
    }
    void Update()
    {
        if (Input.touchCount == 1) 
        { 
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); 
            }
        }
        if (Input.touchCount == 2) 
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * ZoomSpeed);
            SpriteBounds(MapGameObject); 
        }
        else if (Input.touchCount == 1) 
        {

            if (Input.GetTouch(0).phase == TouchPhase.Moved) {

                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                transform.position += direction;
            }
        }
        CameraBounds();
    }
    void Zoom(float _increment) { Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - _increment, ZoomOutMin, ZoomOutMax); } 
    /// <summary>
    /// Sets the camera bounds based on the sprite's bounds and the camera's extents.
    /// </summary>
    /// <param name="_gameObject">The GameObject whose sprite bounds will be used to calculate the camera bounds.</param>
    private void SpriteBounds(GameObject _gameObject) 
    { 

        var _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        
        // Update the camera extents based on the new position
        float _vertExtent = Camera.main.orthographicSize;
        float _horiExtent = _vertExtent * Screen.width / Screen.height;

        // Calculate the new limits based on the sprite's bounds and the camera's extents
        LeftLimit = _gameObject.transform.position.x - (_spriteRenderer.bounds.size.x / 2.0f) + _horiExtent;
        RightLimit = _gameObject.transform.position.x + (_spriteRenderer.bounds.size.x / 2.0f) - _horiExtent;
        BottomLimit = _gameObject.transform.position.y - (_spriteRenderer.bounds.size.y / 2.0f) + _vertExtent;
        TopLimit = _gameObject.transform.position.y + (_spriteRenderer.bounds.size.y / 2.0f) - _vertExtent;

    }
    /// <summary>
    /// This function is used to keep the camera within the defined bounds.
    /// </summary>
    private void CameraBounds() 
    { 
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, LeftLimit, RightLimit), 
        Mathf.Clamp(transform.position.y, BottomLimit, TopLimit), 
        Mathf.Clamp(transform.position.z, CameraBottomLimit, CameraDistanceLimit)); 
    }
    /// <summary>
    /// This function is used to set the center of the camera.
    /// </summary>
    /// <param name="_center">The new center position for the camera.</param>
    private void SetCameraCenter(Vector3 _center) { Camera.main.transform.position = new Vector3(_center.x, _center.y, Camera.main.transform.position.z); }
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(LeftLimit, TopLimit), new Vector2(RightLimit, TopLimit));
        Gizmos.DrawLine(new Vector2(RightLimit, TopLimit), new Vector2(RightLimit, BottomLimit));
        Gizmos.DrawLine(new Vector2(RightLimit, BottomLimit), new Vector2(LeftLimit, BottomLimit));
        Gizmos.DrawLine(new Vector2(LeftLimit, BottomLimit), new Vector2(LeftLimit, TopLimit));
    }
}

