using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireJet : MonoBehaviour, IAttackable
{
    private SpriteRenderer _spriteRenderer;
    public BoxCollider2D damageCollider;

    private bool _isToggledOn = true;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnAttack()
    {
        _isToggledOn = !_isToggledOn;

        _spriteRenderer.enabled = _isToggledOn;
        damageCollider.enabled = _isToggledOn;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("AttackCollider"))
        {
            OnAttack();
        }
    }
}
