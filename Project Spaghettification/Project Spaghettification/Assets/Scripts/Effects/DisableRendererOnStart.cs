using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRendererOnStart : MonoBehaviour {

    // Global Variables
    // Public
    public SpriteRenderer objectSprite;
    public Animator objectAnimator;

	// Use this for initialization
	void Start () {
        if(objectSprite != null)
            objectSprite.enabled = false;
        if (objectAnimator != null)
        {
            objectAnimator.StopPlayback();
            objectAnimator.enabled = false;
        }
	}
}
