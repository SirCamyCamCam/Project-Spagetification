using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNewTarget : MonoBehaviour {

    // Global Variables
    // Public
    public EnemyShoot enemyShootScript;
    public AimAtTarget aimAtTargetScript;
    public TargetNewTarget targetMineScript;
    public Transform targetTransform;
    public int percentChance = 15;
    // Private
    private Transform newTargetTransform;
    private bool isOnFocus = false;
    private int randomChance = 0;
    private int minChance = 0;
    private int maxChance = 100;

	// Update is called once per frame
	void Update () {
        if (isOnFocus == true && newTargetTransform == null)           // If we are focusing and target no onger exists
        {
            enemyShootScript.DisableForceShoot();
            aimAtTargetScript.ResetToOriginal();
            targetMineScript.enabled = false;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)     // Checks for collisions
    {
        if(isOnFocus == false && collision.gameObject.tag == targetTransform.tag)       // If we are not already focused
        {
            randomChance = RandomChance();              // Find random chance
            if(randomChance <= percentChance)           // If random chance is within range
            {
                targetMineScript.enabled = true;        // Enable the Update
                SetNewTarget(collision.gameObject.transform);       // Set new target
            }
        }
    }

    // Force to shoot and set new target
    public void SetNewTarget(Transform newTarget)
    {
        enemyShootScript.ForceToShoot();                    // Force enemy to shoot
        aimAtTargetScript.ChangeTarget(newTarget);          // Sets new aiming target
        isOnFocus = true;                                   // Is now on focus
        newTargetTransform = newTarget;                     // Set new targte transform
    }

    // Gets random chance to focus
    public int RandomChance()
    {
        return Random.Range(minChance, maxChance);
    }
}
