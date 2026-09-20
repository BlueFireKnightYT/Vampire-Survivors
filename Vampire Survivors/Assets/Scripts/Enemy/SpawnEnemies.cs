using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    float cooldown = 0.5f;
    float remainingTime;
    PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
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

            GameObject chosenEnemy = pooledObject();

            if(chosenEnemy != null)
            {
                chosenEnemy.transform.position = spawnPos;
                chosenEnemy.SetActive(true);
            }
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
