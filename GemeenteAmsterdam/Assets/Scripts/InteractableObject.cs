using UnityEngine.Events;
using UnityEngine;
using System;
using Mapbox.Examples;
using Mapbox.Utils;
using Mapbox.Unity.Location;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject Child;
    [SerializeField] private UnityEvent InteractEvent;
    [SerializeField] private bool IsItActive;
    private LocationStatus PlayerLocation;
    public Vector2d position;

    void Start()
    {
        if (InteractEvent == null) { InteractEvent = new UnityEvent(); }
    }
    // Update is called once per frame
    void Update() 
    {         
        Child.SetActive(IsItActive);
    }

    private void OnMouseDown()
    {
        PlayerLocation = GameObject.Find("MapManager").GetComponent<LocationStatus>();
        var _currentLocation = new GeoCoordinatePortable.GeoCoordinate(PlayerLocation.GetLatitude(), PlayerLocation.GetLongitude());
        if (IsItActive)
        {
            InteractEvent.Invoke();
        }
    }
}