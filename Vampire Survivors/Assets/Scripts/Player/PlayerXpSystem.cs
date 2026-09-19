using System.Collections.Generic;
using UnityEngine;

public class PlayerXpSystem : MonoBehaviour
{
    public float lvlOneNeededXp;
    public float neededXpMultiplier;
    float nextLevelXp;

    public float xp;
    float totalXp;

    public List<GameObject> xpOrbPool = new List<GameObject>();
    float poolLimit = 100;
    public GameObject xpPrefab;

    private void Start()
    {
        nextLevelXp = lvlOneNeededXp;
        xp = 0;
        SpawnPool();
    }

    void SpawnPool()
    {
        while (xpOrbPool.Count < poolLimit)
        {
            GameObject spawnedOrb = Instantiate(xpPrefab);
            xpOrbPool.Add(spawnedOrb);
            spawnedOrb.SetActive(false);
        }
    }

    public void GetXp(float xpAmount)
    {
        xp += xpAmount;
        totalXp += xpAmount; //possible fun stat for death screen

        if (xp >= nextLevelXp)
        {
            xp = 0;
            nextLevelXp *= neededXpMultiplier;
            LevelUp();
        }
    }

    void LevelUp()
    {
        Debug.Log("Level Up. New goal is " + nextLevelXp + " Xp" );
    }
}
