using UnityEngine;

public class AxeBehaviour : MonoBehaviour, IWeapon
{
    private WeaponSO weaponData;
    private Weapon weapon;
    public void Fire()
    {
        Debug.Log("Fire" + weaponData.weaponName);
    }

    public void Initialize(WeaponSO weaponData, Weapon owner)
    {
        this.weaponData = weaponData;
        weapon = owner;
    }
}
