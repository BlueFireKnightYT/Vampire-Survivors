using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public static SpawnEnemies instance { get; private set; }
    PoolManager poolManager;

    public List<WaveSO> waves = new List<WaveSO>();

    public int waveCount = 0;
    bool randomizedPos = false;

    Vector2 randomDirection1;
    float randomDistance1;

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
        while (true)
        {
            waveCount++;
            foreach (WaveSO wave in waves)
            {
                foreach (WaveEnemyData enemies in wave.enemies)
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
                            chosenEnemy.GetComponent<Walker>().enemyType = wave.waveType;

                            Enemy enemyScript = chosenEnemy.GetComponent<Enemy>();

                            enemyScript.enemySO = enemies.enemySO;

                            enemyScript.retrieveSO();
                            print("Spawn");

                            if(wave.waveType == 1)
                            {
                                if(!randomizedPos)
                                {
                                    randomDirection1 = Random.insideUnitCircle.normalized;
                                    randomDistance1 = Random.Range(15, 30);
                                    randomizedPos = true;
                                }
                                spawnPos = transform.position + (Vector3)(randomDirection1 * randomDistance1);
                                chosenEnemy.transform.position = spawnPos * Random.Range(0, 0.3f);
                            }
                        }
                        yield return new WaitForSeconds(wave.spawnDelay);
                    }
                }
                randomizedPos = false;
                yield return new WaitForSeconds(wave.waveDelay);
            }
        }

    }
}
