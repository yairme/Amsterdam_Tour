# GPS

This is the chart to visualize how the GPS system for MapBox work with out game.

## Interactable Object

This is how MapBox interacts with my script InteractableObject, making it find the distance between itself and the player location.

```mermaid
classDiagram
   InteractableObject <|-- PlayerLocation
   InteractableObject <|-- SpawnWaypoints
   InteractableObject <|-- GeoCoordinate
   
   PlayerLocation <|-- MapBox
   SpawnWaypoints <|-- MapBox

   class InteractableObject{
       +Range;
       +eventPos;
       Update()
   }
   class SpawnWaypoints{
       +Locations;
       +InteractEvent;
   }
   class GeoCoordinate{
       GeoCoordinatePortable.GeoCoordinate()
   }
   class MapBox{
       AbstractLocationProvider()
       Location()
   }
   class PlayerLocation{
       +CurrentLocation;
       GetLatitude()
       GetLongitude()
   }
```

## PlayerLocation on GameObject

This is how MapBox gives the player location on a GameObject so it can be visualized through a game object.

```mermaid
classDiagram
    DevicePosition <|-- MapBox
    DeviceRotation <|-- MapBox

    class DevicePosition{
        ILocationProvider TheLocationProvider;
        ILocationProvider LocationProvider;
    }
    class DeviceRotation{
        ILocationProvider DeviceLocationProvider;
        ILocationProvider LocationProvider;
        LocationProvider_OnLocationUpdated(Location _location)
        getNewEulerAngles(float _newAngle)
    }
    class MapBox{
        Location()
    }
```