using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private BoxCollider2D _collider;
    
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _interactAction;
    [SerializeField] private InputActionReference _crouchAction;

    [SerializeField] private float _moveSpeed = 20.0f;
    [SerializeField] private float _jumpForce = 10.5f;
    [SerializeField] private float _gravityMultiplier = 1.5f;
    
    private Vector2 _wishVelocity;
    private Vector2 _moveInput;
    private Vector2 _defaultColliderSize;
    private Vector2 _defaultColliderOffset;
    public float crouchHeightMultiplier = 0.5f;
    
    private bool _isGrounded = false;
    private bool _doJump = false;
    private bool _isCrouching = false;
    public bool isAttacking = false;
    public bool canMove = true;

    [HideInInspector] public UnityEvent OnJump;
    
    private void Start()
    {
        // Get references
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponentInChildren<BoxCollider2D>();
        
        // Bind actions to respective bools
        _crouchAction.action.started += ctx  => _isCrouching = true;
        _crouchAction.action.canceled += ctx => _isCrouching = false;
        
        // Get collider defaults
        _defaultColliderSize = _collider.size;
        _defaultColliderOffset = _collider.offset;
        
        OnJump ??= new UnityEvent();
    }
    
    private void FixedUpdate()
    {
        // Handle rigidbody velocity modification within the physics update loop
        HandleMovement();
    }

    private void Update()
    {
        HandleInput();
        CheckGround();
        HandleJump();
        HandleCrouch();
        HandleSprite();
    }

    private void CheckGround()
    {
        // Set is grounded based on player y velocity
        _isGrounded = _rigidbody.velocity.y != 0 ? _isGrounded = false : _isGrounded = true;
        _animator.SetBool("isJumping", !_isGrounded);

        // If player is not grounded, apply gravity on top of rigidbody gravity
        if (!_isGrounded)
        {
            _wishVelocity.y -= (float)9.15 *  _gravityMultiplier * Time.deltaTime;
        }
    }
    
    private void HandleJump()
    {
        // Break from logic if we are not grounded - prevent double jumping
        // Break if we can't move either
        if (!_isGrounded || !canMove)
            return;

        if (_jumpAction.action.WasPressedThisFrame())
        {
            OnJump.Invoke();
            _doJump = true;
        }
    }

    private void HandleCrouch()
    {
        // Break from logic if we are not grounded or currently attacking
        if (!_isGrounded || isAttacking)
        {
            return;
        }
        
        // Prevent movement during crouch, change collider size, and set bool accordingly
        if (_isCrouching)
        {
            canMove = false;
            _collider.size = new Vector2(_defaultColliderSize.x, _defaultColliderSize.y * crouchHeightMultiplier);
            _collider.offset = new Vector2(_defaultColliderOffset.x, -0.79086f);
            _animator.SetBool("isCrouching", true);
        }
        else
        {
            canMove = true;
            _collider.size = _defaultColliderSize;
            _collider.offset = _defaultColliderOffset;
            _animator.SetBool("isCrouching", false);
        }
    }

    private void HandleSprite()
    {
        // Update isMoving bool according to movement velocity
        if (_wishVelocity.x == 0)
        {
            _animator.SetBool("isMoving", false);
        }
        else
        {
            _animator.SetBool("isMoving", true);
        }

        // Adjust sprite position and flip x accordingly
        // Im just too lazy to fix the imported sprites
        if (_wishVelocity.x < 0)
        {
            _spriteRenderer.flipX = true;
            _spriteRenderer.gameObject.transform.localPosition = new Vector3(-0.5f, 0.25f, 0.0f);
        }
        else if (_wishVelocity.x > 0)
        {
            _spriteRenderer.flipX = false;
            _spriteRenderer.gameObject.transform.localPosition = new Vector3(0.5f, 0.25f, 0.0f);
        }
    }


    
    private void HandleInput()
    {
        // Reset input vector and get movement input value from input system
        _moveInput = Vector2.zero;
        _moveInput = _moveAction.action.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        // Break from logic if player cannot move
        if (!canMove || isAttacking)
        {
            _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
            return;
        }
        
        // Get new wish velocity
        _wishVelocity = new Vector2(_moveInput.x * _moveSpeed * Time.deltaTime, _rigidbody.velocity.y);
        
        // Apply velocity
        _rigidbody.velocity = _wishVelocity;

        // If wishing to jump, apply force and remove ability to jump
        if (_doJump)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _doJump = false;
        }
        
    }
}
