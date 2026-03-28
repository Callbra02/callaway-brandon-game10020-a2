using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] private InputActionReference _attackAction;
    private PlayerController _playerController;
    public GameObject _attackCollider;
    private Animator _animator;
    private bool _canAttack = true;
    private bool _isAttacking = false;
    private bool _isAttackColliderActive = false;

    private float _attackTimerMax = 0.65f;
    private float _attackTimer = 0.0f;

    [HideInInspector] public UnityEvent OnAttack;
    
    
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerController = GetComponentInParent<PlayerController>();
        _attackCollider.SetActive(_isAttackColliderActive);

        OnAttack ??= new UnityEvent();
        OnAttack.AddListener(ToggleAttackCollider);
    }

    void Update()
    {
        _playerController.isAttacking = _isAttacking;
        HandleAttack();
        HandleSprite();
    }

    private void HandleAttack()
    {
        // If attack was pressed and the player can attack, invoke OnAttack
        if (_attackAction.action.WasPressedThisFrame() && _canAttack)
        {
            _isAttacking = true;
            _canAttack = false;
            _playerController.canMove = false;
            OnAttack.Invoke();
        }

        // If attack, increment attacktimer
        if (_isAttacking)
        {
            _attackTimer += Time.deltaTime;
        }

        // If attack timer reaches max, invoke OnAttack and allow for another attack
        if (_attackTimer > _attackTimerMax)
        {
            _attackTimer = 0.0f;
            _canAttack = true;
            _isAttacking = false;
            _playerController.canMove = true;
            OnAttack.Invoke();
        }
    }

    // Set animator attack bool accordingly
    private void HandleSprite()
    {
        _animator.SetBool("isAttacking", _isAttacking);
    }

    // Toggle attack collider per Invoke of OnAttack
    private void ToggleAttackCollider()
    {
        _isAttackColliderActive = !_isAttackColliderActive;
        _attackCollider.SetActive(_isAttackColliderActive);
    }
}
