using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    [SerializeField] private float damage;
    private Rigidbody2D _rb;
    private Vector2 _savedVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PauseController.Instance.OnPaused += Pause;
        PauseController.Instance.OnUnpaused += Resume;
    }

    private void OnDisable()
    {
        PauseController.Instance.OnPaused -= Pause;
        PauseController.Instance.OnUnpaused -= Resume;
    }

    private void Pause()
    {
        _savedVelocity = _rb.linearVelocity;
        _rb.linearVelocity = Vector2.zero;
        _rb.simulated = false;
    }

    private void Resume()
    {
        _rb.simulated = true;
        _rb.linearVelocity = _savedVelocity;
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

    public GameObject Owner
    {
        get => owner;
        set => owner = value;
    }
}