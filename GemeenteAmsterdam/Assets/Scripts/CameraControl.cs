using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Camera Zoom")]
    [SerializeField] private float zoomOutMin;
    [SerializeField] private float zoomOutMax;
    [SerializeField] private float zoomSpeed;

    [Header("Map")]
    [SerializeField] private GameObject mapGameObject;
    [SerializeField] private Transform startPoint;

    private float leftLimit;
    private float rightLimit;
    private float topLimit;
    private float bottomLimit;
    private float cameraDistanceLimit = 0; // This will never change, this will stay at 0.
    private float cameraBottomLimit = -10; // This is how far the camera is from the ground, and won't change.
    private Vector3 touchStart;

    private void Awake() 
    { 
        Camera.main.orthographicSize = zoomOutMax; // Set the camera to the maximum zoom out value.
        SetCameraCenter(startPoint.position);
        SpriteBounds(mapGameObject); 
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition); }
        if (Input.touchCount == 2) {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * zoomSpeed);
            SpriteBounds(mapGameObject); 
        }
        else if (Input.GetMouseButton(0)) {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += direction;
        }
        CameraBounds();
    }
    void Zoom(float increment) { Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax); } 
    /// <summary>
    /// Sets the camera bounds based on the sprite's bounds and the camera's extents.
    /// </summary>
    /// <param name="_gameObject">The GameObject whose sprite bounds will be used to calculate the camera bounds.</param>
    private void SpriteBounds(GameObject _gameObject) { 

        var _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        
        // Update the camera extents based on the new position
        float _vertExtent = Camera.main.orthographicSize;
        float _horiExtent = _vertExtent * Screen.width / Screen.height;

        // Calculate the new limits based on the sprite's bounds and the camera's extents
        leftLimit = _gameObject.transform.position.x - (_spriteRenderer.bounds.size.x / 2.0f) + _horiExtent;
        rightLimit = _gameObject.transform.position.x + (_spriteRenderer.bounds.size.x / 2.0f) - _horiExtent;
        bottomLimit = _gameObject.transform.position.y - (_spriteRenderer.bounds.size.y / 2.0f) + _vertExtent;
        topLimit = _gameObject.transform.position.y + (_spriteRenderer.bounds.size.y / 2.0f) - _vertExtent;

    }
    /// <summary>
    /// This function is used to keep the camera within the defined bounds.
    /// </summary>
    private void CameraBounds() { 
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
        Mathf.Clamp(transform.position.y, bottomLimit, topLimit), 
        Mathf.Clamp(transform.position.z, cameraBottomLimit, cameraDistanceLimit)); 
    }
    /// <summary>
    /// This function is used to set the center of the camera.
    /// </summary>
    /// <param name="center">The new center position for the camera.</param>
    private void SetCameraCenter(Vector3 center) { Camera.main.transform.position = new Vector3(center.x, center.y, Camera.main.transform.position.z); }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }
}

