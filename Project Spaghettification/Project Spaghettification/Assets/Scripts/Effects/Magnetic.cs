using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour {

    // Global Variables
    // Public
    public Transform targetTransform;
    public Transform objectTransform;
    public Magnetic magneticScript;
    public bool withinRange = false;
    public float maxSpeed = 100.0f;
    public float rate = 2.0f;
    // Private 
    private float speed = 0;
	
	// Update is called once per frame
	void Update () {
        objectTransform.position = Vector3.MoveTowards(objectTransform.position, targetTransform.transform.position, speed * Time.deltaTime);       // Move towrd target
        if (withinRange == true)        // If withing range of object
        {
            if (speed < maxSpeed)   // If less than max speed
            {
                speed += rate;      // Increase the speed
            }
            else
            {
                speed = maxSpeed;       // Set to max
            }
        }
        else
        {
            if(speed > 0)       // If speed is greater than 0
            {
                speed -= rate;      // Remove by rate
            }
            else
            {
                speed = 0;      // Set speed to 0
                magneticScript.enabled = false;     // Disable script
            }
        }
    }
}
