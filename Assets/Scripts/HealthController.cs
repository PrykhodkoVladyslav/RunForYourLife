using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float _health;

    private void Awake()
    {
        _health = maxHealth;
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
                _health = maxHealth;
            }
            else
            {
                _health = value;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}