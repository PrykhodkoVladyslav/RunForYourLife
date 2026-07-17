using UnityEngine;

namespace Weapons
{
    public class Pistol : Weapon
    {
        private static readonly int ShootTrigger = Animator.StringToHash("shoot");
        public GameObject bulletPrefab;
        public GameObject owner;
        public float bulletForce;
        public float fireCooldown;
        public Transform firePoint;
        private Animator _animator;
        private float _timeElapsedAfterFire;

        private void Awake()
        {
            _timeElapsedAfterFire = fireCooldown;

            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (PauseController.Instance.IsPaused)
                return;

            _timeElapsedAfterFire += Time.deltaTime;
        }

        public override void Shoot(Quaternion quaternion)
        {
            if (_timeElapsedAfterFire >= fireCooldown)
            {
                _timeElapsedAfterFire = 0;

                var bullet = Instantiate(bulletPrefab, firePoint.position, quaternion);

                Physics2D.IgnoreCollision(owner.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>(), true);

                var bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.Owner = owner;

                var rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

                _animator.SetTrigger(ShootTrigger);
            }
        }
    }
}