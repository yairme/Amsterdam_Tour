using UnityEngine.Events;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private int Radius;
    [SerializeField] private GameObject Child;
    [SerializeField] private UnityEvent InteractEvent;
    private bool IsItActive;
    public bool IsActive { get => IsItActive;  set => IsItActive = value; } 

    void Start()
    {
        if (InteractEvent == null) { InteractEvent = new UnityEvent(); }
    }
    // Update is called once per frame
    void Update() 
    {         
        Child.SetActive(IsItActive);
        if (IsItActive)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    InteractEvent.Invoke();
                }
            }
        }
    }
    private void SetActive(bool v)
    {
        throw new NotImplementedException();
    }
}