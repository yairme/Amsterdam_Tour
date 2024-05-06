using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public void ActivateCanvas()
    {
        this.SetActive(true); // Activate the current canvas
    }
    
    public void DeactivateCanvas()
    {
        this.SetActive(false); // Deactivate the current canvas
    }
}
