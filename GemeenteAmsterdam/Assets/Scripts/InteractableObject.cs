using UnityEngine.Events;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private int Radius;
    [SerializeField] private GameObject Child;
    [SerializeField] UnityEvent interactEvent;
    [SerializeField] private bool isActive;
    public bool IsActive { get { return isActive; } set { isActive = value; } }

    void Start()
    {
        if (interactEvent == null) { interactEvent = new UnityEvent(); }
    }
    // Update is called once per frame
    void Update() 
    {         
        Child.SetActive(isActive);
        if (isActive)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    interactEvent.Invoke();
                }
            }
        }
    }

    private void SetActive(bool v)
    {
        throw new NotImplementedException();
    }

    public void Ping() { Debug.Log("Ping"); } //Test listener
    public void Pong() { Debug.Log("Pong"); } // Test listener
}