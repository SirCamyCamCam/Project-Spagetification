using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpriteBasedOnPlayerPos : MonoBehaviour {

    // Global Variable
    // Public
    public Transform targetTransform;
    public Transform objectTransform;
    public SpriteRenderer objectSprite;
    // Private
    private bool facingRight = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(targetTransform.position.x > objectTransform.position.x && facingRight == false)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingRight = true;
        }
        else if(targetTransform.position.x < objectTransform.position.x && facingRight == true)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingRight = false;
        }
	}
}
