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