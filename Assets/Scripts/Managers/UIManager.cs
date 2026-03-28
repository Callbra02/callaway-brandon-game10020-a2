using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    
    public GameObject inventoryObject;
    public LevelOrb levelOrb;
    private bool _isInventoryDisplayed = false;

    public Slider healthSlider;
    public Slider staminaSlider;
    
    void Start()
    {
        // Set inventory to unactive;
        inventoryObject.SetActive(false);
    }

    // Toggle inventory object activity
    public void SetInventoryActive()
    {
        _isInventoryDisplayed = !_isInventoryDisplayed;
        inventoryObject.SetActive(_isInventoryDisplayed);
    }

    // Update health slider with currencies controllers current values
    public void UpdateHealthSlider()
    {
        healthSlider.value = gameManager.currenciesController.currentHealth / gameManager.currenciesController.maxHealth;
    }

    // Update stamina slider with currencies controllers current values
    public void UpdateStaminaSlider()
    {
        staminaSlider.value = gameManager.currenciesController.currentStamina / gameManager.currenciesController.maxStamina;
    }
}
