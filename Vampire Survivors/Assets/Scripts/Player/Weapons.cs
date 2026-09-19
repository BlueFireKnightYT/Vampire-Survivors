using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public List <IWeapon> weaponList = new List<IWeapon>(6);

    private void Awake()
    {
        MagicMissileWeapon starterWeaponScript = GetComponent<MagicMissileWeapon>();
        AddWeapon(starterWeaponScript.GetComponent<IWeapon>());
    }
    private void Update()
    {
        foreach(IWeapon weapon in weaponList)
        {
            weapon.AutoShoot();
        }
    }

    public void AddWeapon(IWeapon addedWeapon)
    {
        if (weaponList.Count < 6)
        {
            weaponList.Add(addedWeapon);
        }
    }
}
