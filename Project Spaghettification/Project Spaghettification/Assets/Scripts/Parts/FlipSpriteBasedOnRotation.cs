using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpriteBasedOnRotation : MonoBehaviour {

    // Global Varibales
    // Public
    public Transform objectTransform;
    public SpriteRenderer objectSprite;
    public int frames = 3;
    // Private
    private int currentFrame = 0;
    private bool facingRight = true;

	// Update is called once per frame
	void Update () {
		if(currentFrame % frames == 0)
        {
            if(facingRight == true && objectTransform.rotation.eulerAngles.z > 90 && objectTransform.rotation.eulerAngles.z < 270)
            {
                facingRight = false;
                objectTransform.localScale = new Vector2(objectTransform.localScale.x, -objectTransform.localScale.y);
            }
            else if(facingRight == false && (objectTransform.rotation.eulerAngles.z <= 90 || objectTransform.rotation.eulerAngles.z >= 270))
            {
                facingRight = true;
                objectTransform.localScale = new Vector2(objectTransform.localScale.x, -objectTransform.localScale.y);
            }
        }
	}
}
