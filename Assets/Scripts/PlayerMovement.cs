using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody;
    private PlayerMoveInputAction _playerMoveInputAction;
    private InputAction _moveInputAction;
    private Vector2 _move;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private SpriteRotator _spriteRotator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerMoveInputAction = new PlayerMoveInputAction();
        _animator = GetComponent<Animator>();
        _spriteRotator = GetComponent<SpriteRotator>();
    }

    private void OnEnable()
    {
        _moveInputAction = _playerMoveInputAction.Player.Move;
        _moveInputAction.Enable();

        _moveInputAction.performed += OnMove;
        _moveInputAction.canceled += OnMove;
    }

    private void OnDisable()
    {
        _moveInputAction.performed -= OnMove;
        _moveInputAction.canceled -= OnMove;

        _moveInputAction.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
        _spriteRotator.ByDirection(_move);
    }

    private void Update()
    {
        _animator.SetBool(IsWalking, (_move.x != 0 || _move.y != 0));
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _move * (speed * Time.fixedDeltaTime));
    }
}