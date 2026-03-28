using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour, IAttackable
{
    private bool hasBeenAttacked = false;
    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void OnAttack()
    {
        // Break from logic if statue has been attacked
        if (hasBeenAttacked)
            return;

        _spriteRenderer.color = Color.red;
        hasBeenAttacked = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("AttackCollider"))
        {
            OnAttack();
        }
    }
}
