using Mapbox.Unity.Location;
using UnityEngine;
public class DevicePosition : MonoBehaviour
{
	private bool IsInitialized;
	private ILocationProvider TheLocationProvider;
	public ILocationProvider LocationProvider
	{
		get
		{
			if (TheLocationProvider == null)
			{
				TheLocationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
			}
			return TheLocationProvider;
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