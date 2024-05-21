using UnityEngine;
using UnityEngine.Events;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
public class SpawnWayPoints : MonoBehaviour
{
	[SerializeField] private AbstractMap Map;
	[SerializeField] [Geocode] private string[] LocationStrings;
	[SerializeField] private float SpawnScale = 100f;
	[SerializeField] private GameObject MarkerPrefab;
    [SerializeField] private UnityEvent[] InteractEvent;

	private Vector2d[] Locations;
	private List<GameObject> SpawnedObjects;

	void Start()
	{
		Locations = new Vector2d[LocationStrings.Length];
		SpawnedObjects = new List<GameObject>();
		for (int i = 0; i < LocationStrings.Length; i++)
		{
			var locationString = LocationStrings[i];
			Locations[i] = Conversions.StringToLatLon(locationString);
			var instance = Instantiate(MarkerPrefab);
			instance.GetComponent<InteractableObject>().eventPos = Locations[i];
            instance.GetComponent<InteractableObject>().InteractEvent = InteractEvent[i];
			instance.transform.localPosition = Map.GeoToWorldPosition(Locations[i], true);
			instance.transform.localScale = new Vector3(SpawnScale, SpawnScale, SpawnScale);
			SpawnedObjects.Add(instance);
		}
	}
	private void Update()
	{
		int count = SpawnedObjects.Count;
		for (int i = 0; i < count; i++)
		{
			var spawnedObject = SpawnedObjects[i];
			var location = Locations[i];
			spawnedObject.transform.localPosition = Map.GeoToWorldPosition(location, true);
			spawnedObject.transform.localScale = new Vector3(SpawnScale, SpawnScale, SpawnScale);
		}
	}
}
