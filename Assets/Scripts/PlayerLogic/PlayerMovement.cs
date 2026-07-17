using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody2D _rigidbody;
        private MainInputAction _mainInputAction;
        private InputAction _moveInputAction;
        private Vector2 _move;
        private Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private SpriteRotator _spriteRotator;
        private HealthController _healthController;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _mainInputAction = new MainInputAction();
            _animator = GetComponent<Animator>();
            _spriteRotator = GetComponent<SpriteRotator>();
            _healthController = GetComponent<HealthController>();
        }

        private void OnEnable()
        {
            PauseController.Instance.OnPaused += Disable;
            PauseController.Instance.OnUnpaused += Enable;

            Enable();
        }

        private void OnDisable()
        {
            PauseController.Instance.OnPaused -= Disable;
            PauseController.Instance.OnUnpaused -= Enable;

            Disable();
        }

        private void Enable()
        {
            _moveInputAction = _mainInputAction.Player.Move;
            _moveInputAction.Enable();

            _moveInputAction.performed += OnMove;
            _moveInputAction.canceled += OnMove;

            _animator.speed = 1;
        }

        private void Disable()
        {
            _moveInputAction.performed -= OnMove;
            _moveInputAction.canceled -= OnMove;

            _moveInputAction.Disable();

            _animator.speed = 0;

            _move = Vector2.zero;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
            _spriteRotator.ByDirection(_move);
        }

        private void Update()
        {
            if (PauseController.Instance.IsPaused)
                return;

            if (_healthController.IsDied)
                return;

            _animator.SetBool(IsWalking, (_move.x != 0 || _move.y != 0));
        }

        private void FixedUpdate()
        {
            if (_healthController.IsDied)
                return;

            _rigidbody.MovePosition(_rigidbody.position + _move * (speed * Time.fixedDeltaTime));
        }
    }
}