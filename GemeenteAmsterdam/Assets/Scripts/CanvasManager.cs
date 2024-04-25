using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject[] canvases; // Array to hold references to different UI canvases

    // Function to activate a canvas and deactivate others
    public void ActivateCanvas(int index)
    {
        // Loop through all canvases
        for (int i = 0; i < canvases.Length; i++)
        {
            // Activate the canvas at the specified index and deactivate others
            canvases[i].SetActive(i == index);
        }
    }
}
