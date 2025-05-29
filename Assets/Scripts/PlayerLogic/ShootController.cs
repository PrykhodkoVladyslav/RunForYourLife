using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerLogic
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private GameObject weaponPivot;
        private WeaponInputAction _weaponInputAction;
        private InputAction _shootInputAction;
        private bool _isShooting;
        private WeaponPivot _weaponPivot;

        private void Awake()
        {
            _weaponInputAction = new WeaponInputAction();
        }

        private void OnEnable()
        {
            _shootInputAction = _weaponInputAction.Shoot.ShootButton;
            _shootInputAction.Enable();

            _shootInputAction.performed += OnShoot;
            _shootInputAction.canceled += OnShoot;
        }

        private void OnShoot(InputAction.CallbackContext context)
        {
            _isShooting = context.performed;
        }

        private void OnDisable()
        {
            _shootInputAction.performed -= OnShoot;
            _shootInputAction.canceled -= OnShoot;

            _shootInputAction.Disable();
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
    }
}