using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector3 touchStart;

    [Header("Camera Zoom")]
    [SerializeField] private float ZoomOutMin;
    [SerializeField] private float ZoomOutMax;
    [SerializeField] private float ZoomSpeed;

    [Header("Camera Boundaries")]
    [SerializeField] private float LeftLimit;
    [SerializeField] private float RightLimit;
    [SerializeField] private float TopLimit;
    [SerializeField] private float BottomLimit;
    private float CameraDistaceLimit = 0; // This will never change, this will stay at 0.
    private float CameraBottomLimit = -10; // This is how far the camera is from the ground, and won't change.
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

            Zoom(difference * ZoomSpeed);
        }
        else if (Input.GetMouseButton(0)) {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LeftLimit, RightLimit), Mathf.Clamp(transform.position.y, BottomLimit, TopLimit), Mathf.Clamp(transform.position.z, CameraBottomLimit, CameraDistaceLimit));
    }

    void Zoom(float increment) { Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, ZoomOutMin, ZoomOutMax); } 

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(LeftLimit, TopLimit), new Vector2(RightLimit, TopLimit));
        Gizmos.DrawLine(new Vector2(RightLimit, TopLimit), new Vector2(RightLimit, BottomLimit));
        Gizmos.DrawLine(new Vector2(RightLimit, BottomLimit), new Vector2(LeftLimit, BottomLimit));
        Gizmos.DrawLine(new Vector2(LeftLimit, BottomLimit), new Vector2(LeftLimit, TopLimit));
    }
}