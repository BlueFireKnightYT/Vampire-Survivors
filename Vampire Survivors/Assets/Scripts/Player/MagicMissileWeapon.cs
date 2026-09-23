using System.Collections;
using UnityEngine;

public class MagicMissileWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] float shootCooldown;
    [SerializeField] float damage;
    float remainingCooldown;
    float searchRadius = 30;

    int level = 1;

    Transform targetPos;
    [SerializeField] GameObject bulletPrefab;
    public int baseBulletAmount;
    public float multiShotCooldown;

    [SerializeField] LayerMask enemyLayer;

    private void Awake()
    {
        remainingCooldown = shootCooldown;
    }

    public void AutoShoot()
    {
        remainingCooldown -= Time.deltaTime;
        if(remainingCooldown <= 0)
        { 
            StartCoroutine(MultiShoot());
            remainingCooldown = shootCooldown;
        }
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

            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();

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
        Debug.Log(nearestEnemy.name);
        
        return nearestEnemy;
    }

    void LevelUp()
    {
        // + 1 level
        // +20% damage
        // + 1 bullet every 2 levels
        // - 5% cooldown
        // max level = level 6
        if (level < 6)
        {
            level++;
            damage *= 1.2f;
            shootCooldown *= 0.95f;

            int result = level % 2;
            if (result == 0) baseBulletAmount++;
        }
    }
}
