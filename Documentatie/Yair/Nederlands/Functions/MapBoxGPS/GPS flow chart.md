# GPS

Dit is het diagram om te visualiseren hoe het GPS-systeem voor MapBox werkt met ons spel.

## Interactief Object

Dit is hoe MapBox interacteert met mijn script InteractiefObject, waardoor het de afstand tussen zichzelf en de locatie van de speler vindt.

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

## SpelerLocatie op GameObject

Dit is hoe MapBox de locatie van de speler geeft op een GameObject zodat het gevisualiseerd kan worden via een gameobject.

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