using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpawer : MonoBehaviour {

    // Global Variables
    // Public
    public SwarmSpawner[] swarmSpawnerScripts;
    public Transform targetTransform;
    public Transform healthPrefab;
    public Transform objectTransform;
    public int[] spawnChances;             // Has to be out of 100 and in order 
    public float waitTime = 15f;
    public float enemyWaitTime = 60f;
    [SerializeField]
    public string recentSpawn = "";
    // Private
    private int numSpawners;
    private int beeCount = 0;
    private int currentBees = 0;
    private int dragonflyCount = 0;
    private int currentDragonflies = 0;
    private int currentViper = 0;
    private int viperCount = 0;

	// Use this for initialization
	void Start () {
        numSpawners = swarmSpawnerScripts.Length;
        SpawnAnEnemy();
	}

    // Spawns an enemy based off random chance
    public void SpawnAnEnemy()
    {
        int random = Random.Range(1,100), i = 0;
        for(i = 0; i < numSpawners; i++)
        {
            if(random < spawnChances[i])
            {
                swarmSpawnerScripts[i].SpawnSwarm();
                if (i == 0)
                {
                    dragonflyCount = swarmSpawnerScripts[i].spawnCount;
                    currentDragonflies += dragonflyCount;
                    recentSpawn = "Dragonfly";
                }
                else if( i == 1) 
                {
                    beeCount = swarmSpawnerScripts[i].spawnCount;
                    currentBees += beeCount;
                    recentSpawn = "Bee";
                }
                else if(i == 2)
                {
                    viperCount = swarmSpawnerScripts[i].spawnCount;
                    currentViper += viperCount;
                    recentSpawn = "Viper";
                }
                return;
            }
        }
    }

    // Enemy died
    public void EnemyDied(Transform transformDeath)
    {
        currentViper--;
        if(currentViper % viperCount == 0)
        {
            SpawnHealth(transformDeath);
            SpawnAnEnemy();
        }
    }

    // Bee death
    public void BeeDeath(Transform transformDeath)
    {
        currentBees--;
        if(currentBees % beeCount == 0)
        {
            SpawnHealth(transformDeath);
            SpawnAnEnemy();
        }
    }

    // Dragonfly death
    public void DragonflyDeath(Transform transformDeath)
    {
        currentDragonflies--;
        if(currentDragonflies % dragonflyCount == 0)
        {
            SpawnHealth(transformDeath);
            SpawnAnEnemy();
        }
    }

    // Spawns a health gameobject
    public void SpawnHealth(Transform curtransform)
    {
        objectTransform.position = new Vector3(curtransform.position.x, curtransform.position.y);
        Transform newSpawn = Instantiate(healthPrefab, objectTransform.position, objectTransform.rotation);
        newSpawn.GetComponentInChildren<TargetWithinRange>().target = targetTransform;
        newSpawn.GetComponent<Magnetic>().targetTransform = targetTransform;
    }
}
