using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private int Radius;
    [SerializeField] UnityEvent interactEvent;
    private bool isActive;
    public bool IsActive { get { return isActive; } set { isActive = value; } }

    void Start()
    {
        if (interactEvent == null) { interactEvent = new UnityEvent(); }
    }
    // Update is called once per frame
    void Update() 
    {         
        if (!isActive) return;


            if (Input.GetMouseButton(0))
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    interactEvent.Invoke();
                }
            }


    }
    public void Ping() { Debug.Log("Ping"); } //Test listener
    public void Pong() { Debug.Log("Pong"); } // Test listener
}