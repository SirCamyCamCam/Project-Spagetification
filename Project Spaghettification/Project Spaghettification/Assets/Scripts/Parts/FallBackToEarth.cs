using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBackToEarth : MonoBehaviour {

    // Global Variables
    // Public
    public PlayerGravity playerGravityScript;
    public FallBackToEarth fallBackToEarthScript;
    public Transform objectTransform;
    public Rigidbody2D objectRigidbody2D;
    public float maxGravity = 5f;
    public float increaseRate = 0.1f;
    public float decreaseRate = 0.4f;
    public float waitTime = 0.2f;
    //Private
    private bool isOutOfBounds = true;
    public float initalGravity = 0;
    private Transform spaceWall;
    private Coroutine waitTimer = null;

	// Use this for initialization
	void Start () {
        fallBackToEarthScript.enabled = false;
        initalGravity = playerGravityScript.maximumGravity;
	}

    // Update is called once per frame
    private void Update()
    {
        if (objectTransform.position.y >= spaceWall.position.y)
        {
            if(waitTimer != null)
            {
                StopCoroutine(waitTimer);
                waitTimer = null;
            }
            isOutOfBounds = true;
        }
        else 
        {
            if (waitTimer == null)
            {
                waitTimer = StartCoroutine(FallTimer());
            }
        }
    }

    // Fixed update for physics
    void FixedUpdate()
    {
        // Add Gravity
        if (playerGravityScript != null && isOutOfBounds == true)
        {
            IncreaseGravity();
        }
        // Remove gravity and reset
        else if (playerGravityScript != null && isOutOfBounds == false && objectRigidbody2D.gravityScale > initalGravity)
        {
            objectRigidbody2D.gravityScale -= decreaseRate;
        }
        else if (playerGravityScript != null && isOutOfBounds == false && objectRigidbody2D.gravityScale <= initalGravity)
        {
            playerGravityScript.allowGravity = true;
            fallBackToEarthScript.enabled = false;
        }
    }

    // Sets gravity
    private void IncreaseGravity()
    {
        if (objectRigidbody2D.gravityScale < maxGravity)
        {
            objectRigidbody2D.gravityScale += increaseRate;
        }
        else if (objectRigidbody2D.gravityScale > maxGravity)
        {
            objectRigidbody2D.gravityScale = maxGravity;
        }
    }

    // Checks if collided with space wall
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Inital
        if(collision.gameObject.tag == "SpaceWall")
        {
            spaceWall = collision.gameObject.transform;
            fallBackToEarthScript.enabled = true;
            playerGravityScript.allowGravity = false;
            isOutOfBounds = true;
        }
    }

    private IEnumerator FallTimer()
    {
        yield return new WaitForSeconds(waitTime);
        isOutOfBounds = false;
        waitTimer = null;
    }
}
