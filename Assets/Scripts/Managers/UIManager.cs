using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryObject;
    public LevelOrb levelOrb;
    private bool _isInventoryDisplayed = false;
    
    void Start()
    {
        inventoryObject.SetActive(false);
    }

    public void SetInventoryActive()
    {
        _isInventoryDisplayed = !_isInventoryDisplayed;
        inventoryObject.SetActive(_isInventoryDisplayed);
    }
}
