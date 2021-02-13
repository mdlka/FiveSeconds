using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private Vector2 _groundCheckerOffset;
    [SerializeField] private float _groundCheckerRadius;
    [SerializeField] private LayerMask _groundLayerMask;

    private Rigidbody2D _rigidbody2D;
    private MainInputAction _input;

    private float _movementX;
    private float _addForceX;
    private Collider2D[] _results = new Collider2D[1];
    private bool _isGrounded;

    public Vector2 Velocity => _rigidbody2D.velocity;
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = new MainInputAction();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Jump.performed += context => TryJump();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Jump.performed -= context => TryJump();

        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
    }

    private void Update()
    {
        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();
        _movementX = direction.x * _moveSpeed;

        _isGrounded = false;

        Physics2D.OverlapCircleNonAlloc(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2) +
                                        _groundCheckerOffset, _groundCheckerRadius, _results, _groundLayerMask);

        if (_results[0] != null)
        {
            _isGrounded = true;
            _addForceX = 0;
            _results[0] = null;
        }

        if (direction.x != 0)
            transform.localRotation = Quaternion.Euler(0, direction.x > 0 ? 0 : 180, 0);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_movementX * Time.fixedDeltaTime + _addForceX, _rigidbody2D.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2) + 
                              _groundCheckerOffset, _groundCheckerRadius);
    }

    private void TryJump()
    {
        if (_isGrounded)
        {
            _rigidbody2D.velocity = Vector2.up * Mathf.Sqrt(_jumpHeight * -2 * Physics2D.gravity.y);
        }
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        _addForceX = force.x;
    }
}
