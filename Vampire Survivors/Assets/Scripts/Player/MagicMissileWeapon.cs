using UnityEngine;

public class MagicMissileWeapon : MonoBehaviour
{
    [SerializeField] float shootCooldown;
    [SerializeField] float damage;
    float remainingCooldown;
    float searchRadius = 15;

    Transform targetPos;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] LayerMask enemyLayer;

    private void Awake()
    {
        remainingCooldown = shootCooldown;
    }

    private void Update()
    {
        remainingCooldown -= Time.deltaTime;

        if(remainingCooldown <= 0)
        {
            remainingCooldown = shootCooldown;
            Shoot();
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
        float shortestDistance = 999;

        Transform nearestEnemy = null;

        Collider2D[] allColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, enemyLayer);

        foreach(Collider2D col in allColliders)
        {
            float distance = Vector2.Distance(transform.position, col.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = col.transform;
            }
        }
        Debug.Log(nearestEnemy.name);
        return nearestEnemy;
    }
}
