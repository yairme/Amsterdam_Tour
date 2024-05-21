using Mapbox.Unity.Location;
using UnityEngine;
public class RotateWithLocationProvider : MonoBehaviour
{
	/// <summary>
	/// The rate at which the transform's rotation tries catch up to the provided heading.  
	/// </summary>
	[SerializeField] private float RotationFollowFactor = 1;
	/// <summary>
	/// Location property used for rotation: false=Heading (default), true=Orientation  
	/// </summary>
	[SerializeField] private bool UseDeviceOrientation;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] private bool SubtractUserHeading;
	/// <summary>
	/// Set this to true if you'd like to adjust the rotation of a RectTransform (in a UI canvas) with the heading.
	/// </summary>
	[SerializeField] private bool RotateZ;
	/// <summary>
	/// <para>Set this to true if you'd like to adjust the sign of the rotation angle.</para>
	/// <para>eg angle passed in 63.5, angle that should be used for rotation: -63.5.</para>
	/// <para>This might be needed when rotating the map and not objects on the map.</para>
	/// </summary>
	[SerializeField] private bool UseNegativeAngle;
	/// <summary>
	/// Use a mock <see cref="T:Mapbox.Unity.Location.TransformLocationProvider"/>,
	/// rather than a <see cref="T:Mapbox.Unity.Location.EditorLocationProvider"/>.   
	/// </summary>
	[SerializeField] private bool UseTransformLocationProvider;
	private Quaternion TargetRotation;
	/// <summary>
	/// The location provider.
	/// This is public so you change which concrete <see cref="ILocationProvider"/> to use at runtime.  
	/// </summary>
	private ILocationProvider locationProvider;
	public ILocationProvider LocationProvider
	{
		private get
		{
			if (locationProvider == null)
			{
				locationProvider = UseTransformLocationProvider ?
					LocationProviderFactory.Instance.TransformLocationProvider : LocationProviderFactory.Instance.DefaultLocationProvider;
			}
			return locationProvider;
		}
		set
		{
			if (locationProvider != null)
			{
				locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
			}
			locationProvider = value;
			locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
		}
	}
	void Start()
	{
		LocationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
	}
	void OnDestroy()
	{
		if (LocationProvider != null)
		{
			LocationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
		}
	}
	void LocationProvider_OnLocationUpdated(Location location)
	{
		float rotationAngle = UseDeviceOrientation ? location.DeviceOrientation : location.UserHeading;
		if (UseNegativeAngle) { rotationAngle *= -1f; }
		// 'Orientation' changes all the time, pass through immediately
		if (UseDeviceOrientation)
		{
			if (SubtractUserHeading)
			{
				if (rotationAngle > location.UserHeading)
				{
					rotationAngle = 360 - (rotationAngle - location.UserHeading);
				}
				else
				{
					rotationAngle = location.UserHeading - rotationAngle + 360;
				}
				if (rotationAngle < 0) { rotationAngle += 360; }
				if (rotationAngle >= 360) { rotationAngle -= 360; }
			}
			TargetRotation = Quaternion.Euler(getNewEulerAngles(rotationAngle));
		}
		else
		{
			// if rotating by 'Heading' only do it if heading has a new value
			if (location.IsUserHeadingUpdated)
			{
				TargetRotation = Quaternion.Euler(getNewEulerAngles(rotationAngle));
			}
		}
	}
	private Vector3 getNewEulerAngles(float newAngle)
	{
		var localRotation = transform.localRotation;
		var currentEuler = localRotation.eulerAngles;
		var euler = Mapbox.Unity.Constants.Math.Vector3Zero;
		if (RotateZ)
		{
			euler.z = -newAngle;
			euler.x = currentEuler.x;
			euler.y = currentEuler.y;
		}
		else
		{
			euler.y = -newAngle;
			euler.x = currentEuler.x;
			euler.z = currentEuler.z;
		}
		return euler;
	}
	void Update()
	{
		transform.localRotation = Quaternion.Lerp(transform.localRotation, TargetRotation, Time.deltaTime * RotationFollowFactor);
	}
}