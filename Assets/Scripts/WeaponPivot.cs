using UnityEngine;
using Weapons;

public class WeaponPivot : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    private Weapon _weaponComponent;

    public GameObject Weapon
    {
        get => weapon;
        set
        {
            _weaponComponent = weapon ? weapon.GetComponent<Weapon>() : null;

            weapon = value;
        }
    }

    private void Start()
    {
        _weaponComponent = weapon ? weapon.GetComponent<Weapon>() : null;
    }

    private void Update()
    {
        FlipWeaponIfNeeded();
    }

    private void FlipWeaponIfNeeded()
    {
        if (!weapon)
            return;

        var aimDirection = transform.right;

        var scale = weapon.transform.localScale;
        scale.y = Mathf.Abs(scale.y);
        if (aimDirection.x < 0)
            scale.y *= -1;

        weapon.transform.localScale = scale;
    }

    public void Shoot()
    {
        if (!_weaponComponent)
            return;

        _weaponComponent.Shoot(transform.rotation);
    }
}