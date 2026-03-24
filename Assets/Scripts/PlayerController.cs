using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _interactAction;

    [SerializeField] private float _moveSpeed = 20.0f;
    [SerializeField] private float _jumpForce = 10.5f;
    [SerializeField] private float _gravityMultiplier = 1.5f;
    private Vector2 _wishVelocity;

    private Vector2 _moveInput;
    private bool isGrounded = false;
    private bool doJump = false;
    
    
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        isGrounded = _rigidbody.velocity.y != 0 ? isGrounded = false : isGrounded = true;

        if (!isGrounded)
        {
            _wishVelocity.y -= (float)9.15 *  _gravityMultiplier * Time.deltaTime;
        }
    }
    
    private void HandleJump()
    {
        if (!isGrounded)
            return;

        if (_jumpAction.action.WasPressedThisFrame())
        {
            doJump = true;
            
        }
    }

    private void HandleSprite()
    {
        if (_wishVelocity.x == 0)
            return;
        
        _spriteRenderer.flipX = _wishVelocity.x < 0 ? true : false;

        if (_wishVelocity.x < 0)
        {
            _spriteRenderer.flipX = true;
            _spriteRenderer.gameObject.transform.localPosition = new Vector3(-0.5f, 0.25f, 0.0f);
        }
        else
        {
            _spriteRenderer.flipX = false;
            _spriteRenderer.gameObject.transform.localPosition = new Vector3(0.5f, 0.25f, 0.0f);
        }
        
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void HandleInput()
    {
        _moveInput = Vector2.zero;
        _moveInput = _moveAction.action.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        _wishVelocity = new Vector2(_moveInput.x * _moveSpeed * Time.deltaTime, _rigidbody.velocity.y);
        _rigidbody.velocity = _wishVelocity;

        if (doJump)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            doJump = false;
        }
        
    }
}
