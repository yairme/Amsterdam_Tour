using UnityEngine;

public class CameraControl : MonoBehaviour
{
    
    [Header("Camera Zoom")]
    [SerializeField] private float zoomOutMin;
    [SerializeField] private float zoomOutMax;
    [SerializeField] private float zoomSpeed;

    [Header("Camera Boundaries")]
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float topLimit;
    [SerializeField] private float bottomLimit;

    private float cameraDistanceLimit = 0; // This will never change, this will stay at 0.
    private float cameraBottomLimit = -10; // This is how far the camera is from the ground, and won't change.
    private Vector3 touchStart;

    // Update is called once per frame
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
        }
        else if (Input.GetMouseButton(0)) {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, bottomLimit, topLimit), Mathf.Clamp(transform.position.z, cameraBottomLimit, cameraDistanceLimit));
    }

    void Zoom(float increment) { Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax); } 

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }
}