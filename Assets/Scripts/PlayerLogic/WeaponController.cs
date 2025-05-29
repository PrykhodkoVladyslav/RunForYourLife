using UnityEngine;

namespace PlayerLogic
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private GameObject weaponPivot;
        private WeaponPivot _weaponPivot;

        private void Start()
        {
            _weaponPivot = weaponPivot.GetComponent<WeaponPivot>();
        }

        public void SetWeapon(GameObject weapon)
        {
            RemoveWeapon();

            weapon.transform.parent = _weaponPivot.transform;
            _weaponPivot.Weapon = weapon;
        }

        public void RemoveWeapon()
        {
            if (!HasWeapon())
                return;

            _weaponPivot.Weapon.transform.parent = null;
            Destroy(_weaponPivot.Weapon.gameObject);
            _weaponPivot.Weapon = null;
        }

        public bool HasWeapon()
        {
            return _weaponPivot.Weapon;
        }
    }
}