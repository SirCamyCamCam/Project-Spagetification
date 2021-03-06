using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHyperMode : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public PlayerThrust playerThrustScript;
    public CameraShake cameraShakeScript;
    public PlayerTurn playerTurnScript;
    [Header("Variables")]
    public int increaseThrustRate = 100;
    public int addedTrust = 5000;
    public bool hyperMode = false;
    public float maxShake = 0.007f;
    public float increaseShakeRate = 0.000001f;
    public float decreaseTurnRate = 0.01f;
    public float maxTurnRate = 4f;
    // Private
    private float currentShake = 0;
    public bool switchThrust = false;
    public bool allowHyper = false;
    private float currentTurn = 0f;

    public void Start()
    {
        currentTurn = maxTurnRate;
    }

    public void Update()
    {
        if(playerThrustScript.isThrusting == false)
        {
            allowHyper = false;
            hyperMode = false;
        }
        else
        {
            allowHyper = true;
        }
        if(hyperMode == true)
        {
            AddThrust();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetButtonDown("Hyper") && allowHyper == true)
        {
            hyperMode = true;
            switchThrust = true;
        }
        else if (switchThrust == true && (Input.GetButtonUp("Hyper") || allowHyper == false))
        {
            playerThrustScript.ResetThrust();
            switchThrust = false;
            hyperMode = false;
            currentShake = 0;
            currentTurn = maxTurnRate;
            playerTurnScript.HyperSpeedModeOff();
        }
	}

    private void AddThrust()
    {
        // Variable Camera Shake
        if (currentShake < maxShake)
        {
            currentShake += increaseShakeRate;
        }
        else if (currentShake > maxShake)
        {
            currentShake = maxShake;
        }
        // Make Camera Shake
        cameraShakeScript.ShakeCamera(currentShake);
        // Add Thrust to player
        if (playerThrustScript.thrust < addedTrust)
        {
            playerThrustScript.AddToThrustOutside(increaseThrustRate, true);
        }
        else
        {
            playerThrustScript.thrust = addedTrust;
        }
        // Change Turn
        playerTurnScript.HyperSpeedModeOn(currentTurn);
        if (currentTurn > 0)
        {
            currentTurn -= decreaseTurnRate;
        }
        else
        {
            currentTurn = 0;
        }
    }
}
