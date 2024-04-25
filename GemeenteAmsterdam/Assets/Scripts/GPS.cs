using UnityEngine;
using System.Collections;
using TMPro;

public class GPS : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;
    private float Offset;
    private void Start()    
    {
        StartCoroutine(Compass());
    }
    public IEnumerator Compass()
    {
        //check the gps availability
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        if (!UnityEngine.Input.location.isEnabledByUser)
        {
            Text.text = "No GPS enabled";
            StartCoroutine(Compass());
            yield break;
        }

        UnityEngine.Input.compass.enabled = true;

        UnityEngine.Input.location.Start(1, 0.01f);

        // Wait until service initializes
        int _MaxSecondsToWaitForLocation = 10;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && _MaxSecondsToWaitForLocation > 0)
        {
            yield return new WaitForSeconds(1);
            _MaxSecondsToWaitForLocation--;
        }

        if (_MaxSecondsToWaitForLocation < 1)
        {
            Text.text = "location service timeout";
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status == LocationServiceStatus.Failed)
        {
            Text.text = "unable to determine device location";
            yield break;
        }

        Text.text = "location service loaded";
        yield break;
    }
    public void GPS_Check()
    {
        Debug.LogFormat("Location service live. status {0}", UnityEngine.Input.location.status);
        Text.text = ("Location: "
            + UnityEngine.Input.location.lastData.latitude + " "
            + UnityEngine.Input.location.lastData.longitude + " "
            + UnityEngine.Input.location.lastData.horizontalAccuracy);
        Offset = UnityEngine.Input.compass.magneticHeading;
        transform.rotation = Quaternion.Euler(0, 0,Offset);
    }
}
