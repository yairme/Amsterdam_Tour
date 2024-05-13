using UnityEngine;
using UnityEngine.UI;

public class UITransition : MonoBehaviour
{
    // Function to turn on or off a specified canvas element
    public void ToggleCanvasElement(GameObject elementToToggle)
    {
        if (elementToToggle != null)
        {
            elementToToggle.SetActive(!elementToToggle.activeSelf);
        }
    }
}