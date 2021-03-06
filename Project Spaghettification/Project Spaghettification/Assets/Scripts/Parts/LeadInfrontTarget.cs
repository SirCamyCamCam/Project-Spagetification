using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadInfrontTarget : MonoBehaviour {

    // Global Variables
    // Public
    public Transform targetTransform;
    public Rigidbody2D targetRigidbody;
    public float multiplyer = 1.1f;
    public float time = 2.5f;
	
	// Update is called once per frame
	void Update () {
        if (targetTransform != null && targetTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position + new Vector3(targetRigidbody.velocity.x, targetRigidbody.velocity.y, 0f) * multiplyer, Time.deltaTime * time);
        }
    }
}
