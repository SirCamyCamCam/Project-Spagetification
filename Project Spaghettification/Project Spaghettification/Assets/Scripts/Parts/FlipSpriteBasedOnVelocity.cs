using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpriteBasedOnVelocity : MonoBehaviour {

    // Global Variables
    public SpriteRenderer objectSprite;
    public Rigidbody2D objectRigidbody;
    public int frames = 3;
    // Private
    private bool facingRight = true;
    private int currentFrame = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currentFrame % frames == 0)
        {
            if (objectRigidbody.velocity.x > 0 && facingRight == true)
            {
                objectSprite.flipY = true;
                facingRight = false;
            }
            else if (objectRigidbody.velocity.x < 0 && facingRight == false)
            {
                objectSprite.flipY = false;
                facingRight = true;
            }
        }
        currentFrame++;
	}
}
