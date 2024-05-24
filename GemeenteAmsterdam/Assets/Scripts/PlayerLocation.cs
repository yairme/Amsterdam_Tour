using UnityEngine;
using Mapbox.Unity.Location;

public class PlayerLocation : MonoBehaviour
{
    private AbstractLocationProvider LocationProvider = null;
    private Location CurrentLocation;
	private void Start()
	{
        if (null == LocationProvider)
		{
			LocationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
		}
	}
    private void Update()
    {
        CurrentLocation = LocationProvider.CurrentLocation;
    }
    public double GetLatitude()
    {
        return CurrentLocation.LatitudeLongitude.x;
    }
    public double GetLongitude()
    {
        return CurrentLocation.LatitudeLongitude.y;
    }
}