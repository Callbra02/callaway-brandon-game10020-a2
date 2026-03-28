using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrenciesController : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public float currentHealth { get; private set; }
    public float maxStamina = 100.0f;
    public float currentStamina { get; private set; }

    private BoxCollider2D _boxCollider;

    [HideInInspector] 
    public UnityEvent OnHealthChanged;
    public UnityEvent OnStaminaChanged;

    public UnityEvent OnDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        OnHealthChanged ??= new UnityEvent();
        OnStaminaChanged ??= new UnityEvent();
        OnDeath ??= new UnityEvent();
        
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        
    }

    // Update is called once per frame
    private void Update()
    {
        HandleStamina();
    }

    public void HandleStamina()
    {
        if (currentStamina < maxStamina && Time.timeScale != 0)
        {
            currentStamina += 5.0f  * Time.deltaTime;
            OnStaminaChanged.Invoke();
        } 
    }

    public void OnHealthPickup()
    {
        currentHealth += 25.0f;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        OnHealthChanged.Invoke();
    }

    public void TaxHealth(float amount)
    {
        if (currentHealth < 0)
        {
            Die();
        }
        
        currentHealth -= amount;
        OnHealthChanged.Invoke();
    }

    public void TaxHealthSpike()
    {
        TaxHealth(10);
    }

    public void TaxStamina(float amount)
    {
        currentStamina -= amount;
        OnStaminaChanged.Invoke();
    }

    public void TaxStaminaJump()
    {
        TaxStamina(20);
    }

    public void Die()
    {
        OnDeath.Invoke();
    }
    
}
