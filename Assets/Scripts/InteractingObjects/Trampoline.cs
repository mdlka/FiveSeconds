using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Trampoline : MonoBehaviour
{
    [SerializeField] private float _repulsiveForce;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            _animator.SetTrigger("Jump");
            player.AddForce(transform.up * _repulsiveForce);
        }
    }
}
