using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController playerController;

    public InventoryManager inventoryManager;
    public UIManager uiManager;

    private bool _toggleTimeScale = false;
    

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
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        
        // Event Listeners
        inventoryManager.OnDisplayed.AddListener(uiManager.SetInventoryActive);
        inventoryManager.OnDisplayed.AddListener(ToggleTimeScale);
    }

    void Update()
    {
        
    }

    // Swaps timescale from 1.0f to 0.0f
    public void ToggleTimeScale()
    {
        _toggleTimeScale = !_toggleTimeScale;

        float currentTimeScale = _toggleTimeScale ? 0.0f : 1.0f;

        Time.timeScale = currentTimeScale;
    }
}
