using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject owner;

    public GameObject Owner
    {
        get => owner;
        set => owner = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != owner)
            Destroy(gameObject);
    }
}