using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InputActionReference _inventoryAction;
    
    [HideInInspector] 
    public UnityEvent OnDisplayed;
    
    void Start()
    {
        OnDisplayed ??= new UnityEvent();
    }

    void Update()
    {
        if (_inventoryAction.action.WasPressedThisFrame())
        {
            ToggleUI();
        }
    }

    // Toggle UI event
    public void ToggleUI()
    {
        OnDisplayed.Invoke();
    }
}
