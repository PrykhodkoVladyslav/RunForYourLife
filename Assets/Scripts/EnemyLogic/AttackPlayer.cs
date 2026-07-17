using UnityEngine;

namespace EnemyLogic
{
    public class AttackPlayer : MonoBehaviour
    {
        [SerializeField] private float damage;

        private GameObject _player;
        private HealthController _healthController;

        private GameObject Player
        {
            get => _player;
            set
            {
                _player = value;
                _healthController = _player ? _player.GetComponent<HealthController>() : null;
            }
        }

        private void FixedUpdate()
        {
            if (PauseController.Instance.IsPaused)
                return;

            if (!_healthController)
                return;

            _healthController.TakeDamage(damage * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            Player = other.gameObject;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject != Player)
                return;

            Player = null;
        }
    }
}