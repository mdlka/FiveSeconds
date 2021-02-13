using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.TookHit += OnTookHit;
    }

    private void OnDisable()
    {
        _player.TookHit -= OnTookHit;
    }

    private void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_playerMovement.Velocity.x));
        _animator.SetFloat("VelocityY", _playerMovement.Velocity.y);
        _animator.SetBool("IsGrounded", _playerMovement.IsGrounded);
    }

    private void OnTookHit()
    {
        _animator.SetTrigger("TakeHit");
        _animator.SetInteger("Health", _player.Health);
    }
}
