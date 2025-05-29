using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerLogic
{
    [RequireComponent(typeof(HealthController))]
    [RequireComponent(typeof(WeaponController))]
    public class Player : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private HealthController _healthController;
        private WeaponController _weaponController;
        private Animator _animator;

        private void Start()
        {
            _healthController = GetComponent<HealthController>();
            _weaponController = GetComponent<WeaponController>();
            _animator = GetComponent<Animator>();

            _healthController.OnDie += (sender, args) =>
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
    }
}