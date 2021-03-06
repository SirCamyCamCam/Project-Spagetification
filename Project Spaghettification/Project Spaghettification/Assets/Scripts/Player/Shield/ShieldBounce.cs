using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBounce : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public PlayerUncontrollableSpin playerUncontrollableSpinScript;
    public PlayerGravity playerGravityScript;
    public Rigidbody2D objectRigidbody;
    public Transform raycastStartPoint1;
    public Transform raycastStartPoint2;
    public Transform raycastStartPoint3;
    public Transform playerTransform;
    [Header("Variables")]
    public int rotateSpeed = 6;
    public float rotateTime = 2;
    public float forceY = 1f;
    public float forceX = 0.3f;
    public int minSpeed = 3;
    public float seconds = 3f;
    public bool allowRaycast = false;
    [Header("Enemy")]
    public EnemyUncontrollableSpin enemyUncontrollableSpinScript;
    public EnemySoraingThrust enemySoraingThrustScript;
    public ShieldController shieldControllerScript;
    public AimWithRandomness aimWithRandomnessScript;
    public Transform objectTransform;
    public int shieldBounceSpeed = 20;
    public float turnSpeed = 300;
    [HideInInspector]
    public bool bouncing = false;
    [HideInInspector]
    public bool allowBouncing = true;
    // Private 
    private Vector2 direction;
    private bool shouldAddForceY = true;
    private bool shouldAddForceX = true;
    private bool shouldAddForceXY = true;
    private bool shouldAddForce = true;
    private bool enemyDownBounce = false;
    private bool enemyLeftBounce = false;
    private bool enemyRightBounce = false;
    private Coroutine yBounce;
    private Coroutine xBounce;
    private Coroutine xyBounce;
    private Coroutine bounceCor;
    private int layerMask = 1 << 8;

    private void Update()
    {
        if (allowRaycast == true)
        {
            RaycastHit2D hit1 = Physics2D.Raycast(raycastStartPoint1.position, -raycastStartPoint1.right, 0.0001f, layerMask);                        // Raycast at 0 degrees
            //RaycastHit2D hit2 = Physics2D.Raycast(raycastStartPoint2.position, -raycastStartPoint2.right, 0.0001f, layerMask);                        // Raycast at 60 degrees
            //RaycastHit2D hit3 = Physics2D.Raycast(raycastStartPoint3.position, -raycastStartPoint3.right, 0.0001f, layerMask);                        // Raycast at -60 degrees

            if (hit1.collider != null)              // Hit collider 1 collision checkk
            {
                CheckCollision(hit1);
            }
            /*if (hit2.collider != null)              // Hit collider 2 collision check
            {
                CheckCollision(hit2);
            }
            if (hit3.collider != null)              // Hit collider 3 collision check
            {
                CheckCollision(hit3);
            }*/
        }
        // Turns shield off and on
        /*if (shouldAddForceY == false && allowBouncing == true)               // If have a direction to add force for y
        {
            AddForceY();
        }
        if(shouldAddForceX == false && allowBouncing == true)                // If we have a direction to add force for x
        {
            AddForceX();
        }
        if(shouldAddForceXY == false && allowBouncing == true)
        {
            AddForceXY();
        }*/
        if(shouldAddForce == false && allowBouncing == true)
        {
            AddForce();
        }
        // Make enemy face direction for bounce for downward direction
        /*if(enemyDownBounce == true)
        {
            aimWithRandomnessScript.allowAiming = false;
            if ((objectTransform.rotation.eulerAngles.z >= 0 && objectTransform.rotation.eulerAngles.z < 86) || (objectTransform.rotation.eulerAngles.z <= 360 && objectTransform.rotation.eulerAngles.z > 270))
            {
                RotateToDirection(Vector3.forward);
            }
            else if (objectTransform.rotation.eulerAngles.z >= 94 && objectTransform.rotation.eulerAngles.z <= 270)
            {
                RotateToDirection(-Vector3.forward);
            }
            else
            {
                objectTransform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
        // Make enemy face direction for bounce for left direction
        if(enemyLeftBounce == true)
        {
            aimWithRandomnessScript.allowAiming = false;
            if ((objectTransform.rotation.eulerAngles.z > 0) && objectTransform.rotation.eulerAngles.z < 90)
            {
                RotateToDirection(-Vector3.forward);
            }
            else if (objectTransform.rotation.eulerAngles.z > 270 && objectTransform.rotation.eulerAngles.z < 360)
            {
                RotateToDirection(Vector3.forward);
            }
        }
        // Make enemy face direction for bounce for right direction
        if(enemyRightBounce == true)
        {
            aimWithRandomnessScript.allowAiming = false;
            if ((objectTransform.rotation.eulerAngles.z < 180) && objectTransform.rotation.eulerAngles.z > 90)
            {
                RotateToDirection(Vector3.forward);
            }
            else if ((objectTransform.rotation.eulerAngles.z > 180) && objectTransform.rotation.eulerAngles.z < 270)
            {
                RotateToDirection(-Vector3.forward);
            }
        }*/
    }

    // Rotates the enemy toward the ground
    private void RotateToDirection(Vector3 direction)
    {
        objectTransform.Rotate(direction * Time.deltaTime * turnSpeed);
    }

    // Checks for collisions with the raycast
    private void CheckCollision(RaycastHit2D hit)
    {
        // Ground
        if (hit.collider.tag == "Ground")
        {
            //BounceObjectYDirection(-hit.point);
            BounceObject(hit);
        }
        // Left wall
        if (hit.collider.tag == "LeftWall" || hit.collider.tag == "RightWall")
        {
            //BounceObjectXDirection(-hit.point);
            BounceObject(hit);
        }
        if(hit.collider.tag == "FloatingIsland")
        {
            //BounceObjectXYDirection(-hit.point);
            BounceObject(hit);
        }
    }

    // Adds force to object in y direction
    private void AddForceY()
    {
        objectRigidbody.velocity = new Vector2(objectRigidbody.velocity.x, direction.y * forceY);
    }

    // Adds force to object in x direction
    private void AddForceX()
    {
        objectRigidbody.velocity = new Vector2(direction.x * forceX, objectRigidbody.velocity.y);
    }

    // Adds force to the object in both x and y direction
    private void AddForceXY()
    {
        objectRigidbody.velocity = new Vector2(direction.x * forceX, direction.y * forceY);
    }

    private void AddForce()
    {
        objectRigidbody.velocity = new Vector2(direction.x * forceX, direction.y * forceY);
    }

    // Set direction and call other functions
    private void BounceObjectYDirection(Vector2 hitDirection)
    {
        if(shouldAddForceY == true)                                                                                    // If we haven't already started adding force
        {
            yBounce = StartCoroutine(AllowAnotherShieldY());                                                           // Call to kill force after seconds
            shouldAddForceY = false;                                                                                   // We are now adding force
            if(playerUncontrollableSpinScript != null)
            {
                playerUncontrollableSpinScript.MakePlayerSpin(rotateSpeed, rotateTime, true);                           // Spin the object
            }
            if(enemyUncontrollableSpinScript != null)
            {
                enemyUncontrollableSpinScript.MakeEnemySpin(rotateSpeed, rotateTime, false);
            }
        }
        else
        {
            StopCoroutine(yBounce);
            yBounce = StartCoroutine(AllowAnotherShieldY());
        }
        direction = hitDirection;                                                                                       // Assign direwction of hit
        playerGravityScript.allowGravity = false;                                                                       // Disable gravity during bounce
        playerGravityScript.gravity = 0;                                                                                // Set gravity to 0
    }

    // Set direction and call other functions
    private void BounceObjectXDirection(Vector2 hitDirection)
    {
        if (shouldAddForceX == true)                                                                                      // If we haven't already started adding force
        {
            xBounce = StartCoroutine(AllowAnotherShieldX());                                                              // Call to kill force after seconds
            shouldAddForceX = false;                                                                                      // We are now adding force
            if (playerUncontrollableSpinScript != null)
            {
                playerUncontrollableSpinScript.MakePlayerSpin(rotateSpeed, rotateTime, true);                                 // Spin the object
            }
            if(enemyUncontrollableSpinScript != null)
            {
                enemyUncontrollableSpinScript.MakeEnemySpin(rotateSpeed, rotateTime, false);
            }
        }
        else
        {
            StopCoroutine(xBounce);
            xBounce = StartCoroutine(AllowAnotherShieldX());
        }
        direction = hitDirection;                                                                                       // Assign direwction of hit
        playerGravityScript.allowGravity = false;                                                                       // Disable gravity during bounce
        playerGravityScript.gravity = 0;                                                                                // Set gravity to 0
    }

    // Set direction and call other functions
    private void BounceObjectXYDirection(Vector2 hitDirection)
    {
        if (shouldAddForceXY == true)                                                                                      // If we haven't already started adding force
        {
            xyBounce = StartCoroutine(AllowAnotherShieldXY());                                                              // Call to kill force after seconds
            shouldAddForceXY = false;                                                                                      // We are now adding force
            if (playerUncontrollableSpinScript != null)
            {
                playerUncontrollableSpinScript.MakePlayerSpin(rotateSpeed, rotateTime, true);                                 // Spin the object
            }
            if (enemyUncontrollableSpinScript != null)
            {
                enemyUncontrollableSpinScript.MakeEnemySpin(rotateSpeed, rotateTime, false);
            }
        }
        else
        {
            StopCoroutine(xyBounce);
            xyBounce = StartCoroutine(AllowAnotherShieldXY());
        }
        direction = hitDirection;                                                                                       // Assign direwction of hit
        playerGravityScript.allowGravity = false;                                                                       // Disable gravity during bounce
        playerGravityScript.gravity = 0;                                                                                // Set gravity to 0
    }

    // Bounces in set direction
    private void BounceObject(RaycastHit2D hitPoint)
    {
        if(shouldAddForce == true)
        {
            bounceCor = StartCoroutine(AllowAnotherShield());
            shouldAddForce = false;
            if(playerUncontrollableSpinScript != null)
            {
                playerUncontrollableSpinScript.MakePlayerSpin(rotateSpeed, rotateTime, true);
            }
        }
        else
        {
            StopCoroutine(bounceCor);
            bounceCor = StartCoroutine(AllowAnotherShield());
        }
        direction = reflectDirection(hitPoint);
        playerGravityScript.allowGravity = false;
        playerGravityScript.gravity = 0;
    }

    // Disables and renablea add force after seconds
    private IEnumerator AllowAnotherShieldY()
    {
        yield return new WaitForSeconds(seconds);           // Wait seconds
        shouldAddForceY = true;                             // Disable thrust
        playerGravityScript.allowGravity = true;            // Allow gravity again
        if(bouncing == true)
        {
            bouncing = false;
            if (enemyDownBounce == true)
            {
                enemyDownBounce = false;
            }
            if(enemyLeftBounce == true)
            {
                enemyLeftBounce = false;
            }
            if(enemyRightBounce == true)
            {
                enemyRightBounce = false;
            }
            shieldControllerScript.ForceShieldOn(false);
        }
    }

    // Disables and renables add force after seconds
    private IEnumerator AllowAnotherShield()
    {
        yield return new WaitForSeconds(seconds);
        shouldAddForce = true;
        playerGravityScript.allowGravity = true;
        if(bouncing == true)
        {
            bouncing = false;
            shieldControllerScript.ForceShieldOn(false);
        }
    }

    // Disables and renablea add force after seconds
    private IEnumerator AllowAnotherShieldX()
    {
        yield return new WaitForSeconds(seconds);           // Wait seconds
        shouldAddForceX = true;                             // Disable thrust
        playerGravityScript.allowGravity = true;            // Allow gravity again
        if (bouncing == true)
        {
            bouncing = false;
            if (enemyDownBounce == true)
            {
                enemyDownBounce = false;
            }
            if (enemyLeftBounce == true)
            {
                enemyLeftBounce = false;
            }
            if (enemyRightBounce == true)
            {
                enemyRightBounce = false;
            }
            shieldControllerScript.ForceShieldOn(false);
        }
    }

    // Disables and renables add force after seconds
    private IEnumerator AllowAnotherShieldXY()
    {
        yield return new WaitForSeconds(seconds);
        shouldAddForceXY = true;
        playerGravityScript.allowGravity = true;
        if (bouncing == true)
        {
            bouncing = false;
            if (enemyDownBounce == true)
            {
                enemyDownBounce = false;
            }
            if (enemyLeftBounce == true)
            {
                enemyLeftBounce = false;
            }
            if (enemyRightBounce == true)
            {
                enemyRightBounce = false;
            }
            shieldControllerScript.ForceShieldOn(false);
        }
    }

    // Makes enemy bounce when called
    public void EnemyGroundBounce()
    {
        if (allowBouncing == true)
        {
            bouncing = true;
            enemyDownBounce = true;
            shieldControllerScript.ForceShieldOn(true);
        }
    }

    // Makes enemy bounce when called
    public void EnemyLeftBounce()
    {
        if (allowBouncing == true)
        {
            bouncing = true;
            enemyLeftBounce = true;
            shieldControllerScript.ForceShieldOn(true);
        }
    }

    // Makes enemy bounce when called
    public void EnemyRightBounce()
    {
        if (allowBouncing == true)
        {
            bouncing = true;
            enemyRightBounce = true;
            shieldControllerScript.ForceShieldOn(true);
        }
    }

    private Vector2 reflectDirection(RaycastHit2D hitPoint)
    {
        Vector2 playerPos = new Vector2(playerTransform.position.x, playerTransform.position.y);
        //Vector2 incomingVec = hitPoint.point - playerPos;
        return Vector2.Reflect(playerPos, hitPoint.normal);
    }
}
