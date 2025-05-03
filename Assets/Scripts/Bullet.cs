using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject owner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != owner)
            Destroy(gameObject);
    }
}