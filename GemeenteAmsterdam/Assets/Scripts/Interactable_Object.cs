using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Interactable_Object : MonoBehaviour
{
    UnityEvent interactEvent;

    void Start()
    {
        if (interactEvent == null)
            interactEvent = new UnityEvent();

        interactEvent.AddListener(Ping);
    }
    // Update is called once per frame
    void Update() { Interact(); }
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
                if (hit.transform != this.transform) return;
                Debug.Log("Object clicked with mouse");
                interactEvent.Invoke();
            }
        }
    }
    public void Ping()
    {
        Debug.Log("Ping");
    }
}