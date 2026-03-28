using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController playerController;
    public InventoryManager inventoryManager;
    public UIManager uiManager;
    public CurrenciesController currenciesController;
    public PlayerCollider playerCollider;

    private bool _pauseTime = false;
    private bool _isCursorVisible = true;
    

    // Singleton 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCollider = playerController.gameObject.GetComponentInChildren<PlayerCollider>();
        
        // Event Listeners
        inventoryManager.OnDisplayed.AddListener(uiManager.SetInventoryActive);
        inventoryManager.OnDisplayed.AddListener(ToggleTimeScale);
        inventoryManager.OnDisplayed.AddListener(ToggleCursorLock);

        // Health and stamina UI event listeners
        currenciesController.OnHealthChanged.AddListener(uiManager.UpdateHealthSlider);
        currenciesController.OnStaminaChanged.AddListener(uiManager.UpdateStaminaSlider);
        currenciesController.OnDeath.AddListener(EndGame);
        
        // Player collider events
        playerCollider.OnSpikeCollision.AddListener(currenciesController.TaxHealthSpike);
        playerCollider.OnHealthItemCollision.AddListener(currenciesController.OnHealthPickup);
        playerCollider.OnFireCollisionStay.AddListener(currenciesController.TaxHealthFire);

        
        playerController.OnJump.AddListener(currenciesController.TaxStaminaJump);
    }

    // Swaps timescale from 1.0f to 0.0f
    public void ToggleTimeScale()
    {
        _pauseTime = !_pauseTime;

        float currentTimeScale = _pauseTime ? 0.0f : 1.0f;

        Time.timeScale = currentTimeScale;
    }

    // Toggle cursor lock state
    public void ToggleCursorLock()
    {
        _isCursorVisible = !_isCursorVisible;
        Cursor.lockState = _isCursorVisible ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Level2");
    }
}
