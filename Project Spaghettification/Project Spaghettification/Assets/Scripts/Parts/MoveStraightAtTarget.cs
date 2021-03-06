using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraightAtTarget : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public Transform objectTransform;
    public Rigidbody2D objectRigidbody;
    [Header("Variables")]
    public bool useTransform = false;
    public bool useRgidbody = false;
    public float transformSpeed = 3.0f;
    public float rigidbodyForce = 10.0f;
    public bool allowMovement = true;
	
	// Update is called once per frame
	void Update () {
		if(allowMovement == true)
        {
            if(useRgidbody == true)
            {
                objectRigidbody.AddForce(objectTransform.right * rigidbodyForce);
            }
            if(useTransform == true)
            {
                objectTransform.Translate(Vector2.right * transformSpeed * Time.deltaTime);
            }
        }
	}
}
