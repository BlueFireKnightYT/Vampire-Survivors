using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // for overvieuw in inspector
    public string PoolName;

    [Header("Variables")]
    public int poolLimit;
    public GameObject poolObject;

    [Header("List")]
    public List<GameObject> objectPool = new List<GameObject>();

    private void Awake()
    {
        SpawnPool();
    }
    void SpawnPool()
    {
        while (objectPool.Count < poolLimit)
        {
            GameObject spawnedOrb = Instantiate(poolObject);
            objectPool.Add(spawnedOrb);
            spawnedOrb.SetActive(false);
        }
    }

    // --- copy-pastable pool object retriever ---

    //GameObject pooledObject()
    //{
    //    GameObject foundObject = null;
    //    foreach (GameObject listObject in PoolManager.objectPool)
    //    {
    //        if (!listObject.activeSelf)
    //        {
    //            foundObject = listObject;
    //            break;
    //        }
    //    }
    //    return foundObject;
    //}

}
