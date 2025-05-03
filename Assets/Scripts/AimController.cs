using UnityEngine;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
    public GameObject weaponPivot;
    private WeaponInputAction _weaponInputAction;
    private InputAction _mousePositionInputAction;
    private InputAction _gamepadAimInputAction;
    private Quaternion _rotation;

    private void Awake()
    {
        _weaponInputAction = new WeaponInputAction();
    }

    private void OnEnable()
    {
        _mousePositionInputAction = _weaponInputAction.Aim.MousePosition;
        _mousePositionInputAction.Enable();

        _mousePositionInputAction.performed += OnMousePositionChanged;
        _mousePositionInputAction.canceled += OnMousePositionChanged;

        _gamepadAimInputAction = _weaponInputAction.Aim.GamepadAim;
        _gamepadAimInputAction.Enable();

        _gamepadAimInputAction.performed += OnStickMoved;
        _gamepadAimInputAction.canceled += OnStickMoved;
    }

    private void OnDisable()
    {
        _mousePositionInputAction.performed -= OnMousePositionChanged;
        _mousePositionInputAction.canceled -= OnMousePositionChanged;

        _mousePositionInputAction.Disable();

        _gamepadAimInputAction.performed -= OnStickMoved;
        _gamepadAimInputAction.canceled -= OnStickMoved;

        _gamepadAimInputAction.Disable();
    }

    private void OnMousePositionChanged(InputAction.CallbackContext context)
    {
        var mousePosition = _mousePositionInputAction.ReadValue<Vector2>();

        if (Camera.main == null)
            return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = mouseWorldPosition - weaponPivot.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnStickMoved(InputAction.CallbackContext context)
    {
        var gamepadAim = _gamepadAimInputAction.ReadValue<Vector2>();

        if (gamepadAim == Vector2.zero)
            return;

        float angle = Mathf.Atan2(gamepadAim.y, gamepadAim.x) * Mathf.Rad2Deg;
        _rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        weaponPivot.transform.rotation = _rotation;
    }
}