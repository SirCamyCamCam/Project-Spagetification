using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// May need to remove Time.deltaTime!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
public class MoveRelativeToPlayer : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public Rigidbody2D objectRigidbody;
    public Transform objectTransform;
    public Transform targetTransform;
    [Header("Variables")]
    public int xMovementThrust = 3500;
    public int yMovementThrust = 4500;
    public int minXDistance = 70;
    public int minYBellowDistance = -60;
    public int minYAboveDistance = -30;
    public bool tilts = false;
    public float tiltRate = 0.5f;
    [SerializeField]
    public bool playerDead = false;
    // Private
    private Transform orginalTargetTransform;
    private Vector3 playerPosition;
    
	// Use this for initialization
	void Start () {
        orginalTargetTransform = targetTransform;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerDead == false)
        {
            playerPosition = targetTransform.position - objectTransform.position;       // Find position relative to object
        }
        else
        {
            playerPosition = -targetTransform.position - objectTransform.position;
        }

        if(playerPosition.y < minYBellowDistance)       // If bellow min distance
        {
            objectRigidbody.AddForce(Vector3.up * -yMovementThrust * Time.deltaTime);       // Add y thrust
        }
        else if(playerPosition.y > minYAboveDistance)       // If above min distance
        {
            objectRigidbody.AddForce(Vector3.up * yMovementThrust * Time.deltaTime);        // Add y thrust
        }
        if (playerPosition.x < minXDistance)             // If behind min distance
        {
            objectRigidbody.AddForce(Vector3.right * -xMovementThrust * Time.deltaTime);    // Add x thrust
            if (tilts == true && (objectTransform.rotation.eulerAngles.z < 15 || objectTransform.rotation.eulerAngles.z > 340))      // Tilts and checks angles
            {
                objectTransform.Rotate(Vector3.forward * tiltRate);    // Tilt backward
            }
        }
        else if (playerPosition.x > minXDistance)        // If infront min distance
        {
            objectRigidbody.AddForce(Vector3.right * xMovementThrust * Time.deltaTime);     // Add x thrust
            if (tilts == true && (objectTransform.rotation.eulerAngles.z > 345 || objectTransform.rotation.eulerAngles.z < 20))
            {
                objectTransform.Rotate(Vector3.forward * -tiltRate);
            }
        }
        else                // If no movement
        {
            if (tilts == true && targetTransform.rotation.eulerAngles.z > 340 && targetTransform.rotation.eulerAngles.z < 358)      // Reset angles
            {
                targetTransform.Rotate(Vector3.forward * 1);        // Rotate
            }
            else if (tilts == true && targetTransform.rotation.eulerAngles.z < 20 && targetTransform.rotation.eulerAngles.z > 2)    // Reset angles
            {
                targetTransform.Rotate(Vector3.forward * -1);       // Rotate
            }
        }
    }

    // Sets new target
    public void SetNewTarget(Transform newTarget)
    {
        targetTransform = newTarget;
    }

    // Reset to original 
    public void ResetToOriginalTarget()
    {
        targetTransform = orginalTargetTransform;
    }
}
