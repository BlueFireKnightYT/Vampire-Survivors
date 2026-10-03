using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public static SpawnEnemies instance { get; private set; }
    PoolManager poolManager;

    public List<WaveSO> waves = new List<WaveSO>();

    public int waveCount = 0;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
        StartCoroutine(SpawnWave());

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    GameObject PooledObject()
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

    IEnumerator SpawnWave()
    {
        foreach(WaveSO wave in waves)
        {
            waveCount++;
            foreach(WaveEnemyData enemies in wave.enemies)
            {
                for (int i = 0; i < enemies.amount;)
                {
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    float randomDistance = Random.Range(15, 30);

                    Vector3 spawnPos = transform.position + (Vector3)(randomDirection * randomDistance);

                    GameObject chosenEnemy = PooledObject();

                    if (chosenEnemy != null)
                    {
                        i++;

                        chosenEnemy.transform.position = spawnPos;
                        chosenEnemy.SetActive(true);

                        Enemy enemyScript = chosenEnemy.GetComponent<Enemy>();

                        enemyScript.enemySO = enemies.enemySO;

                        enemyScript.retrieveSO();
                    }
                    yield return new WaitForSeconds(wave.spawnDelay);
                }
            }
            yield return new WaitForSeconds(wave.waveDelay);
        }

    }
}
