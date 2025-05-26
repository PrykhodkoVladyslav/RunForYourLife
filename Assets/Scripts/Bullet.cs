using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    [SerializeField] private float damage;

    public GameObject Owner
    {
        get => owner;
        set => owner = value;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == owner)
            return;

        var healthController = other.gameObject.GetComponent<HealthController>();
        if (healthController)
            healthController.TakeDamage(damage);

        Destroy(gameObject);
    }
}