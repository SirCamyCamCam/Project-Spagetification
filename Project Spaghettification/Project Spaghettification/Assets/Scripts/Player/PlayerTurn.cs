using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour {

    // Global Variables
    // Public
    public Transform playerTransform;
    public Rigidbody2D playerRigidBody;
    public bool allowTurning = true;
    public float rotationSpeed = 5.0f;
    public bool isRotating = false;
    public bool fluidSpeed = false;
    public bool speedBasedTurn = false;
    public float fluidSpeedRate = 0.1f;
    public float rotationSpeedStart;
    public float speedBasedDecreaseRateMultiplyer = 0.1f;
    public float minRotAtMaxSpeed = 0.5f;
    public float startDecreaseMultiplyer = 2.0f;
    // Private
    private bool hyperMode = false;
    private float hyperModeTurnRate = 0;
    private float originalRotation;

	// Use this for initialization
	void Start () {
        rotationSpeedStart = rotationSpeed;      // Assign original speed
        originalRotation = rotationSpeed;
        /*if(fluidSpeed == true && speedBasedTurn == true)
        {
            fluidSpeed = false;
        }*/
	}
	
	// Update is called 
	void FixedUpdate () {
		if(allowTurning == true && hyperMode == false)    // Set to false to disable all player turning control
        {
            if (Input.GetButton("Left") || Input.GetAxis("XAxisJoystick") < 0)    // Input
            {
                RotateObject(-rotationSpeed);   // Roatate at speed
                if(fluidSpeed == true)      // If dynamic rotation
                {
                    IncreaseRotationSpeed(fluidSpeedRate);
                }
                if(speedBasedTurn == true)
                {
                    RotationBasedOnSpeed(speedBasedDecreaseRateMultiplyer);
                }
            }
            else if (Input.GetButton("Right") || Input.GetAxis("XAxisJoystick") > 0)       // Input
            {   
                RotateObject(rotationSpeed);    // Roatate at speed
                if(fluidSpeed == true)      // If dynamic rotation
                {
                    IncreaseRotationSpeed(fluidSpeedRate);
                }
                if(speedBasedTurn == true)
                {
                    RotationBasedOnSpeed(speedBasedDecreaseRateMultiplyer);
                }
            }
            else
            {
                isRotating = false;     // No longer turing
                if(fluidSpeed == true)      // Reset rotation speed when no input
                {
                    DecreaseRotationSpeed(fluidSpeedRate);
                }
            }
        }
        else if (allowTurning == true && hyperMode == true)    // Set to false to disable all player turning control
        {
            if (Input.GetButton("Left") || Input.GetAxis("XAxisJoystick") < 0)    // Input
            {
                RotateObject(-hyperModeTurnRate);   // Roatate at speed
            }
            else if (Input.GetButton("Right") || Input.GetAxis("XAxisJoystick") > 0)       // Input
            {
                RotateObject(hyperModeTurnRate);    // Roatate at speed
            }
        }
        else
        {
            isRotating = false;     // Dependent on whether we want things to know it is or isn't turning during locked rotation
            if(fluidSpeed == true)      // Reset rotation speed when no input
            {
                rotationSpeed = rotationSpeedStart;
            }
        }
	}

    // Rotates the object
    public void RotateObject(float speed)
    {
        transform.Rotate(Vector3.back * speed);
        isRotating = true;
    }

    // Increase the rotation speed
    public void IncreaseRotationSpeed(float rate)
    {
        if(rotationSpeed < rotationSpeedStart)
        {
            rotationSpeed += rate;
        }
        else
        {
            rotationSpeed = rotationSpeedStart;
        }
    }

    // Decreases the rotation speed
    public void DecreaseRotationSpeed(float rate)
    {
        if (rotationSpeed > 0)
        {
            rotationSpeed -= rate;
        }
        else
        {
            rotationSpeed = 0;
        }
    }

    // For HyperSpeed On
    public void HyperSpeedModeOn(float newRate)
    {
        hyperModeTurnRate = newRate;
        hyperMode = true;
    }

    // For HyperSpeed Off
    public void HyperSpeedModeOff()
    {
        hyperMode = false;
    }

    // If we are turning with rotation speed based on speed
    private void RotationBasedOnSpeed(float speed)
    {
        if(rotationSpeed >= minRotAtMaxSpeed && rotationSpeed <= originalRotation)
        {
            rotationSpeedStart = (originalRotation * startDecreaseMultiplyer) / Mathf.Pow(1 + speedBasedDecreaseRateMultiplyer, playerRigidBody.velocity.magnitude); // og / mul^velocity
        }
        if(rotationSpeed < minRotAtMaxSpeed)
        {
            rotationSpeed = minRotAtMaxSpeed;
        }
        if(rotationSpeed > originalRotation)
        {
            rotationSpeed = originalRotation;
        }
    }
}
