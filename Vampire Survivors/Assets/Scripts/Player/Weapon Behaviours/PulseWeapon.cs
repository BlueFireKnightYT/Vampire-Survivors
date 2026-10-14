using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PulseWeapon : MonoBehaviour, IWeapon, IUpgradable
{
    GameObject projectile;
    float damage;
    float activeTime = 1;
    int maxLvl;
    Weapon weapon;
    public void Initialize(WeaponSO weaponData, Weapon owner)
    {
        if (projectile == null)
        {
            projectile = Instantiate(weaponData.projectile, transform.position, quaternion.identity);
            projectile.SetActive(false);
        }

        damage = weaponData.damage;
        maxLvl = weaponData.maxLevel;
        weapon = owner;
    }

    public void Fire()
    {
        print(projectile);
        projectile.SetActive(true);
        print("Active");
        projectile.GetComponent<BulletDamage>().damage = damage;

        StartCoroutine(DisableProjectile());
    }

    private IEnumerator DisableProjectile()
    {
        yield return new WaitForSeconds(activeTime);
        projectile.SetActive(false);
    }

    public void Upgrade()
    {
        if(weapon.level < maxLvl)
        { 
            weapon.level++;
            damage *= 1.10f;
            activeTime *= 1.2f;
        }
    }
}
