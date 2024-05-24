# Technisch Ontwerp GPS

Dit is het technische ontwerpdossier voor het GPS-systeem, waarin ik van plan was te onderzoeken hoe je je huidige locatie en de doellocatie kunt vinden.

## Gewenste Functies

Een systeem dat in staat is te detecteren waar je je bevindt in de wereld door gebruik te maken van de GPS-locatie en Geolocatie, en deze vervolgens toe te wijzen aan gameobjecten in de gamescène zodat het overeenkomt met de locaties op de echte kaart.

## Zoeken

Ik begon te zoeken naar hoe ik een systeem voor Unity kon maken dat je locatie bijhoudt. Ik werd doorverwezen naar een paar video's die MapBox gebruikten, dus ik keek wat het inhield. Het bleek een pakket te zijn dat op Unity geïnstalleerd kan worden en gebruikmaakt van de Google Maps API, wat het gemakkelijker maakt om het te gebruiken in Unity voor elke ontwikkelaar die van plan is een spel met een GPS-systeem te maken.

Bij het zoeken naar hoe je het kunt gebruiken en het mogelijk maakt om de afstand tussen de speler en de locatie in real-time te vinden, zodat het scripts kan activeren wanneer de locatie wordt bereikt, ontdekte ik dat MapBox dit niet ingebouwd had. Dus moest ik een uitbreidingsscript downloaden dat de geolocatie van de coördinaat van de speler en de doellocatie decodeerde.

Terwijl ik zocht, kon ik verschillende manieren vinden om het GPS-systeem te verkrijgen, maar door gebrek aan tijd om hieraan te werken, had ik niet veel tijd om veel opties te zoeken. Dus vond ik de snelste optie die ik kon vinden om te maken wat we wilden maken.

## Experimenteren

Met behulp van de voorbeeldscènes van MapBox zou ik proberen een kopie te maken van de locatie waarvoor we het spel maken. MapBox was bedoeld om een kaartachtergrond te creëren op basis van de locatie die was ingesteld met behulp van Google Maps-coördinaten. Voor mij werkte het niet precies zoals het zou moeten, dus ontdekte ik hoe ik die functie kon uitschakelen.

Ik zag dat sommige scripts van de scène geen deel uitmaakten van het pakket, maar van het voorbeeld, en nodig waren voor het hele systeem om te werken. Dus begon ik te kijken wat er nodig was en kopieerde het, zag wat er werd gebruikt en maakte mijn eigen versie om het later zelf te kunnen draaien.

