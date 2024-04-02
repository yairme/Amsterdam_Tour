using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Object : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
     
        Interact();

    }

    public void Interact()
    {
        // Detect if we are interacting with a mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // If we hit this object
                if (hit.transform == this.transform)
                {
                    // Do something
                    Debug.Log("Object clicked with mouse");
                }
            }
            
        }
    }

}