using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWithinRange : MonoBehaviour {

    // Global Variables
    // Public
    public EnemyShoot enemyShootScript;
    public GroundMovementController groundMovementControllerScript;
    public MoveRelativeToPlayer moveRelativeToPlayerScript;
    public Magnetic magneticScript;
    public AvoidGround avoidGroundScript;
    public Transform target;
    // Private
    private bool useEnemyShootScript = false;
    private bool useGroundMovementControllerScript = false;
    private bool useMoveRelativeToPlayerScript = false;
    private bool useMagneticScript = false;

    // Use this to intialize
    private void Start()
    {
        if(enemyShootScript != null)    // If were using shoot script
        {
            useEnemyShootScript = true;
        }
        if(groundMovementControllerScript  != null)     // If were using moveing on ground script
        {
            useGroundMovementControllerScript = true;
        }
        if(moveRelativeToPlayerScript != null)      // If were using move relative to player
        {
            useMoveRelativeToPlayerScript = true;
        }
        if(magneticScript != null)
        {
            useMagneticScript = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)     // Checks for collision
    {
        if (collision.gameObject.tag == target.tag)      // Checks for target tags
        {
            if (useEnemyShootScript == true)
            {
                enemyShootScript.enabled = true;        // Turns on shoot function
            }
            if (useGroundMovementControllerScript == true)
            {
                groundMovementControllerScript.enabled = true;      // Turns on ground movement
            }
            if (useMoveRelativeToPlayerScript == true)
            {
                moveRelativeToPlayerScript.enabled = true;      // Turns on relative to player movement
            }
            if (useMagneticScript == true)
            {
                magneticScript.enabled = true;
                magneticScript.withinRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)      // Checks for coliisions
    {
        if(collision.gameObject.tag == target.tag)      // Checks for target tag
        {
            if (useEnemyShootScript == true)
            {
                //enemyShootScript.enabled = false;       // Turns off shoot
            }
            if(useGroundMovementControllerScript == true)
            {
                groundMovementControllerScript.enabled = false;     // Turns on ground movement
            }
            if(useMoveRelativeToPlayerScript == true)
            {
                moveRelativeToPlayerScript.enabled = false;     // Turns off realtive to player movement
            }
            if(useMagneticScript == true)
            {
                magneticScript.withinRange = false;
            }
        }
    }
}
