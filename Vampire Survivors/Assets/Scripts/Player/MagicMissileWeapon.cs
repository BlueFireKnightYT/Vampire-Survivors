using System.Collections;
using UnityEngine;

public class MagicMissileWeapon : MonoBehaviour, IWeapon, IUpgradable
{
    float searchRadius = 30;

    WeaponSO weaponData;
    Weapon weapon;

    private float damage;

    Transform targetPos;
    [SerializeField] GameObject bulletPrefab;
    public int baseBulletAmount;
    public float multiShotCooldown;

    PoolManager poolManager;

    [SerializeField] LayerMask enemyLayer;

    private void Start()
    {
        poolManager = GetComponent<PoolManager>();
        damage = weaponData.damage;
    }

    public void Fire()
    {
        StartCoroutine(MultiShoot());
    }
    public void Initialize(WeaponSO weaponData, Weapon owner)
    {
        this.weaponData = weaponData;
        weapon = owner;
    }

    public IEnumerator MultiShoot()
    {
        int bulletAmount = baseBulletAmount;
        while (bulletAmount > 0)
        {
            Shoot();
            bulletAmount--;
            yield return new WaitForSeconds(multiShotCooldown);
        }
    }
    void Shoot()
    {
        targetPos = FindNearestEnemy();
        Vector3 direction = targetPos.position - transform.position;

        Quaternion fullRotation = Quaternion.LookRotation(direction);
        float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, zAngle));

        GameObject bullet = pooledObject();
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bullet.transform.position = transform.position;
        bullet.transform.rotation = rotation;

        bullet.SetActive(true);

        bulletScript.damage = damage;
    }

    Transform FindNearestEnemy()
    {
        float shortestDistance = float.MaxValue;

        Transform nearestEnemy = null;

        Collider2D[] allColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, enemyLayer);

        foreach(Collider2D col in allColliders)
        {
            Vector2 displacement = (Vector2)transform.position - (Vector2)col.transform.position;
            float distance = displacement.sqrMagnitude;

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = col.transform;
            }
        }
        
        return nearestEnemy;
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
}
