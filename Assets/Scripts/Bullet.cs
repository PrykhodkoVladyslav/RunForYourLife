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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == owner)
            return;

        Destroy(gameObject);

        var healthController = collision.gameObject.GetComponent<HealthController>();
        if (healthController)
            healthController.TakeDamage(damage);
    }
}