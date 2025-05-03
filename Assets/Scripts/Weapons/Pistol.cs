using UnityEngine;

namespace Weapons
{
    public class Pistol : Weapon
    {
        public GameObject bulletPrefab;
        public GameObject owner;
        public float bulletForce;
        public float fireCooldown;

        private float _lastFireTime = -Mathf.Infinity;

        public override void Shoot(Quaternion quaternion)
        {
            if (Time.time < _lastFireTime + fireCooldown)
                return;

            _lastFireTime = Time.time;

            var bullet = Instantiate(bulletPrefab, transform.position, quaternion);

            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.owner = owner;

            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
        }
    }
}