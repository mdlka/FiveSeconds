using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerFallDamage : MonoBehaviour
{
    [SerializeField] private float _maxSafeDistanceToGround;
    [SerializeField] private int _minDamage;
    [SerializeField] private LayerMask _layerMask;

    private Player _player;
    private PlayerMovement _playerMovement;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hits = new RaycastHit2D[1];

    private float _maxDistanceToGround;
    private int _damage;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();

        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        if(_playerMovement.Velocity.y < 0)
        {
            Physics2D.Raycast(transform.position, Vector2.down, _contactFilter, _hits);
            float distanceToGround = _hits[0].distance;

            if (distanceToGround > _maxSafeDistanceToGround && distanceToGround > _maxDistanceToGround)
            {
                _damage = (int)(_minDamage * (distanceToGround - _maxSafeDistanceToGround));
                _maxDistanceToGround = distanceToGround;
            }
                
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Ground ground))
        {
            _player.TakeDamage(_damage);
        }

        _damage = 0;
        _maxDistanceToGround = 0;
    }
}
