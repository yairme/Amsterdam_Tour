using UnityEngine;
using Mapbox.Unity.Location;

public class PlayerLocation : MonoBehaviour
{
    private AbstractLocationProvider LocationProvider = null;
    private Location CurrLoc;
	private void Start()
	{
        if (null == LocationProvider)
		{
			LocationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
		}
	}

    private void Update()
    {
        CurrLoc = LocationProvider.CurrentLocation;
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