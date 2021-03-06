using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSpawner : MonoBehaviour {

    // Global Variriables
    // Public
    [Header("Objects")]
    public Transform swarmerTransform;
    public SwarmSpawner swarmSpawnerScript;
    public Transform[] avoidObjects;
    public Transform targetTransform;
    public Transform objectTransform;
    public Transform groundTransform;
    public Transform topWallTransform;
    public Transform leftWallTransform;
    public Transform rightWallTransform;
    public GlobalSpawer globalSpawerScript;
    public Transform leadObjectTransform;
    [Header("Variables")]
    public int spawnCount = 6;
    public float timeToSpawn = 10.0f;
    public float distanceFromTarget = 6.0f;
    public float spawnRadius = 3.0f;
    public float avoidDistance = 2.0f;
    public float wallDistance = 4.0f;
    public bool globalSpawner = true;
    public bool randomSpawnCount = false;
    public int randomCountMin = 1;
    public int randomCountMax = 10;
    [Header("Scripts to Assign")]
    public bool usesAimAtTarget = false;
    public bool usesAimWithRandomness = false;
    public bool usesEnemySoaring = false;
    public bool usesFireRateOnDistance = false;
    public bool usesMoveRelativeToPlayer = false;
    public bool usesTargetWithinRange = false;
    public bool usesFlipSpriteWithPlayerPos = false;
    public bool usesAvoidGround = false;
    public bool usesGroundForceIncrease = false;
    // Private
    private int currentCount = 0;
    private float randomAngle = 0.0f;
    private int numAvoidanceObjects = 0;
    private int i = 0;
    private int newLocationAvoidanceCount = 0;
    private int newLocationXWallCount = 0;
    private int newLocationYWallCount = 0;
    private bool canSpawn = false;

	// Use this for initialization
	void Start () {
        if (globalSpawner == false)
        {
            SpawnSwarm();
        }
	}

    // Spawn the new swarm
    public void SpawnSwarm()
    {
        numAvoidanceObjects = avoidObjects.Length;              // Get the length of the objects we should avoid
        randomAngle = Random.value * 360;                       // Get random angle from player
        float randomX = randomCenterX();                        // Get a new x position
        float randomY = randomCenterY();                        // Get a new y position
        objectTransform.position = new Vector3(randomX, randomY);     // Move to new random location along radius
        canSpawn = CheckDistanceOfAvoidObjects(false);
        if (canSpawn == true)
        {
            if(randomSpawnCount == true)
            {
                spawnCount = RandomSpawnCount();
            }
            while (currentCount < spawnCount)                                        // If we haven't spawned all objects yet
            {
                float randomSpawnX = randomXSpawnPoint();                           // Find new spawn X
                float randomSpawnY = randomYSpawnPoint();                           // Find new spawn Y
                Transform newSpawned = Instantiate(swarmerTransform, new Vector3(randomSpawnX, randomSpawnY), objectTransform.rotation);           // Spawn object
                if (usesAimAtTarget == true)
                {
                    newSpawned.GetComponentInChildren<AimAtTarget>().targetTranform = targetTransform;
                }
                if(usesAimWithRandomness == true)
                {
                    newSpawned.GetComponent<AimWithRandomness>().targetTransform = targetTransform;
                }
                if(usesEnemySoaring == true)
                {
                    newSpawned.GetComponent<EnemySoraingThrust>().targetTransform = targetTransform;
                }
                if(usesTargetWithinRange == true)
                {
                    newSpawned.GetComponentInChildren<TargetWithinRange>().target = targetTransform;
                }
                if(usesFireRateOnDistance == true)
                {
                    newSpawned.GetComponent<FireRateBasedOnDistance>().targetTransform = targetTransform;
                }
                if(usesMoveRelativeToPlayer == true)
                {
                    newSpawned.GetComponent<MoveRelativeToPlayer>().targetTransform = targetTransform;
                }
                if(usesFlipSpriteWithPlayerPos == true)
                {
                    newSpawned.GetComponent<FlipSpriteBasedOnPlayerPos>().targetTransform = targetTransform;
                }
                if(usesAvoidGround == true)
                {
                    newSpawned.GetComponent<AvoidGround>().groundTransform = groundTransform;
                }
                if(usesGroundForceIncrease == true)
                {
                    newSpawned.GetComponent<GroundForceIncrease>().groundTransform = groundTransform;
                }
                if(globalSpawerScript != null && globalSpawner == true)
                {
                    newSpawned.GetComponent<Health>().globalSpawerScript = globalSpawerScript;
                }
                if(leadObjectTransform != null)
                {
                    Transform newLead = Instantiate(leadObjectTransform, new Vector3(randomSpawnX, randomSpawnY), objectTransform.rotation);
                    newLead.GetComponent<LeadInfrontTarget>().targetTransform = newSpawned.transform;
                    newLead.GetComponent<LeadInfrontTarget>().targetRigidbody = newSpawned.GetComponent<Rigidbody2D>();
                    newSpawned.GetComponent<BulletSettings>().leadTargetTransform = newLead;
                }
                currentCount++;                                                     // Increase object count
            }
        }
        currentCount = 0;                                                       // Reset spawn count
        if (globalSpawner == false)
        {
            StartCoroutine(WaitToSpawn());
        }
    }

    // Gets a random spawn count
    public int RandomSpawnCount()
    {
        return Random.Range(randomCountMin, randomCountMax);
    }

    // Check avoid Distances
    private bool CheckDistanceOfAvoidObjects(bool status)
    {
        if(((objectTransform.position.y < (groundTransform.position.y + wallDistance)) || (objectTransform.position.y > (topWallTransform.position.y - wallDistance))) && newLocationYWallCount == 0)               // If bellow or above walls
        {
            newLocationYWallCount += 1;
            float randomY = InverseCenterY();                                                                            // Set the number of times weve had to reset the location
            objectTransform.position = new Vector2(objectTransform.position.x, randomY);                                 // Move to opposite y
            CheckDistanceOfAvoidObjects(status);                                                                         // Do again until safe distance
        }
        if(((objectTransform.position.x < (leftWallTransform.position.x + wallDistance)) || (objectTransform.position.x > (rightWallTransform.position.x + wallDistance))) && newLocationYWallCount < 2)           // If left or right of walls
        {
            newLocationYWallCount += 1;                                                                                  // Increase how many times we've done this
            objectTransform.position = new Vector2(InverseCenterX(), objectTransform.position.y);                        // Set new opposite x
            CheckDistanceOfAvoidObjects(status);                                                                         // Call function to try again
        }

        if(newLocationYWallCount == 2 && newLocationXWallCount == 1)                                                     // If no valid x or y position
        {
            Debug.Log("Not within bounds!");
            return false;
        }

        newLocationXWallCount = 0;                                                                                       // Reset X wall safety
        newLocationYWallCount = 0;                                                                                       // Reset Y wall saftey

        if (newLocationAvoidanceCount < 8)                                                                              // If we have less than max differnent angles
        {
            for (i = 0; i < numAvoidanceObjects; i++)                                                                    // For every object to avoid
            {
                if (Vector3.Distance(objectTransform.position, avoidObjects[i].position) < avoidDistance)                // If the object is less than that distance
                {
                    i = 0;                                                                                               // Reset i to check all object again
                    newLocationAvoidanceCount += 1;                                                                      // Increase the number of places we've checked
                    if (randomAngle + 22.5f > 360)
                    {
                        randomAngle = (randomAngle - 360) + 22.5f;
                    }
                    else
                    {
                        randomAngle += 10;
                    }
                    objectTransform.position = new Vector3(randomCenterX(), randomCenterY());                            // Move to a new random location
                    CheckDistanceOfAvoidObjects(status);
                }
            }
            return true;                                                                                                 // Passed all checks!
        }
        else
        {
            Debug.Log("Failed to find safe area from avoid objects");                                                    // Can't spawn here
            return false;
        }
    }

    // Center X position
    private float randomCenterX()
    {
        return targetTransform.position.x + distanceFromTarget * Mathf.Sin((randomAngle) * Mathf.Deg2Rad);
    }

    // Center Y position
    private float randomCenterY()
    {
        return targetTransform.position.y + distanceFromTarget * Mathf.Cos((randomAngle) * Mathf.Deg2Rad);
    }

    // Gets the inverse of the current random X angle
    private float InverseCenterX()
    {
        return targetTransform.position.x + distanceFromTarget * -Mathf.Sin(randomAngle * Mathf.Deg2Rad);
    }

    // Gets the inverse of the current random Y angle
    private float InverseCenterY()
    {
        return targetTransform.position.y + distanceFromTarget * -Mathf.Cos(randomAngle * Mathf.Deg2Rad);
    }

    // Spawn X Position
    private float randomXSpawnPoint()
    {
        return objectTransform.position.x + (spawnRadius * Random.Range(-1.0f, 1.0f));
    }

    // Spawn Y position
    private float randomYSpawnPoint()
    {
        return objectTransform.position.y + (spawnRadius * Random.Range(-1.0f, 1.0f));
    }

    // Waits to spawn more
    private IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(timeToSpawn);
        SpawnSwarm();
    }
}
