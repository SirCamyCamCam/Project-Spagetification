using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public DropDownSpawner dropDownSpawnerScript;
    public Transform objectTransform;
    public GameObject objectGameObject;
    public Rigidbody2D bombRigidbody;
    public Rigidbody2D balloonsRigidbody;
    public Transform bombTransform;
    public Transform balloonsTransform;
    public Health bombHealthScript;
    public Health balloonHealthScript;
    public MineController mineControllerScript;
    [Header("Variables")]
    public float movingDownGravity = 4.0f;
    public float defaultGravity = 1.0f;
    public float fallingGravity = 20.0f;
    public float fallingDrag = 0.5f;
    public float ballonMaxRiseSpeed = 2.0f;
    public float baloonRiseRate = 0.2f;
    // Private
    private float ySpawnPoint = 0;
    private bool reachedSpawn = false;
    private float balloonRiseSpeed = 0.0f;
	
	// Update is called once per frame
	void Update () {
		if(objectTransform.position.y < ySpawnPoint && reachedSpawn == false)       // If we are above target location
        {
            if (bombRigidbody != null)      // If bombRigidbody is not null
            {
                bombRigidbody.gravityScale = movingDownGravity;     // Make gravity
            }
        }
        else
        {
            if(bombRigidbody != null && balloonsRigidbody != null)      // If both rigidbodies still exist
            {
                bombRigidbody.gravityScale = defaultGravity;
            }
            reachedSpawn = true;        // We have reached the spawn
            mineControllerScript.enabled = false;           // Disable script
        }
        if(bombTransform == null && balloonsTransform == null)      // If neither exist, delete
        {
            dropDownSpawnerScript.spawnCount -= 1;
            Destroy(objectGameObject);
        }
        if(balloonsTransform == null && bombTransform != null)      // If ballons have popped
        {
            if(bombRigidbody != null)
            {
                bombRigidbody.gravityScale = fallingGravity;        // Set falling gravity
                bombRigidbody.drag = fallingDrag;                   // Set falling drag
            }
        }
        if(bombTransform == null &&  balloonsTransform != null)     // If mine exploded
        {
            balloonsTransform.Translate(Vector3.up * balloonRiseSpeed);     // Move upward
            if(balloonRiseSpeed < ballonMaxRiseSpeed)       // If max sped not met
            {
                balloonRiseSpeed += baloonRiseRate;     // Add speed
            }
            else
            {
                balloonRiseSpeed = ballonMaxRiseSpeed;      // Set to max
            }
        }
	}

    // Assigns the Y spawn location that it needs to float down to
    public void GetYSpawnPoint(float y)
    {
        ySpawnPoint = y;
    }
}
