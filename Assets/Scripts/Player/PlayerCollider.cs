using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollider : MonoBehaviour
{  
    [HideInInspector] public UnityEvent OnSpikeCollision;

    [HideInInspector] public UnityEvent OnFireCollisionStay;

    [HideInInspector] public UnityEvent OnHealthItemCollision;

    private void Start()
    {
        OnSpikeCollision ??= new UnityEvent();
        OnHealthItemCollision ??= new UnityEvent();
        OnFireCollisionStay ??= new UnityEvent();
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Spike"))
        {
            OnSpikeCollision.Invoke();
        }

        if (collider.gameObject.CompareTag("HealthItem"))
        {
            OnHealthItemCollision.Invoke();
            Destroy(collider.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Fire"))
        {
            OnFireCollisionStay.Invoke();
        }
    }
}
