using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float _health;
    private HealthBar _healthBar;

    private void Awake()
    {
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
                Destroy(gameObject);
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