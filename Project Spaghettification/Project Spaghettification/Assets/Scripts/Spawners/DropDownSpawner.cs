using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownSpawner : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public Transform targetPrefab;
    public Transform spawnPoint;
    [Header("Variables")]
    public int maxWaitTime = 5;
    public int minWaitTime = 1;
    public int spawnCount = 0;
    public int maxSpawned = 4;
    public int maxX = 730;
    public int minX = -730;
    public int maxY = 500;
    public int minY = 100;
    [Header("Type")]
    public bool mineSpawner = false;
    // Private

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitToSpawn());
    }

    //Wait to spawn
    private IEnumerator WaitToSpawn()
    {
        while (true)        // Loop forever
        {
            yield return new WaitForSeconds(RandomWaitVal());   // Wait
            Spawn();        // Call spawn
        }
    }

    //Random wait time to spawn
    private float RandomWaitVal()
    {
        return Random.Range(minWaitTime, maxWaitTime);
    }

    //Spawn the mines
    private void Spawn()
    {
        float spawnPointX = RandomSpawnXVal();      // Gets random X val
        float spawnPointY = RandomSpawnYVal();      // Gets random Y val
        SetSpawnPoint(spawnPointX);
        if (spawnCount < maxSpawned)
        {
            Transform spawnedObject = Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
            if (mineSpawner == true)
            {
                spawnedObject.GetComponent<MineController>().GetYSpawnPoint(spawnPointY);
            }
            spawnCount += 1;
        }
    }

    //Random X spawn 
    private float RandomSpawnXVal()
    {
        return Random.Range(minX, maxX);
    }

    //Random Y spawn
    private float RandomSpawnYVal()
    {
        return Random.Range(minY, maxY);
    }

    //Set Spawn X Point
    private void SetSpawnPoint(float x)
    {
        spawnPoint.transform.position = new Vector3(x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
    }
}
