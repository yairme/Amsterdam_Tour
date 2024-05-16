using UnityEngine;
using Mapbox.Unity.Location;

public class PlayerLocation : MonoBehaviour
{
    private AbstractLocationProvider _locationProvider = null;
    private Location currLoc;
	private void Start()
	{
        if (null == _locationProvider)
		{
			_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
		}
	}

    private void Update()
    {
        currLoc = _locationProvider.CurrentLocation;
    }

    public double GetLatitude()
    {
        return currLoc.LatitudeLongitude.x;
    }

    public double GetLongitude()
    {
        return currLoc.LatitudeLongitude.y;
    }
}