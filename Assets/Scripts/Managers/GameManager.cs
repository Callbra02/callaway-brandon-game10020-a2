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
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        
        // Event Listeners
        inventoryManager.OnDisplayed.AddListener(uiManager.SetInventoryActive);
        inventoryManager.OnDisplayed.AddListener(ToggleTimeScale);
        inventoryManager.OnDisplayed.AddListener(ToggleCursorLock);
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
}
