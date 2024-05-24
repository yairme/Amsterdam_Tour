# Camera System

The camera System is to give the player control of the camera, making it possible for the player to look at everything that the game has to show.

## List of Features

- Movement
- Zoom
- Bounds
- Focus

## Variables Overview

This is to see all the variables, and only the variables.

#### Camera Zoom Variables

These variables are to keep the camera from zooming out or in to much, they also control the speed of the zoom.

- zoomOutMin
- zoomOutMax
- zoomSpeed

zoomOutMin - The bound that stops the camera from zooming in too much.

zoomOutMaz - The bound that stops the camera from zooming out too much.

zoomSpeed - Is the multiplier that changes how fast the zoom will happen, either when zooming out or in.

```csharp
    [Header("Camera Zoom")]
    [SerializeField] private float zoomOutMin;
    [SerializeField] private float zoomOutMax;
    [SerializeField] private float zoomSpeed;
```
> _Header is to make it clear in the unity inspector._

#### Map Variables

These variables are to get the transform of these objects, to get there position in the scene to give it to there 

- mapGameObject
- startPoint

mapGameobject - The variable to get the game object in the unity inspector, for the map object.

startPoint - The variable to get the game object in the unity inspector, for the start point object.

```csharp
    [Header("Map")]
    [SerializeField] private GameObject mapGameObject;
    [SerializeField] private Transform startPoint;
```

#### Bounds Variable
This is the variables to makes the bounds according to how far from the center of the map it is.

- leftLimit
- rightLimit
- topLimit
- bottomLimit

leftLimit - The distance of the left most bound from the center.

rightLimit - The distance of the right most bound from the center.

topLimit - The distance of the top most bound from the center.

bottomLimit - The distance of the bottom most bound from the center.

```csharp
    private float leftLimit;
    private float rightLimit;
    private float topLimit;
    private float bottomLimit;
```
> _These variables are applied in a function._

#### Camera Distance Variable
These variables are just here to make sure the bound function doesn't move the camera in the Z axes.

cameraDistanceLimit - This is the limit is where the map should be.

cameraBottomLimit - This is the distance away from the map.

```csharp
    private float cameraDistanceLimit = 0; // This will never change, this will stay at 0.
    private float cameraBottomLimit = -10; // This is how far the camera is from the ground, and won't change.
```
> _These won't change as it's to stop a function from ruining how the camera works._

#### Touch location variable

This variable saves the spot where the player sets there finger, and then according where the finger goes, it will give the direction where the camera needs to move.

touchStart - This variable saves the spot where the player sets there finger.

```csharp
    private Vector3 touchStart;
````

## Movement

The movement is simple, it let's the player move the camera with their finger, going across the X & Y axis.

#### Movement control
```csharp
    if (Input.touchCount == 1) 
    { 
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); 
        }
    }
    else if (Input.touchCount == 1) 
    {

        if (Input.GetTouch(0).phase == TouchPhase.Moved) {

            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.position += direction;
        }
    }
```
> _This feature is in the update._

## Zoom

With this feature it let's the player zoom in and out by using two fingers, when you pull your fingers away from each other the camera zooms out, when you pull them together the camera zooms in.

#### Zoom Control
```csharp
    else if (Input.touchCount == 2) {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

        float difference = currentMagnitude - prevMagnitude;

        Zoom(difference * zoomSpeed);
    }
```
> _This part of the feature is in the update function._

#### Zoom function
```csharp
     void Zoom(float increment) { Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax); } 
```

## Bounds

The bound feature is an invincible feature that is to keep the player from moving the camera outside of where it needs to be, the bound feature is constantly being updated, seeing if the center of the camera reached it, if it did it will stop the camera from moving forward.

#### Bound Function
```csharp
    private void CameraBounds() { 
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
        Mathf.Clamp(transform.position.y, bottomLimit, topLimit), 
        Mathf.Clamp(transform.position.z, cameraBottomLimit, cameraDistanceLimit)); 
    }
```

> _This function is constantly being updated in the update function._

#### Bound Creating/Updating Function
```csharp
    private void SpriteBounds(GameObject _gameObject) { 

        var _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();

        float _vertExtent = Camera.main.orthographicSize;
        float _horiExtent = _vertExtent * Screen.width / Screen.height;

        // Calculate the new limits based on the sprite's bounds and the camera's extents
        leftLimit = _gameObject.transform.position.x - (_spriteRenderer.bounds.size.x / 2.0f) + _horiExtent;
        rightLimit = _gameObject.transform.position.x + (_spriteRenderer.bounds.size.x / 2.0f) - _horiExtent;
        bottomLimit = _gameObject.transform.position.y - (_spriteRenderer.bounds.size.y / 2.0f) + _vertExtent;
        topLimit = _gameObject.transform.position.y + (_spriteRenderer.bounds.size.y / 2.0f) - _vertExtent;
    }
```
> _This is created on awake, but it also updated each time the zoom control is being done, to make sure the camera stays inside of bounds when zooming in or out._

## Focus

I created the function to center the camera to a specific point, that point would be the start of the camera, that point can be changed to center the camera on something.

#### Focus Function
```csharp
    private void SetCameraCenter(Vector3 center) { Camera.main.transform.position = new Vector3(center.x, center.y, Camera.main.transform.position.z); }
```

> _This is called once in the awake of the script to start out, it can be called later if needed._

## Script Overview

This is where I will put the whole script, to see how everything flows and work together.

```csharp
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

            Zoom(difference * zoomSpeed);
            SpriteBounds(mapGameObject); 
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
    void Zoom(float increment) { Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax); } 
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
        leftLimit = _gameObject.transform.position.x - (_spriteRenderer.bounds.size.x / 2.0f) + _horiExtent;
        rightLimit = _gameObject.transform.position.x + (_spriteRenderer.bounds.size.x / 2.0f) - _horiExtent;
        bottomLimit = _gameObject.transform.position.y - (_spriteRenderer.bounds.size.y / 2.0f) + _vertExtent;
        topLimit = _gameObject.transform.position.y + (_spriteRenderer.bounds.size.y / 2.0f) - _vertExtent;

    }
    /// <summary>
    /// This function is used to keep the camera within the defined bounds.
    /// </summary>
    private void CameraBounds() 
    { 
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
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }
}
```
