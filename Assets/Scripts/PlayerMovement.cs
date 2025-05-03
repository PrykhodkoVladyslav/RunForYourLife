using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;
    private PlayerMoveInputAction _playerMoveInputAction;
    private InputAction _moveInputAction;
    private Vector2 _move;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerMoveInputAction = new PlayerMoveInputAction();
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

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _move * (speed * Time.fixedDeltaTime));
    }
}