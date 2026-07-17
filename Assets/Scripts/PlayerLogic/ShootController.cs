using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerLogic
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private GameObject weaponPivot;
        private MainInputAction _mainInputAction;
        private InputAction _shootInputAction;
        private bool _isShooting;
        private WeaponPivot _weaponPivot;

        private void Awake()
        {
            _mainInputAction = new MainInputAction();
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

        private void Start()
        {
            _weaponPivot = weaponPivot.GetComponent<WeaponPivot>();
        }

        private void Update()
        {
            if (_isShooting)
                _weaponPivot.Shoot();
        }

        private void Enable()
        {
            _shootInputAction = _mainInputAction.Shoot.ShootButton;
            _shootInputAction.Enable();

            _shootInputAction.performed += OnShoot;
            _shootInputAction.canceled += OnShoot;
        }

        private void Disable()
        {
            _isShooting = false;

            _shootInputAction.performed -= OnShoot;
            _shootInputAction.canceled -= OnShoot;

            _shootInputAction.Disable();
        }

        private void OnShoot(InputAction.CallbackContext context)
        {
            _isShooting = context.performed;
        }
    }
}