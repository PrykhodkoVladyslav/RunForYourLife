using UnityEngine;
using Weapons;

public class WeaponPivot : MonoBehaviour
{
    public GameObject weapon;

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
        if (!weapon)
            return;

        weapon.GetComponent<Weapon>()
            .Shoot(transform.rotation);
    }
}