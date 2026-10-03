using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponSO weaponData;

    private IWeapon weaponBehaviour;
    public float cooldown;

    public int level;

    private void Awake()
    {
        if (TryGetComponent<IWeapon>(out weaponBehaviour))
        {
            weaponBehaviour.Initialize(weaponData, this);
        }
        else
        {
            Debug.LogWarning("No Behaviour found on " + gameObject.name);
        }
        cooldown = weaponData.interval;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0 && level > 0)
        {
            weaponBehaviour.Fire();
            cooldown = weaponData.interval;
        }
    }
}