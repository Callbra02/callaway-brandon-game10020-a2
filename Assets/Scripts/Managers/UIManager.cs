using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryObject;
    public LevelOrb levelOrb;

    
    void Start()
    {
        inventoryObject.SetActive(false);
    }

    public void SetInventoryActive(bool state)
    {
        inventoryObject.SetActive(state);
    }
}
