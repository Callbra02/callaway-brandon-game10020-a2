using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] private InputActionReference _attackAction;
    private PlayerController _playerController;
    private bool _canAttack = true;
    private bool _isAttacking = false;
    private Animator _animator;

    private float _attackTimerMax = 0.65f;
    private float _attackTimer = 0.0f;
    
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        HandleAttack();
        HandleSprite();
    }

    private void HandleAttack()
    {
        if (_attackAction.action.WasPressedThisFrame() && _canAttack)
        {
            _isAttacking = true;
            _canAttack = false;
            _playerController.canMove = false;
        }

        if (_isAttacking)
        {
            _attackTimer += Time.deltaTime;
        }

        if (_attackTimer > _attackTimerMax)
        {
            _attackTimer = 0.0f;
            _canAttack = true;
            _isAttacking = false;
            _playerController.canMove = true;
        }
    }

    private void HandleSprite()
    {
        _animator.SetBool("isAttacking", _isAttacking);
    }
}
