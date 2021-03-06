using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoraingThrust : MonoBehaviour {

    // Global Variables
    // Public 
    [Header("Objects")]
    public Transform targetTransform;
    public Transform objectTransform;
    public Rigidbody2D objectRigidbody;
    public ShieldController shieldControllerScript;
    public PlayerFlame playerFlameScript;
    [Header("Variables")]
    public int minDistance = 75;
    public int angleDistance = 10;
    public float thrust = 6000;
    public int addToThrustVal = 3000;
    public int increaseRate = 300;
    public int decreaseRate = 200;
    public int addToThrustBellowVelocity = 150;
    public int leftNoThrustAngle = 135;
    public int rightNoThrustAngle = 45;
    public int updateFrame = 4;
    [HideInInspector]
    public bool isThrusting = false;
    [HideInInspector]
    public bool forceThrust = false;
    [HideInInspector]
    public bool allowThrust = true;
    [HideInInspector]
    public float targetDistance = 0.0f;
    // Private
    private float angleTarget = 0.0f;
    private int currentFrame = 0;
    private bool addedThrustOnce = false;
    private float thrustAddQueueBelowVelocity = 0;
    private float thrustRemoveQueueBelowVelocity = 0;
    private float thrustAddQueueOutside = 0;
    private float thrustRemoveQueueOutside = 0;
    private float originalThrust = 0;
    private bool usesShield = false;

    // Use this for initialization
    private void Start()
    {
        originalThrust = thrust;
        if(shieldControllerScript != null)
        {
            usesShield = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (usesShield == false)
        {
            if (allowThrust == true)
            {
                FindTargetAngle();
                DecideToThrust();               
            }
            else
            {
                DisableThrust();
            }
        }
        else
        {
            if(allowThrust == true && shieldControllerScript.shieldStatus == false)
            {
                FindTargetAngle();
                DecideToThrust();
            }
            else
            {
                DisableThrust();
            }
        }

        AddOrRemoveThrust();
    }

    // Fixed update for physics
    private void FixedUpdate()
    {
        if (isThrusting == true)
        {
            objectRigidbody.AddForce(transform.right * -thrust);    // Add Thrust
            playerFlameScript.EnableFlame();
        }
        else
        {
            playerFlameScript.DisableFlame();
        }
    }

    // Adds to thrust
    private void AddToThrustForBelowVelocity()
    {
        thrust += increaseRate;
        thrustAddQueueBelowVelocity -= increaseRate;
        if (thrustAddQueueBelowVelocity < 0)
        {
            thrust += thrustAddQueueBelowVelocity;
            thrustAddQueueBelowVelocity = 0;
        }
    }

    // Chnages the thrust value
    private void AddOrRemoveThrust()
    {
        // Adds and removes thrust on as need basis
        if (thrustAddQueueBelowVelocity > 0)
        {
            AddToThrustForBelowVelocity();
        }
        if (thrustRemoveQueueBelowVelocity > 0)
        {
            RemoveThrustForBelowVelocity();
        }
        if (thrustAddQueueOutside > 0)
        {
            AddToThrustForOutside();
        }
        if (thrustAddQueueOutside > 0)
        {
            RemoveThrustForOutside();
        }
    }

    // Enables thrusting
    private void EnableThrust()
    {
        if (addedThrustOnce == false)
        {
            addedThrustOnce = true;
            AddToThrustForBelowVelocity();                          // Add to thrust
        }
        else
        {
            addedThrustOnce = false;
            RemoveThrustForBelowVelocity();                       // Remove from thrust
        }
    }

    // Disables the thrust
    private void DisableThrust()
    {
        isThrusting = false;                            // No longer thrusting  
        if (addedThrustOnce == true)                     // If thrust hasn't been reset
        {
            addedThrustOnce = false;                    // Reset
            RemoveThrustForBelowVelocity();               // Remove from thrust
        }
    }

    // Enables or disables the thrust
    private void DecideToThrust()
    {
        // Enables and disables thrust
        if ((targetDistance > minDistance && !(angleTarget < leftNoThrustAngle && angleTarget > rightNoThrustAngle && targetDistance < angleDistance)) || forceThrust == true)       // If between range
        {
            if (addedThrustOnce == false && objectRigidbody.velocity.magnitude < addToThrustBellowVelocity)              // If bellow add thrust velocity and haven't added yet
            {
                EnableThrust();
            }
            else if (addedThrustOnce == true && objectRigidbody.velocity.magnitude >= addToThrustBellowVelocity)         // If above add thrust velocity and thrust hasn't been removed yet
            {
                EnableThrust();
            }
            isThrusting = true;                                 // We are thrusting
        }
        else
        {
            DisableThrust();
        }
    }

    // Finds the target angle
    private void FindTargetAngle()
    {
        // Find target angle
        if (currentFrame % updateFrame == 0)
        {
            targetDistance = Vector3.Distance(targetTransform.position, objectTransform.position);                        // Find position
            Vector3 vectorToTarget = targetTransform.position - transform.position;
            angleTarget = Mathf.Atan2(-vectorToTarget.y, -vectorToTarget.x) * Mathf.Rad2Deg;                              // Find angle
            currentFrame = 0;                                                                                             // Reset current frame
        }
        currentFrame++;                                                                                                   // Add to frame
    }

    // Removes Thrust
    private void RemoveThrustForBelowVelocity()
    {
        thrust -= decreaseRate;
        thrustRemoveQueueBelowVelocity -= decreaseRate;
        if (thrustRemoveQueueBelowVelocity < 0)
        {
            thrust += -thrustRemoveQueueBelowVelocity;
            thrustRemoveQueueBelowVelocity = 0;
        }
    }

    // Adds to thrust for outside
    private void AddToThrustForOutside()
    {
        thrust += increaseRate;
        thrustAddQueueOutside -= increaseRate;
        if (thrustAddQueueOutside < 0)
        {
            thrust += thrustAddQueueOutside;
            thrustAddQueueOutside = 0;
        }
    }

    // Removes from thrust for outside
    private void RemoveThrustForOutside()
    {
        thrust -= decreaseRate;
        thrustRemoveQueueOutside -= decreaseRate;
        if (thrustRemoveQueueOutside < 0)
        {
            thrust += -thrustRemoveQueueOutside;
            thrustRemoveQueueOutside = 0;
        }
    }

    // Add thrust for below velocity
    private void AddToThrustQueueBelowVelocity(int addVal)
    {
        thrustAddQueueBelowVelocity = (originalThrust + addVal) - thrust;
        thrustRemoveQueueBelowVelocity = 0;
    }

    // Remove thrust for below velocity
    private void RemoveThrustQueueBelowVelcocity(int removeVal)
    {
        thrustRemoveQueueBelowVelocity = -originalThrust + thrust;
        if (thrustRemoveQueueBelowVelocity > removeVal)
        {
            thrustRemoveQueueBelowVelocity = removeVal;
        }
        thrustAddQueueBelowVelocity = 0;
    }

    // Resets Thrust
    public void ResetThrust()
    {
        thrust = originalThrust;
    }

    // Adds thrust for outside script
    public void AddToThrustOutside(int addVal, bool instant)
    {
        if (instant == true)
        {
            thrust += addVal;
            return;
        }
        thrustAddQueueOutside += addVal;
    }

    // Removes thrust for outside script
    public void RemoveFromThrustOutside(int removeVal, bool insant)
    {
        if (insant == true)
        {
            thrust -= removeVal;
            return;
        }
        thrustRemoveQueueOutside += removeVal;
    }
}
