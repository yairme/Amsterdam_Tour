using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class InteractableObject : MonoBehaviour
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
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Interacted with " + this.name);
                interactEvent.Invoke();
            }
        }
    }

    public void Ping()
    {
        Debug.Log("Ping");
    }
}