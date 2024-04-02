using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Interactable_Object : MonoBehaviour
{
    [SerializeField] UnityEvent interactEvent;

    void Start()
    {
        if (interactEvent == null)
        {
            interactEvent = new UnityEvent();
            interactEvent.AddListener(Ping);
        }
    }
    // Update is called once per frame
    void Update() 
    {         
        // Detect if we are interacting with a touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Create a ray from the touch position
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 2f);

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
                    interactEvent.Invoke();
                }
            }
        } 
    }
    public void Interact()
    {

    }

    public void Ping()
    {
        Debug.Log("Ping");
    }
}