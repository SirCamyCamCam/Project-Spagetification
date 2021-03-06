using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrust : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public Rigidbody2D playerRigidbody;
    public PlayerFlame playerFlameScript;
    public ShieldController shieldScript;
    public PlayerUncontrollableSpin playerUncontrollableSpinScript;
    [Header("Variables")]
    public float thrust = 6000;
    public int addThrustValue = 3000;
    public int increaseRate = 300;
    public int decreaseRate = 200;
    public int addThrustBellowVelocity = 150;
    public bool allowThrust = true;
    public bool isThrusting = false;
    // Private
    private float originalThrust = 0;
    private float thrustAddQueueBelowVelocity = 0;
    private float thrustRemoveQueueBelowVelocity = 0;
    private float thrustAddQueueOutside = 0;
    private float thrustRemoveQueueOutside = 0;
    private bool addedToThrustOnce = false;
    private bool forceThrust = false;

    // Use this to intialize
    private void Start()
    {
        originalThrust = thrust;
    }

    // Update is called once per frame
    void Update ()
    {
        if (((Input.GetButton("Thrust") || Input.GetButton("ThrustJoystick")) && allowThrust == true && shieldScript.shieldStatus == false) || forceThrust == true)             // Checks for input
        {
            if (playerRigidbody.velocity.magnitude <= addThrustBellowVelocity && addedToThrustOnce == false)        // If less than low velocity thrust
            {
                AddToThrustQueueBelowVelocity(addThrustValue);            // Add to thrust
                addedToThrustOnce = true;
            }
            else if (playerRigidbody.velocity.magnitude > addThrustBellowVelocity && addedToThrustOnce == true)     // If greater than low veloity thrust
            {
                RemoveThrustQueueBelowVelcocity(addThrustValue);           // Remove from thrust
                addedToThrustOnce = false;
            }
            playerFlameScript.EnableFlame();     // Turn on flame
            isThrusting = true;
        }
        else if ((!Input.GetButton("Thrust") && !Input.GetButton("ThrustJoystick")) || shieldScript.shieldStatus == true || allowThrust == false)      // If not thrusting or shield on
        {
            playerFlameScript.DisableFlame();       // Turn flame off
            isThrusting = false;
            if(addedToThrustOnce == true)               // If we are still above normal thrust
            {
                addedToThrustOnce = false;              // Reset
                RemoveThrustQueueBelowVelcocity(addThrustValue);           // Remove thrust
            }
        }

        if(thrustAddQueueBelowVelocity > 0)
        {
            AddToThrustForBelowVelocity();
        }
        if(thrustRemoveQueueBelowVelocity > 0)
        {
            RemoveThrustForBelowVelocity();
        }
        if(thrustAddQueueOutside > 0)
        {
            AddToThrustForOutside();
        }
        if(thrustAddQueueOutside > 0)
        {
            RemoveThrustForOutside();
        }
        
    }

    // Fixed update for physics
    private void FixedUpdate()
    {
        if(isThrusting == true)
        {
            playerRigidbody.AddForce(transform.right * -thrust);    // Add Thrust
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
        if(thrustAddQueueOutside < 0)
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
        if(thrustRemoveQueueOutside < 0)
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
        if(thrustRemoveQueueBelowVelocity > removeVal)
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
    public void AddToThrustOutside(int addVal, bool  instant)
    {
        if(instant == true)
        {
            thrust += addVal;
            return;
        }
        thrustAddQueueOutside += addVal;
    }

    // Removes thrust for outside script
    public void RemoveFromThrustOutside(int removeVal, bool  insant)
    {
        if(insant == true)
        {
            thrust -= removeVal;
            return;
        }
        thrustRemoveQueueOutside += removeVal;
    }

    // Forces the player to be thrusting
    public void ForceThrust(bool status)
    {
        forceThrust = status;
    }
}
