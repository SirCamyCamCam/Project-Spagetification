using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovementController : MonoBehaviour {

    // Global
    // Public
    [Header("Objects")]
    public Transform target;
    public Transform objectTransform;
    public Rigidbody2D objectRigidbody;
    [Header("Variables")]
    public int thrust = 2000;
    public int minimumDistance = 50;
	
	// Update is called once per frame
	void Update () {
        var playerPosition = target.position - objectTransform.position;
        if(playerPosition.x > 0 && playerPosition.magnitude > minimumDistance)
        {
            objectRigidbody.AddForce(transform.right * thrust * Time.deltaTime);
        }
        else if(playerPosition.x < 0 && playerPosition.magnitude > minimumDistance)
        {
            objectRigidbody.AddForce(transform.right * -thrust * Time.deltaTime);
        }
	}
}
