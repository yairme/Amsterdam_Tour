using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private int Radius;
    [SerializeField] private bool isActive;
    [SerializeField] UnityEvent interactEvent;

    void Start()
    {
        if (interactEvent == null) // To prevent null reference exception
        {
            interactEvent = new UnityEvent(); // Create a new UnityEvent
        }

    }
    // Update is called once per frame
    void Update() 
    {         
        // if ( Radius < 0) // ToDo: Implement a better way to check if the player is in range.
        // { 
        //    if (!isActive) { this.gameObject.SetActive(true); }
        // } 
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began) { interactEvent.Invoke(); }
        }
    }
    public void Ping() { Debug.Log("Ping"); } //Test listener
    public void Pong() { Debug.Log("Pong"); } // Test listener

}