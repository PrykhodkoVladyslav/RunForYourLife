using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;
    private PlayerMoveInputAction _playerMoveInputAction;
    private InputAction _moveInputAction;
    private Vector2 _move;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerMoveInputAction = new PlayerMoveInputAction();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
    }

    private void Update()
    {
        _animator.SetBool(IsWalking, (_move.x != 0 || _move.y != 0));
        if (_move.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_move.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _move * (speed * Time.fixedDeltaTime));
    }
}