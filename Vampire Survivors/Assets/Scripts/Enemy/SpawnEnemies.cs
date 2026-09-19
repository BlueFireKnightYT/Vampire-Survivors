using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    List<GameObject> enemyPool = new List<GameObject>();
    public GameObject enemy;
    public int poolLimit;

    float cooldown = 0.5f;
    float remainingTime;
    private void Awake()
    {
        SpawnPool();
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;

        //enable 3 enemies per second if there are any disabled enemies left in pool
        if(remainingTime <= 0)
        {
            remainingTime = cooldown;

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(15, 30);

            Vector3 spawnPos = transform.position + (Vector3) (randomDirection * randomDistance);

            GameObject chosenEnemy = pooledEnemy();

            if(chosenEnemy != null)
            {
                chosenEnemy.transform.position = spawnPos;
                chosenEnemy.SetActive(true);
            }
        }

    }

    GameObject pooledEnemy()
    {
        GameObject foundEnemy = null;
        foreach (GameObject enemy in enemyPool)
        {
            if(!enemy.activeSelf)
            {
                foundEnemy = enemy;
                break;
            }
        }
        return foundEnemy;
    }

    void SpawnPool()
    {
        while(enemyPool.Count < poolLimit)
        {
            GameObject spawnedEnemy = Instantiate(enemy);
            enemyPool.Add(spawnedEnemy);
            spawnedEnemy.SetActive(false);
        }
    }
}
