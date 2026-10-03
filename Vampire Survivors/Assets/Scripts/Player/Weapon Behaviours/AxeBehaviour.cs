using UnityEngine;

public class AxeBehaviour : MonoBehaviour, IWeapon, IUpgradable
{
    private WeaponSO weaponData;
    private Weapon weapon;

    float damage;
    int baseBulletAmount;

    private void Start()
    {
        damage = weaponData.damage;
    }
    public void Initialize(WeaponSO weaponData, Weapon owner)
    {
        this.weaponData = weaponData;
        weapon = owner;
    }
    public void Fire()
    {
        Debug.Log("Fire" + weaponData.weaponName);
    }

    public void Upgrade()
    {
        // + 1 level
        // +20% damage
        // + 1 bullet every 2 levels
        // - 5% cooldown
        // max level = level 6

        if (weapon.level < weaponData.maxLevel)
        {
            weapon.level++;
            damage *= 1.2f;
            weapon.cooldown *= 0.95f;

            int result = weapon.level % 2;
            if (result == 0) baseBulletAmount++;
        }
    }
}
