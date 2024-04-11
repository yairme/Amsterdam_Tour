using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Waypoint : MonoBehaviour
{
    public void ColorChange()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
}
