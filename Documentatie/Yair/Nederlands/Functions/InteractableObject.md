# InteractableObject

Het interactieve object is een hulpmiddel dat wordt gebruikt in de scène waar het op specifieke locaties kan worden geplaatst. Het is gemaakt om meerdere keren te worden gebruikt, omdat het dezelfde functie vervult als een knop die in het spel aanwezig is.

## Lijst met functies
  
- Activation
- Event

## Variabelenoverzicht

Dit is om alle variabelen te zien, en alleen de variabelen.

#### Activeringsvariabelen

Deze variabelen zijn bedoeld om het object zichtbaar en interactief te maken.

- Child
- IsItActive

- IsActive (Getter & Setter)

Child - Dit is een GameObject dat via de inspecteur kan worden ingesteld.

IsItActive - Dit is een Booleaanse waarde om te controleren of het object actief is.

IsActive - Dit is een Getter en Setter om extern toegang te krijgen tot de Boolean.

```csharp
    [SerializeField] private GameObject Child;
    private bool IsItActive;
    public bool IsActive { get => IsItActive;  set => IsItActive = value; } 
```

#### Gebeurtenisvariabele

Dit is één variabele, de gebeurtenis die het script flexibel maakt.

- InteractEvent

InteractEvent - Een UnityEvent-variabele.

```csharp
    [SerializeField] private UnityEvent InteractEvent;
```

## Activering

De activeringsfunctie is om ervoor te zorgen dat het object niet kan worden gebruikt, tenzij een specifieke Boolean actief is. Wanneer die Boolean actief is, wordt een sprite zichtbaar, waarbij die sprite het kind is van het hoofdobject.

```csharp
   Child.SetActive(IsItActive);
    if (IsItActive)
    {
        //Function
    }
```
> _Dit is gedaan in de functie Update._

## Event  
  
Hier is niets ingewikkelds, dit is slechts een functie om te controleren of de gebruiker überhaupt iets heeft aangeraakt, en roept vervolgens de eenheidsfunctie op.

```csharp
    foreach (Touch touch in Input.touches)
    {
        if (touch.phase == TouchPhase.Began)
        {
            InteractEvent.Invoke();
        }
    }
```
> _Dit is gedaan in de Update-functie, evenals in de activeringscontrole._

## Scriptoverzicht

Dit is waar ik het hele script zal plaatsen, om te zien hoe alles vloeit en samenwerkt.

```csharp
using UnityEngine.Events;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject Child;
    [SerializeField] private UnityEvent InteractEvent;
    private bool IsItActive;
    public bool IsActive { get => IsItActive;  set => IsItActive = value; } 

    void Start()
    {
        if (InteractEvent == null) { InteractEvent = new UnityEvent(); }
    }
    // Update is called once per frame
    void Update() 
    {         
        Child.SetActive(IsItActive);
        if (IsItActive)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    InteractEvent.Invoke();
                }
            }
        }
    }
}
```