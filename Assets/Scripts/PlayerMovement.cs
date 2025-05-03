using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private PlayerMoveInputAction _moveInputAction;
    private InputAction _move;

    private void Awake()
    {
        _moveInputAction = new PlayerMoveInputAction();
    }

    private void OnEnable()
    {
        _move = _moveInputAction.Player.Move;
        _move.Enable();
    }

    private void OnDisable()
    {
        _move.Disable();
    }

    private void Update()
    {
        var move = _move.ReadValue<Vector2>();

        transform.position += new Vector3(move.x, move.y, 0) * (speed * Time.deltaTime);
    }
}