using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float _health;
    private HealthBar _healthBar;
    private bool _died;
    public event EventHandler<EventArgs> OnDie;

    private void Awake()
    {
        _died = false;

        _health = maxHealth;

        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public float Health
    {
        get => _health;
        set
        {
            if (value <= 0)
            {
                if (_died)
                    return;

                _died = true;
                OnDie?.Invoke(this, EventArgs.Empty);
            }
            else if (value > maxHealth)
            {
                SetHealth(maxHealth);
            }
            else
            {
                SetHealth(value);
            }
        }
    }

    public bool IsDied => _died;

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    private void SetHealth(float value)
    {
        _health = value;

        if (_healthBar)
            _healthBar.SetHealth(_health / maxHealth);
    }
}