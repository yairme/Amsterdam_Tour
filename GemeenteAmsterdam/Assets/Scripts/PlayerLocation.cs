using UnityEngine;
using Mapbox.Unity.Location;

public class PlayerLocation : MonoBehaviour
{
    private AbstractLocationProvider _locationProvider = null;
    private Location CurrLoc;
	private void Start()
	{
        if (null == _locationProvider)
		{
			_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
		}
	}

    private void Update()
    {
        CurrLoc = _locationProvider.CurrentLocation;
    }

    public double GetLatitude()
    {
        return CurrLoc.LatitudeLongitude.x;
    }

    public double GetLongitude()
    {
        return CurrLoc.LatitudeLongitude.y;
    }
}