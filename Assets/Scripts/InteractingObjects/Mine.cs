using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Mine : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _throwForce;
    [SerializeField] private ParticleSystem _explosion;

    private bool _isDestroyed;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDestroyed == false && collision.TryGetComponent(out Player player))
        {
            Vector2 direction = collision.transform.position - transform.position;
            Vector2 throwDirection = direction / direction.magnitude;
            
            player.TakeDamage(_damage);
            player.GetComponent<PlayerMovement>().AddForce(throwDirection * _throwForce);

            _explosion.Play();

            _isDestroyed = true;
            _spriteRenderer.color = Color.clear;
            Destroy(gameObject, _explosion.main.duration);
        }
    }
}
