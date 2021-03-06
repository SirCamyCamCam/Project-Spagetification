using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWithRandomness : MonoBehaviour {

    // Global Variables
    // Public 
    [Header("Objects")]
    public Transform targetTransform;
    public Transform objectTransform;
    [Header("Variables")]
    public bool enableRandomness = true;
    public float turnSpeed = 10.0f;
    public float randomMultiplyerMax = 1.3f;
    public float randomMultiplyerMin = 0.7f;
    public float randomMovementRate = 0.002f;
    public float waitForNewRandomSeconds = 1.0f;
    public float noRandomnessChance = 0.5f;
    public Quaternion q;
    public bool allowAiming = true;
    [SerializeField]
    public bool playerDead = false;
    // Private
    private Transform originalTargetTransform;
    private float randomVariationChoice = 1.0f;
    private float randomMovementVariation = 1.0f;
    private bool getNewRandomVal = true;


    // Use this for initialization
    void Start () {
        originalTargetTransform = targetTransform;
	}
	
	// Update is called once per frame
	void Update () {
        if (allowAiming == true)
        {
            if (enableRandomness == true)
            {
                RandomFoo();
            }
            if (playerDead == false)
            {
                TurnToTarget();         // turns toward target
            }
            else
            {
                TurnAwayFromTarget();
            }
        }
	}

    // Does random stuff
    private void RandomFoo()
    {
        if (getNewRandomVal == true)        // If we should get new random offset value
        {
            randomVariationChoice = RandomMovementRange();                      // Get new val
            StartCoroutine(WaitToGetNewRandomVal(waitForNewRandomSeconds));         // Set timer for new val
            getNewRandomVal = false;                                            // Wait till new val needed
        }
        if (randomVariationChoice <= noRandomnessChance)     // If we should be 100% accruate
        {
            if (randomMovementVariation < 1)             // If less than default
            {
                randomMovementVariation += randomMovementRate;          // Add
            }
            else if (randomMovementVariation > 1)                // If greater than default
            {
                randomMovementVariation -= randomMovementRate;          // subtract
            }
            else
            {
                randomMovementVariation = 1;            // Set to default
            }
        }
        else if (randomVariationChoice > noRandomnessChance && randomVariationChoice < (1.0 - (noRandomnessChance / 2.0)))       // If greater than no change chance but less than half no random
        {
            if (randomMovementVariation < randomMultiplyerMax)       // If less than max
            {
                randomMovementVariation += randomMovementRate;          // Add
            }
            else
            {
                randomMovementVariation = randomMultiplyerMax;          // else set to max
            }
        }
        else if (randomVariationChoice > (1.0 - (noRandomnessChance / 2)))       // If greater than second half of random chance
        {
            if (randomMovementVariation > randomMultiplyerMin)       // If greater than min
            {
                randomMovementVariation -= randomMovementRate;      // Subtract
            }
            else
            {
                randomMovementVariation = randomMultiplyerMin;      // Set to min
            }
        }
    }

    // Turns to the target
    public void TurnToTarget()
    {
        Vector3 vectorToTarget = targetTransform.position - objectTransform.position;
        float angleTarget = Mathf.Atan2(-vectorToTarget.y, -vectorToTarget.x) * Mathf.Rad2Deg;
        q = Quaternion.AngleAxis(angleTarget * randomMovementVariation, Vector3.forward);
        //Debug.Log("Random movement Variation:" + randomMovementVariation);
        objectTransform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);
    }

    // Turns away from the target
    public void TurnAwayFromTarget()
    {
        Vector3 vectorToTarget = -targetTransform.position - objectTransform.position;
        float angleTarget = Mathf.Atan2(-vectorToTarget.y, -vectorToTarget.x) * Mathf.Rad2Deg;
        q = Quaternion.AngleAxis(angleTarget * randomMovementVariation, Vector3.forward);
        //Debug.Log("Random movement Variation:" + randomMovementVariation);
        objectTransform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);
    }

    // Set new transform
    public void SetNewTarget(Transform newTarget, bool withRandomness)
    {
        targetTransform = newTarget;        // Set new target
        if(withRandomness == true)      // Turn on or off randomness
        {
            enableRandomness = true;
        }
        else
        {
            enableRandomness = false;
        }
    }

    // Set original target
    public void SetOriginalTarget()
    {
        targetTransform = originalTargetTransform;
        enableRandomness = true;
    }

    // Find random movement range
    private float RandomMovementRange()
    {
        return Random.value;
    }

    // Wait time to get new random val
    private IEnumerator WaitToGetNewRandomVal(float sec)
    {
        yield return new WaitForSeconds(sec);
        getNewRandomVal = true;
    }
}
