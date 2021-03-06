using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnemyControls : MonoBehaviour {

    // Global Variables
    // Public
    public EnemySoraingThrust enemySoraingThrustScript;
    public AimWithRandomness aimWithRandomnessScript;
    public ShieldController shieldControllerScript;
    public EnemyShoot enemyShootScript;

    // Diables conrols
    public void DisableControls(bool includeShield)
    {
        if (enemySoraingThrustScript != null)
        {
            enemySoraingThrustScript.allowThrust = false;
        }
        if (aimWithRandomnessScript != null)
        {
            aimWithRandomnessScript.allowAiming = false;
        }
        if (shieldControllerScript != null && includeShield == true)
        {
            shieldControllerScript.allowShield = false;
        }
        if (enemyShootScript != null)
        {
            enemyShootScript.allowShooting = false;
        }
    }

    // Enables the controls
    public void ReEnableControls(bool includeShield)
    {
        if (enemySoraingThrustScript != null)
        {
            enemySoraingThrustScript.allowThrust = true;
        }
        if (aimWithRandomnessScript != null)
        {
            aimWithRandomnessScript.allowAiming = true;
        }
        if (shieldControllerScript != null && includeShield == true)
        {
            shieldControllerScript.allowShield = true;
        }
        if (enemyShootScript != null)
        {
            enemyShootScript.allowShooting = true;
        }
    }
}
