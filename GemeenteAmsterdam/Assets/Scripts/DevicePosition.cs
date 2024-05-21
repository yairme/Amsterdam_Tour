using Mapbox.Unity.Location;
using UnityEngine;
public class DevicePosition : MonoBehaviour
{
	private bool IsInitialized;
	private ILocationProvider locationProvider;
	public ILocationProvider LocationProvider
	{
		get
		{
			if (locationProvider == null)
			{
				locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
			}
			return locationProvider;
		}
	}
	void Start()
	{
		LocationProviderFactory.Instance.mapManager.OnInitialized += () => IsInitialized = true;
	}
	void LateUpdate()
	{
		if (IsInitialized)
		{
			var map = LocationProviderFactory.Instance.mapManager;
			transform.localPosition = map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude);
		}
	}
}