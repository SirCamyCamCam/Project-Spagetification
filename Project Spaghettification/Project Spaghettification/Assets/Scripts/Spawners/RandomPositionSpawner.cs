using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionSpawner : MonoBehaviour {

    public Transform[] targetPrefab;
    public Transform[] avoidTargets;
    public Transform objectTransform;
    public Transform leftWall;
    public Transform rightWall;
    public Transform bottomWall;
    public Transform topWall;
    public int lengthFromSides = 0;
    public int avoidDistance = 0;

	// Use this for initialization
	void Start ()
    {
        //Declare variables
        int numTargets = targetPrefab.Length, numAvoidanceTargets = avoidTargets.Length, i, k;
        float[] avoidanceDistance = new float[numAvoidanceTargets];

        for (i = 0; i < numTargets; i++)    // For every target
        {
            float randomX = GetRandomX();   // Get Random x val
            float randomY = GetRandomY();   // Get random Y val
            objectTransform.position = new Vector3(randomX, randomY);     // Move to position

            for (k = 0; k < numAvoidanceTargets; )   // Check distance for each object
            {
                avoidanceDistance[k] = Vector2.Distance(objectTransform.position, avoidTargets[k].position);      // Find distance
                if (avoidanceDistance[k] <= avoidDistance)      // If less than distance that it should avoid assign new position
                {
                    randomX = GetRandomX();
                    randomY = GetRandomY();
                    objectTransform.position = new Vector3(randomX, randomY);
                    k = 0;      // Reset for every avoidance object
                }
                else
                {
                    k++;    // If safe check next object
                }
            }
            // Once safe location
            Instantiate(targetPrefab[i], objectTransform.position, objectTransform.rotation);
        }
        // After spawning objects
        Destroy(gameObject);
    }

    // Gets random x val based on walls
    private float GetRandomX()  
    {
        return Random.Range((leftWall.position.x + lengthFromSides), (rightWall.position.x - lengthFromSides));
    }

    // Gets random Y val based on walls
    private float GetRandomY()
    {
        return Random.Range((bottomWall.position.y + lengthFromSides), (topWall.position.y - lengthFromSides));
    }
}
