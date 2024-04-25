using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject quizUI; // Reference to the canvas containing UI elements
    public GameObject[] uiElements; // Array to hold references to UI elements

    // Function to toggle the visibility of UI elements
    public void ToggleUIElements()
    {
        // Toggle the active state of each UI element
        foreach (GameObject element in uiElements)
        {
            element.SetActive(!element.activeSelf);
        }
    }
}