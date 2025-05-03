using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weaponPivot;
    private WeaponPivot _weaponPivot;

    private void Start()
    {
        _weaponPivot = weaponPivot.GetComponent<WeaponPivot>();
    }

    public void SetWeapon(GameObject weapon)
    {
        RemoveWeapon();

        _weaponPivot.weapon = weapon;
        _weaponPivot.weapon.transform.parent = _weaponPivot.transform;
    }

    public void RemoveWeapon()
    {
        if (!HasWeapon())
            return;

        _weaponPivot.weapon.transform.parent = null;
        Destroy(_weaponPivot.weapon.gameObject);
        _weaponPivot.weapon = null;
    }

    public bool HasWeapon()
    {
        return _weaponPivot.weapon;
    }
}