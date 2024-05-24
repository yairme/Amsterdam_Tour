# Camera System

Het camerasysteem is bedoeld om de speler controle te geven over de camera, waardoor de speler kan kijken naar alles wat de game te bieden heeft.

## Lijst met functies

- Beweging
- Zoom
- Grenzen
- Focus

## Variabelenoverzicht

Dit is om alle variabelen te zien, en alleen de variabelen.

#### Camerazoomvariabelen

Deze variabelen zorgen ervoor dat de camera niet te veel in- of uitzoomt, maar bepalen ook de snelheid van de zoom.

- zoomOutMin
- zoomOutMax
- zoomSpeed

zoomOutMin - De grens die ervoor zorgt dat de camera niet te veel inzoomt.

zoomOutMaz - De grens die ervoor zorgt dat de camera niet te veel uitzoomt.

zoomSpeed - Is de vermenigvuldiger die verandert hoe snel de zoom zal plaatsvinden, zowel bij het uitzoomen als bij het inzoomen.

```csharp
    [Header("Camera Zoom")]
    [SerializeField] private float zoomOutMin;
    [SerializeField] private float zoomOutMax;
    [SerializeField] private float zoomSpeed;
```
> _De kop is bedoeld om het duidelijk te maken in de eenheidsinspecteur._

#### Variabelen in kaart brengen

Deze variabelen zijn bedoeld om de transformatie van deze objecten te verkrijgen, om hun positie in de scène te krijgen en deze daar te geven

- mapGameObject
- startPoint

mapGameobject - De variabele om het spelobject op te halen in de eenheidsinspecteur, voor het kaartobject.

startPoint - De variabele om het spelobject op te halen in de eenheidsinspecteur, voor het startpuntobject.

```csharp
    [Header("Map")]
    [SerializeField] private GameObject mapGameObject;
    [SerializeField] private Transform startPoint;
```

#### Grenzen Variabel

Dit zijn de variabelen die de grenzen bepalen op basis van hoe ver deze zich van het midden van de kaart bevindt.

- leftLimit
- rightLimit
- topLimit
- bottomLimit

leftLimit - De afstand van de meest linkse grens vanaf het midden.

rightLimit - De afstand van de meest rechtse grens vanaf het midden.

topLimit - De afstand van de bovenkant die het verst verwijderd is van het midden.

bottomLimit - De afstand van de bodem die het verst van het midden verwijderd is.

```csharp
    private float leftLimit;
    private float rightLimit;
    private float topLimit;
    private float bottomLimit;
```
> _Deze variabelen worden toegepast in een functie._

#### Camera-afstand variabel

Deze variabelen zijn er alleen om ervoor te zorgen dat de gebonden functie de camera niet in de Z-assen beweegt.

cameraDistanceLimit - Dit is de limiet waar de kaart zou moeten zijn.

cameraBottomLimit - Dit is de afstand vanaf de kaart.

```csharp
    private float cameraDistanceLimit = 0; // This will never change, this will stay at 0.
    private float cameraBottomLimit = -10; // This is how far the camera is from the ground, and won't change.
```
> _Deze zullen niet veranderen, omdat het bedoeld is om te voorkomen dat een functie de werking van de camera verpest._

#### Aanraaklocatievariabele

Deze variabele slaat de plek op waar de speler zijn vinger plaatst, en geeft vervolgens, afhankelijk van waar de vinger naartoe gaat, de richting aan waarin de camera moet bewegen.

touchStart - Deze variabele slaat de plek op waar de speler zijn vinger plaatst.

```csharp
    private Vector3 touchStart;
````

## Beweging

De beweging is eenvoudig: de speler kan de camera met zijn vinger bewegen, langs de X- en Y-as.

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
> _Deze functie zit in de update._

## Zoom

Met deze functie kan de speler in- en uitzoomen door twee vingers te gebruiken. Wanneer u uw vingers van elkaar wegtrekt, zoomt de camera uit, wanneer u ze naar elkaar toe trekt, zoomt de camera in.

#### Zoomregeling
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
> _Dit deel van de functie bevindt zich in de updatefunctie._

#### Zoomfunctie
```csharp
     void Zoom(float increment) { Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax); } 
```

## Grenzen

De grensfunctie is een onoverwinnelijke functie die ervoor zorgt dat de speler de camera niet beweegt buiten de plaats waar deze moet zijn. De grensfunctie wordt voortdurend bijgewerkt, om te kijken of het midden van de camera het heeft bereikt. Als dit het geval is, stopt het de camera niet vooruit beweegt.

#### Gebonden functie
```csharp
    private void CameraBounds() { 
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
        Mathf.Clamp(transform.position.y, bottomLimit, topLimit), 
        Mathf.Clamp(transform.position.z, cameraBottomLimit, cameraDistanceLimit)); 
    }
```

> _Deze functie wordt voortdurend bijgewerkt in de updatefunctie._

#### Gebonden functie voor creëren/bijwerken
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
> _Dit wordt gemaakt wanneer de camera wakker wordt, maar wordt ook elke keer bijgewerkt wanneer de zoombediening wordt uitgevoerd, om ervoor te zorgen dat de camera binnen de grenzen blijft bij het in- of uitzoomen._

## Focus

Ik heb de functie gemaakt om de camera op een specifiek punt te centreren, dat punt zou het begin van de camera zijn, dat punt kan worden gewijzigd om de camera ergens op te centreren.

#### Focusfunctie
```csharp
    private void SetCameraCenter(Vector3 center) { Camera.main.transform.position = new Vector3(center.x, center.y, Camera.main.transform.position.z); }
```

> _Dit wordt één keer aangeroepen in het kielzog van het script om te beginnen, het kan indien nodig later worden aangeroepen._

## Scriptoverzicht

Dit is waar ik het hele script zal plaatsen, om te zien hoe alles vloeit en samenwerkt.

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
