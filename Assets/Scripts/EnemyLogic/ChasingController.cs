using UnityEngine;

namespace EnemyLogic
{
    public class ChasingController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform point;
        [SerializeField] private float speed;
        private Rigidbody2D _rigidbody;
        private SpriteRotator _spriteRotator;
        private Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRotator = GetComponent<SpriteRotator>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            PauseController.Instance.OnPaused += Pause;
            PauseController.Instance.OnUnpaused += Unpause;
        }

        private void OnDisable()
        {
            PauseController.Instance.OnPaused -= Pause;
            PauseController.Instance.OnUnpaused -= Unpause;
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = Vector2.zero;

            if (PauseController.Instance.IsPaused)
                return;

            if (!target)
            {
                _animator.SetBool(IsWalking, false);
                return;
            }

            Vector2 direction = (target.position - point.position).normalized;

            _rigidbody.MovePosition(_rigidbody.position + direction * (speed * Time.fixedDeltaTime));
            _rigidbody.linearVelocity = Vector2.zero;

            _spriteRotator.ByDirection(direction);

            _animator.SetBool(IsWalking, true);
        }

        private void Pause() => _animator.speed = 0f;
        private void Unpause() => _animator.speed = 1f;
    }
}