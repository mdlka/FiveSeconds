using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _health;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction TookHit;
    public event UnityAction Died;

    public int Health => _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage == 0) return;

        _health -= damage;

        TookHit?.Invoke();
        HealthChanged?.Invoke(_health > 0 ? _health : 0, _maxHealth);

        if (_health <= 0)
            Death();
    }

    private void Death()
    {
        Died?.Invoke();

        GetComponent<PlayerMovement>().enabled = false;
        this.enabled = false;
    }
}
