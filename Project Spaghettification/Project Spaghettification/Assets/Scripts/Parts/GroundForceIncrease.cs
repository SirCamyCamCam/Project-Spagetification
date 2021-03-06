using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundForceIncrease : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public Rigidbody2D targetRigidbody;
    public Transform targetTransform;
    public Transform groundTransform;
    public PlayerThrust playerThrustScript;
    public EnemySoraingThrust enemySoraingThrustScript;
    // Other Script for enemies
    [Header("Variable")]
    public int increaseThrustVal = 3000;
    public int increaseHeight = 50;
    public int minimumSpeed = 1;
    public bool instantThrust = false;
    // Private
    private float distanceFromGround = 0.0f;
    private bool addThrustOnce = false;
    private int currentFrame = 0;
    private int checkAfterFrames = 1;
    private bool usePlayerScript = false;
    private bool useEnemySoaringThrustScript = false;

	// Use this for initialization
	void Start () {
        if(playerThrustScript != null)
        {
            usePlayerScript = true;
        }
        if(enemySoraingThrustScript != null)
        {
            useEnemySoaringThrustScript = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (currentFrame % checkAfterFrames == 0)       // Update every checkAfterFrames frames
        {
            distanceFromGround = groundTransform.position.y - targetTransform.position.y;       // Find distance from the ground
            currentFrame = 0;       // Reset
        }
        currentFrame++;     // Add every frame
        if(distanceFromGround > -increaseHeight && targetRigidbody.velocity.magnitude > minimumSpeed && (targetTransform.rotation.eulerAngles.z > 210 && targetTransform.rotation.eulerAngles.z < 330))     // Checks height, speed, and angles
        {
            if(addThrustOnce == false && usePlayerScript == true)      // If player
            {
                AddPlayerThrust(increaseThrustVal);     // Add Thrust
                addThrustOnce = true;
            }
            else if(addThrustOnce == false && useEnemySoaringThrustScript == true)     // If using enemySoaringScript
            {
                AddEnemySoaringThrust(increaseThrustVal);      // Add thrust
                addThrustOnce = true;
            }
        }
        else if(addThrustOnce == true)
        {
            if (usePlayerScript == true)
            {
                RemovePlayerThrust(increaseThrustVal);
            }
            else if(useEnemySoaringThrustScript == true)
            {
                RemoveEnemySoaringThrust(increaseThrustVal);
            }
            addThrustOnce = false;
        }
	}

    // Removes the player thrust
    private void RemovePlayerThrust(int value)
    {
        playerThrustScript.RemoveFromThrustOutside(value, instantThrust);
    }

    // Adds thrust to player script
    private void AddPlayerThrust(int value)
    {
        playerThrustScript.AddToThrustOutside(value, instantThrust);
    }

    // Adds thrust to enemies
    private void AddEnemySoaringThrust(int value)
    {
        enemySoraingThrustScript.AddToThrustOutside(value, true);
    }

    // Removes thrust to enemies
    private void RemoveEnemySoaringThrust(int value)
    {
        enemySoraingThrustScript.RemoveFromThrustOutside(value, true);
    }
}
