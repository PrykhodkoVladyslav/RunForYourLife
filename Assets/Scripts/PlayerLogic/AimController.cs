using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerLogic
{
    public class AimController : MonoBehaviour
    {
        [SerializeField] private GameObject weaponPivot;
        private MainInputAction _mainInputAction;
        private InputAction _mousePositionInputAction;
        private InputAction _gamepadAimInputAction;
        private Quaternion _rotation;

        private void Awake()
        {
            _mainInputAction = new MainInputAction();
        }

        private void OnEnable()
        {
            Enable();

            PauseController.Instance.OnPaused += Disable;
            PauseController.Instance.OnUnpaused += Enable;
        }

        private void OnDisable()
        {
            Disable();

            PauseController.Instance.OnPaused -= Disable;
            PauseController.Instance.OnUnpaused -= Enable;
        }

        private void Enable()
        {
            _mousePositionInputAction = _mainInputAction.Aim.MousePosition;
            _mousePositionInputAction.Enable();

            _mousePositionInputAction.performed += OnMousePositionChanged;
            _mousePositionInputAction.canceled += OnMousePositionChanged;

            _gamepadAimInputAction = _mainInputAction.Aim.GamepadAim;
            _gamepadAimInputAction.Enable();

            _gamepadAimInputAction.performed += OnStickMoved;
            _gamepadAimInputAction.canceled += OnStickMoved;
        }

        private void Disable()
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

            if (!Camera.main)
                return;

            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = mouseWorldPosition - weaponPivot.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rotation = Quaternion.Euler(0, 0, angle);
        }

        private void OnStickMoved(InputAction.CallbackContext context)
        {
            var gamepadAim = _gamepadAimInputAction.ReadValue<Vector2>();

            if (gamepadAim == Vector2.zero)
                return;

            var angle = Mathf.Atan2(gamepadAim.y, gamepadAim.x) * Mathf.Rad2Deg;
            _rotation = Quaternion.Euler(0, 0, angle);
        }

        private void Update()
        {
            weaponPivot.transform.rotation = _rotation;
        }
    }
}