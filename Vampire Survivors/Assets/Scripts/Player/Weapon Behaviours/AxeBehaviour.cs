using System.Collections;
using UnityEngine;

public class AxeBehaviour : MonoBehaviour, IWeapon, IUpgradable
{
    private WeaponSO weaponData;
    private Weapon weapon;

    float damage;
    int baseBulletAmount;

    PoolManager poolManager;

    private void Start()
    {
        poolManager = GetComponent<PoolManager>();
    }
    public void Initialize(WeaponSO weaponData, Weapon owner)
    {
        this.weaponData = weaponData;
        damage = weaponData.damage;

        weapon = owner;
    }
    public void Fire()
    {
        //enable axe prefab
        GameObject projectile = pooledObject();
        projectile.transform.position = transform.position;
        projectile.SetActive(true);

        //give info Damage
        projectile.GetComponent<ThrowingProjectile>().damage = damage;
        // get RB and add force

        float dir = Random.Range(-250, 250);
        float height = Random.Range(300, 600);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(dir,height));
        rb.angularVelocity = -360 * Mathf.Sign(dir);
        //Timed Destroy
        StartCoroutine(DisableAxe(projectile));
    }

    GameObject pooledObject()
    {
        GameObject foundObject = null;
        foreach (GameObject listObject in poolManager.objectPool)
        {
            if (!listObject.activeSelf)
            {
                foundObject = listObject;
                break;
            }
        }
        return foundObject;
    }

    IEnumerator DisableAxe(GameObject Projectile)
    {
        yield return new WaitForSeconds(weaponData.interval);
        Projectile.SetActive(false);
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
