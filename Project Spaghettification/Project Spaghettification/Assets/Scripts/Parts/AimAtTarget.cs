using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtTarget : MonoBehaviour {

    // Global Variables
    // Public
    public Transform targetTranform;
    public Transform objectTrasform;
    public float spinSpeed = 3.0f;
    public bool shouldAim = true;
    public Quaternion q;
    [SerializeField]
    public bool playerDead = false;
    // Private
    private Transform originalTargetTransform;

	// Use this for initialization
	void Start () {
        originalTargetTransform = targetTranform;
	}
	
	// Update is called once per frame
	void Update () {
		if(shouldAim == true && playerDead == false)
        {
            PointAtTarget(targetTranform);
        }
        else if(shouldAim == true && playerDead == true)
        {
            PointAwayFromTarget(targetTranform);
        }
	}

    // Points the objectTranfrom at targetTransform
    private void PointAtTarget(Transform target)
    {
        Vector3 vectorToTarget = target.position - objectTrasform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        q = Quaternion.AngleAxis(angle, Vector3.forward);
        objectTrasform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * spinSpeed);
    }

    // Points opposite the objectTranfrom at targetTransform
    private void PointAwayFromTarget(Transform target)
    {
        Vector3 vectorToTarget = -target.position - objectTrasform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        q = Quaternion.AngleAxis(angle, Vector3.forward);
        objectTrasform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * spinSpeed);
    }

    // Changes target
    public void ChangeTarget(Transform newTarget)
    {
        targetTranform = newTarget;
    }

    // Stop Aiming
    public void StopAiming()
    {
        shouldAim = false;
    }

    // Starts aiming
    public void StartAiming()
    {
        shouldAim = true;
    }

    // Reset to original target
    public void ResetToOriginal()
    {
        targetTranform = originalTargetTransform;
    }
}
