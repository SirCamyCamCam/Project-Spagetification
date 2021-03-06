using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour {

    // Public
    [Header("Objects")]
    public Rigidbody2D objectRigidbody;
    public PlayerThrust playerThrustScript;
    public EnemySoraingThrust enemySoraingThrustScript;
    public PlayerTurn playerTurnScript;
    [Header("Variables")]
    public bool allowGravity = true;
    public float gravity = 0f;
    public float maximumGravity = 30.0f;
    public float minimumGravity = 0.0f;
    public float increaseRateHigh = 0.0001f;
    public float increaseRateMedium = 0.001f;
    public float increaseRateLow = 0.01f;
    public float decreaseRate = 15.0f;
    public float highSpeed = 10;
    public float mediumSpeed = 6;
    public float lowSpeed = 3;
    // Private
    private bool waitForFirstInput = true;
    private float increaseRate = 0.0f;
    private bool usePlayerThrustScript = false;
    private bool usePlayerturn = false;

    private void Start()
    {
        if(playerThrustScript != null)
        {
            usePlayerThrustScript = true;
        }
        if(playerThrustScript != null)
        {
            usePlayerThrustScript = true;
        }
        if(playerTurnScript != null)
        {
            usePlayerturn = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (waitForFirstInput == true && (Input.GetButton("Thrust") || Input.GetButton("ThrustJoystick")))   // Wait for input
        {
            waitForFirstInput = false;
        }

        if (allowGravity == true)       // If gravity is allowed right now
        {
            if (usePlayerThrustScript == true)
            {
                if (playerThrustScript.isThrusting == true && waitForFirstInput == false)
                {
                    RemoveGravity(decreaseRate);
                }
                else if (playerThrustScript.isThrusting == false && waitForFirstInput == false)
                {
                    AddGravity(increaseRate);
                }
            }
            if(usePlayerThrustScript == false)
            {
                if(enemySoraingThrustScript.isThrusting == true)
                {
                    RemoveGravity(decreaseRate);
                }
                else if(enemySoraingThrustScript.isThrusting == false)
                {
                    AddGravity(increaseRate);
                }
            }
            objectRigidbody.gravityScale = gravity;
        }
        if (usePlayerturn == true && allowGravity == true)
        {
            if (objectRigidbody.velocity.magnitude <= lowSpeed || playerTurnScript.isRotating == true)
            {
                increaseRate = increaseRateLow;
            }
            else if (objectRigidbody.velocity.magnitude <= mediumSpeed)
            {
                increaseRate = increaseRateMedium;
            }
            else if (objectRigidbody.velocity.magnitude >= highSpeed)
            {
                increaseRate = increaseRateHigh;
            }
        }
        else if(usePlayerturn == false && allowGravity == true)
        {
            if (objectRigidbody.velocity.magnitude <= lowSpeed)
            {
                increaseRate = increaseRateLow;
            }
            else if (objectRigidbody.velocity.magnitude <= mediumSpeed)
            {
                increaseRate = increaseRateMedium;
            }
            else if (objectRigidbody.velocity.magnitude >= highSpeed)
            {
                increaseRate = increaseRateHigh;
            }
        }
	}

    // Adds Gravity to object
    public void AddGravity(float rate)
    {
        if (objectRigidbody.gravityScale < maximumGravity)      // if less than maximum, increase gravity by rate
        {
            gravity += rate;
        }
        else if(objectRigidbody.gravityScale > maximumGravity)
        {
            gravity = maximumGravity;      // Set gravity to max 
        }
    }

    // Remove from gravity
    public void RemoveGravity(float rate)
    {
        if(objectRigidbody.gravityScale > minimumGravity)       // Remove till minimum
        {
            gravity -= rate;
        }
        else if(objectRigidbody.gravityScale < minimumGravity)      // Set to minimum
        {
            gravity = minimumGravity;
        }
    }

    // Sets gravtiy to min
    public void SetMinimumGravity()
    {
        objectRigidbody.gravityScale = minimumGravity;
    }

    // Set gravity to max
    public void SetMaximumGravity()
    {
        objectRigidbody.gravityScale = maximumGravity;
    }
}
