using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _interactAction;

    [SerializeField] private float _moveSpeed = 20.0f;
    [SerializeField] private float _jumpForce = 10.5f;
    [SerializeField] private float _gravityMultiplier = 1.5f;
    
    private Vector2 _wishVelocity;
    private Vector2 _moveInput;
    private bool _isGrounded = false;
    private bool _doJump = false;
    public bool canMove = true;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleInput();
        CheckGround();
        HandleJump();
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
            _doJump = true;
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
        // Im just too lazy to fix the imported sprites -Brandon
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

    private void FixedUpdate()
    {
        // Handle rigidbody velocity modification within the physics update loop
        HandleMovement();
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
        if (!canMove)
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
