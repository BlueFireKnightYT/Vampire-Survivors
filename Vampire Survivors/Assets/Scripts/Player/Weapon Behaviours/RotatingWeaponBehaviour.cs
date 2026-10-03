using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class RotatingWeaponBehaviour : MonoBehaviour, IWeapon, IUpgradable
{
    List<GameObject> projectiles = new List<GameObject>();
    GameObject projectilePrefab;

    float damage;
    float timeEnabled = 3f;
    int maxLevel;

    [SerializeField] float radius = 3f;
    [SerializeField] float orbitSpeed = 3f; // Radians per second

    Rigidbody2D playerRb;
    bool coroutineOn;

    Weapon weapon;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerRb = playerObj.GetComponent<Rigidbody2D>();
        }

        //spawns the first one here, spawn the rest during the game in the upgrade function. max 4
        AddProjectile();
        weapon = GetComponent<Weapon>();
    }

    public void Initialize(WeaponSO weaponData, Weapon owner)
    {
        maxLevel = weaponData.maxLevel;
        damage = weaponData.damage;
        projectilePrefab = weaponData.projectile;
    }

    public void Fire()
    {
        if (!coroutineOn)
        {
            StartCoroutine(ToggleWeapon());
            coroutineOn = true;
        }
    }

    IEnumerator ToggleWeapon()
    {
        int count = projectiles.Count;
        float angleStep = 360f / count; // Use float 360f

        for (int i = 0; i < count; i++)
        {
            GameObject obj = projectiles[i];
            obj.SetActive(true);

            float initialAngle = i * angleStep;

            
            RotatingProjectileBehaviour projectile = obj.GetComponent<RotatingProjectileBehaviour>();
            if (projectile != null)
            {
                projectile.Setup(playerRb, initialAngle, radius, orbitSpeed);
                projectile.damage = damage;
            }
        }

        yield return new WaitForSeconds(timeEnabled);

        foreach (GameObject projectile in projectiles)
        {
            projectile.SetActive(false);
        }

        coroutineOn = false;
    }

    void AddProjectile()
    {
        GameObject added = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
        projectiles.Add(added);
        added.SetActive(false);
    }

    public void Upgrade()
    {
        if (weapon.level < maxLevel)
        {
            weapon.level++;
            damage *= 1.2f;
            timeEnabled += .5f;
            weapon.cooldown += .5f;

            int result = weapon.level % 2;
            if (result == 0) AddProjectile();
        }
    }
}


