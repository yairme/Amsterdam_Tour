using UnityEngine;
using UnityEngine.UI;

public class UITransition : MonoBehaviour
{
    [SerializeField] private GameObject[] Layouts;
    public void SetLayoutActive(string _layoutName) // Function to set the canvas active or inactive.
    {
        foreach (GameObject Layout in Layouts) // Loop through all the canvases in the Canvases array.
        {
            if (Layout.name.ToLower() == _layoutName.ToLower()) // If the canvas name matches the name passed in the parameter.
            {
                Layout.SetActive(true); // Set the canvas active.
            }
            if (Layout.name.ToLower() != _layoutName.ToLower()) // If the canvas is active.
            {
                Layout.SetActive(false); // Set the canvas inactive.
            }
        }
    }

    public void ToggleUIElement(GameObject _UiElement) // Function to toggle the UI element active or inactive.
    {
        bool isActive = _UiElement.activeInHierarchy; // Get the current state of the UI element.

        _UiElement.SetActive(!isActive); // Set the UI element to the opposite state.
    }
}