using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionCameraShake : MonoBehaviour {

    // Global Variables
    // Public
    public GroundCollisionCameraShake groundCollisionCameraShakeScript;
    public Rigidbody2D targetRigidbody;
    public ShieldController shieldControllerScript;
    public int collisionSpeed = 200;
    public float shakeIntensity = 0.2f;
    public bool hasShield = false;


	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasShield == false && collision.gameObject.tag == "Ground" && targetRigidbody.velocity.y <= -collisionSpeed)     // For things without shields 
        {
            //CameraShake.ShakeCamera(shakeIntensity);      // Calls camera shake
        }
        if(hasShield == true && collision.gameObject.tag == "Ground" && targetRigidbody.velocity.y >= -collisionSpeed && shieldControllerScript.shieldStatus == false)       // If objects has shield makes sure it is off
        {
            //CameraShake.ShakeCamera(shakeIntensity);      // Calls camera shake
        }
    }
}
