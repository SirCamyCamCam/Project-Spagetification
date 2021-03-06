using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidGround : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public Transform objectTransform;
    public Transform groundTransform;
    public Rigidbody2D objectRigidbody;
    public AvoidGround avoidGroundScript;
    public AimWithRandomness aimWithRandomnessScript;
    public AimAtTarget aimAtTargetScript;
    public EnemySoraingThrust enemySoraingThrustScript;
    public PlayerGravity playerGravityScript;
    public ShieldController shieldControllerScript;
    public ShieldBounce shieldBounceScript;
    public EnemyShoot enemyShootScript;
    [Header("Variables")]
    public float updateTime = 0.1f;
    public float distanceFromGround = 3;
    public float spinSpeed = 10;
    // Private
    private float distance;
    private bool turning = false;
    private int shieldBounceSpeed;

	// Use this for initialization
	void Start () {
        //avoidGroundScript.enabled = false;
        StartCoroutine(checkDistance());
        shieldBounceSpeed = shieldBounceScript.shieldBounceSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (shieldBounceScript.bouncing == false && distance <= distanceFromGround)
        {
            if (objectRigidbody.velocity.y > -shieldBounceSpeed || shieldControllerScript.allowShield == false)
            {
                if ((transform.localRotation.eulerAngles.z >= 0 && transform.localRotation.eulerAngles.z < 90) || (transform.localRotation.eulerAngles.z <= 360 && transform.localRotation.eulerAngles.z >= 300))
                {
                    rotate(-Vector3.forward);
                }
                else if (transform.localRotation.eulerAngles.z >= 90 && transform.localRotation.eulerAngles.z <= 240)
                {
                    rotate(Vector3.forward);
                }
                if (turning == false)
                {
                    enemyActions(false);
                }
            }
            else
            {
                shieldBounceScript.EnemyGroundBounce();
            }
        }
        else
        {
            if(turning == true && shieldBounceScript.bouncing == false)
            {
                enemyActions(true);
            }
        }
    }

    // Rotates the object away from the ground
    private void rotate(Vector3 direction)
    {
        transform.Rotate(direction * Time.deltaTime * spinSpeed);
    }
    
    // Controls Actions
    private void enemyActions(bool status)
    {
        if (aimWithRandomnessScript != null)
        {
            aimWithRandomnessScript.enabled = status;
        }
        if (aimAtTargetScript != null)
        {
            aimAtTargetScript.enabled = status;
        }
        if (enemySoraingThrustScript != null)
        {
            enemySoraingThrustScript.forceThrust = !status;
        }
        if (playerGravityScript != null)
        {
            playerGravityScript.allowGravity = status;
            if (status == false)
            {
                playerGravityScript.SetMinimumGravity();
            }
        }
        if(shieldControllerScript != null)
        {
            shieldControllerScript.allowShield = status;
        }
        if(enemyShootScript != null)
        {
            enemyShootScript.allowShooting = status;
        }
        turning = !status;
    }

    // Starts checking for ground
    public void CheckForGround()
    {
        StartCoroutine(checkDistance());
        avoidGroundScript.enabled = true;
    }
    
    // Stops Checking for groun
    public void StopCheckForGround()
    {
        StopCoroutine(checkDistance());
        avoidGroundScript.enabled = false;
    }

    private IEnumerator checkDistance()
    {
        distance = objectTransform.position.y - groundTransform.position.y;
        yield return new WaitForSeconds(updateTime);
        StartCoroutine(checkDistance());
    }
}
