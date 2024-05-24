# InteractableObject

The InteractableObject is an asset that is used in the scene where it can be places in specific locations, it's made to be used multiple times, as it does the same job of a button that is in game.

## List of Features  
  
- Activation
- Event

## Variables Overview

This is to see all the variables, and only the variables.

#### Activation Variables

These variables are to make the object visible and interactable.

- Child
- IsItActive

Child - This is a GameObject that can be set through the inspector.

IsItActive - This is a Boolean to check if the object is active.

```csharp
    [SerializeField] private GameObject Child;
    private bool IsItActive;
```

#### GeoLocation Variables  
  
These are the variables that are used check if the player is near the location that is assigned by the coordinates and by using geoLocation it is possible to visualize it with gameObjects in Unity

- PlayerLocation
- eventPos

PlayerLocation - It's using a refactored script that was given with the MapBox Package. 

eventPos - It's the geolocation of the event object according to the coordinates that were assigned. 

```csharp
    private PlayerLocation PlayerLocation;
    [HideInInspector] public Vector2d eventPos;
```

#### Event Variable 

This is one variable, the event that makes the script flexible.

- InteractEvent

InteractEvent - An UnityEvent variable.

```csharp
    [SerializeField] private UnityEvent InteractEvent;
```

## Activation

The activation function is to make the object not be able to be used unless a specific Boolean is active, when that Boolean is active it will make a sprite visible, that sprite being the child of the main object.

```csharp
   Child.SetActive(IsItActive);
    if (IsItActive)
    {
        //Function
    }
```
> _This has been done in the Update function._

## GeoLocation Detection

This is using the extension script GeoCoordinate, that gets the GeoLocation of the interactable object and the player, so it's possible to detect their distance in real time so it can change the Boolean that changes if the object is active or not. 

```csharp
    PlayerLocation = GameObject.Find("Pre_MapManager").GetComponent<PlayerLocation>();
    var _currentPlayerLocation = new GeoCoordinatePortable.GeoCoordinate(PlayerLocation.GetLatitude(), PlayerLocation.GetLongitude());
    var eventLocation = new GeoCoordinatePortable.GeoCoordinate(eventPos.x, eventPos.y);
    var distance = _currentPlayerLocation.GetDistanceTo(eventLocation);

    if (distance < Range) { IsItActive = true; }
    else { IsItActive = false; }
```

## Event  
  
Nothing complicated here, this is just a function to check if the user has touched at all, then invokes the unity function.

```csharp
    foreach (Touch touch in Input.touches)
    {
        if (touch.phase == TouchPhase.Began)
        {
            if (IsItActive)
            {
                InteractEvent.Invoke();
            }
        }
    }
```
> _This has been done in the Update function, as well in the activation check._

## Script Overview

This is where I will put the whole script, to see how everything flows and work together.

```csharp
using UnityEngine.Events;
using UnityEngine;
using Mapbox.Utils;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private double Range;
    [SerializeField] private GameObject Child;
    public UnityEvent InteractEvent;
    private bool IsItActive = true;
    private PlayerLocation PlayerLocation;
    [HideInInspector] public Vector2d eventPos;
    void Start()
    {
        if (InteractEvent == null) { InteractEvent = new UnityEvent(); }
    }
    void Update() 
    {         
        PlayerLocation = GameObject.Find("Pre_MapManager").GetComponent<PlayerLocation>();
        var _currentPlayerLocation = new GeoCoordinatePortable.GeoCoordinate(PlayerLocation.GetLatitude(), PlayerLocation.GetLongitude());
        var eventLocation = new GeoCoordinatePortable.GeoCoordinate(eventPos.x, eventPos.y);
        var distance = _currentPlayerLocation.GetDistanceTo(eventLocation);

        if (distance < Range) { IsItActive = true; }
        else { IsItActive = false; }
        
        Child.SetActive(IsItActive);

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (IsItActive)
                {
                    InteractEvent.Invoke();
                }
            }
        }
    }
}
```