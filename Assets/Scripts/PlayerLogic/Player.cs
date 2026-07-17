using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace PlayerLogic
{
    [RequireComponent(typeof(HealthController))]
    [RequireComponent(typeof(WeaponController))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private HealthController _healthController;
        private WeaponController _weaponController;
        private Animator _animator;
        private MainInputAction _mainInputAction;
        private InputAction _pauseInputAction;

        private void Awake()
        {
            _mainInputAction = new MainInputAction();
        }

        private void Start()
        {
            _healthController = GetComponent<HealthController>();
            _weaponController = GetComponent<WeaponController>();
            _animator = GetComponent<Animator>();

            _healthController.OnDie += (_, _) =>
            {
                _weaponController.RemoveWeapon();

                _animator.SetTrigger(Die);

                StartCoroutine(WaitAndLoadMenu());
            };
        }

        private IEnumerator WaitAndLoadMenu()
        {
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

            Destroy(gameObject);

            SceneManager.LoadScene("MainMenu");
        }

        private void OnEnable()
        {
            _pauseInputAction = _mainInputAction.Player.TogglePause;
            _pauseInputAction.Enable();

            _pauseInputAction.performed += OnPause;
        }

        private void OnDisable()
        {
            _pauseInputAction.performed -= OnPause;

            _pauseInputAction.Disable();
        }

        private void OnPause(InputAction.CallbackContext obj)
        {
            PauseController.Instance.TogglePause();
        }
    }
}