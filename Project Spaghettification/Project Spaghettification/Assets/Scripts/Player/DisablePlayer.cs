using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayer : MonoBehaviour {

    // Global Variables
    // Public
    public PlayerThrust playerThrustScript;
    public PlayerTurn playerTurnScript;
    public ShieldController shieldScript;
    public PlayerShoot playerShootScript;
    public PlayerWindResistance playerWindResistanceScript;
    public PlayerBoost playerBoostScript;

    // Disables all controls
    public void DisableAllControls()
    {
        playerThrustScript.allowThrust = false;
        playerTurnScript.allowTurning = false;
        shieldScript.allowShield = false;
        playerShootScript.allowShooting = false;
        if (playerWindResistanceScript != null)
        {
            playerWindResistanceScript.enabled = false;
        }
        playerBoostScript.allowThrust = false;
    }

    // Enables all controls
    public void EnableAllControls()
    {
        playerTurnScript.allowTurning = true;
        playerThrustScript.allowThrust = true;
        shieldScript.allowShield = true;
        playerShootScript.allowShooting = true;
        if (playerWindResistanceScript != null)
        {
            playerWindResistanceScript.enabled = true;
        }
        playerBoostScript.allowThrust = true;
    }

    // Enables all but shield
    public void AllowControlsWithoutShield()
    {
        playerTurnScript.allowTurning = true;
        playerThrustScript.allowThrust = true;
        playerShootScript.allowShooting = true;
        if (playerWindResistanceScript != null)
        {
            playerWindResistanceScript.enabled = true;
        }
        playerBoostScript.allowThrust = true;
    }

    // Disables all but shield
    public void DisableControlsWithoutShield()
    {
        playerShootScript.allowShooting = false;
        playerThrustScript.allowThrust = false;
        playerTurnScript.allowTurning = false;
        if (playerWindResistanceScript != null)
        {
            playerWindResistanceScript.enabled = false;
        }
        playerBoostScript.allowThrust = true;
    }
}
