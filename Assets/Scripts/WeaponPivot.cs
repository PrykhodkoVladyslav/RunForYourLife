using UnityEngine;
using Weapons;

public class WeaponPivot : MonoBehaviour
{
    public GameObject weapon;

    public void Shoot()
    {
        if (!weapon)
            return;

        weapon.GetComponent<Weapon>()
            .Shoot(transform.rotation);
    }
}