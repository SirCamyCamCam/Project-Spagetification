using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    //Global Variables
    // Public
    public Sprite[] aImages;
    public Dictionary<int, PolygonCollider2D> olFrameColliders;
    public Animator planeAnimation;
    public Transform playerTransform;
    public int currentFrame;
    public bool allowChange = true;
    // Private
    private int oldFrame;
    private SpriteRenderer oSpriteRenderer;

    // Update is called once per frame
    void Update()
    {
        if (allowChange == true)
        {
            SetFrameBasedOnAngle();
            SetFrame();
        }
        if (currentFrame != oldFrame)   // if the current frame is different from the frame displayed last update
        {
            oSpriteRenderer.sprite = aImages[currentFrame];     // display the current sprite (frame)
            EnableCollider(true);               // enable the associated polygon collider
            oldFrame = currentFrame;                // update the old frame to the current frame so we can detect the next change in the image
        }
    }

    void Awake()
    {
        oSpriteRenderer = this.GetComponent<SpriteRenderer>();

        // Creates the colliders
        olFrameColliders = new Dictionary<int, PolygonCollider2D>();    // instantiate the Dictionary.
        
        for (int index = 0; index < aImages.Length; index++)    // loop through each Sprite (image) in our Images array
        {
            oSpriteRenderer.sprite = aImages[index];            // switch to the current image we are processing
            olFrameColliders.Add(index, gameObject.AddComponent<PolygonCollider2D>());             // create the polygon collider and add it to the Dictionary
            olFrameColliders[index].enabled = false;             // disable the collider
            olFrameColliders[index].isTrigger = true;            // set this as a Trigger.
        }

        // Initialize to the correct image and enable
        currentFrame = 0;            
        oldFrame = 0;
        oSpriteRenderer.sprite = aImages[currentFrame];
        EnableCollider(true);
    }

    // Enables the collider
    private void EnableCollider(bool TrueOrFalse)
    {
        olFrameColliders[oldFrame].enabled = false;        // always disable the old collider
        olFrameColliders[currentFrame].enabled = TrueOrFalse;        // enable or disable the current collider as requested
    }

    private void SetFrame()
    {
        planeAnimation.PlayInFixedTime("planeBarrelRollAnimation", 0, currentFrame);
    }

    // Takes current angle and assigns frame, desicion trees worst 12 desisions
    private void SetFrameBasedOnAngle()
    {
        //Set frame for angle
        if ((playerTransform.rotation.eulerAngles.z > 353.587f && playerTransform.rotation.eulerAngles.z <= 360f) || (playerTransform.rotation.eulerAngles.z >= 0 && playerTransform.rotation.eulerAngles.z < 96.435f))
        {
            if ((playerTransform.rotation.eulerAngles.z > 353.587f && playerTransform.rotation.eulerAngles.z <= 360f) || (playerTransform.rotation.eulerAngles.z >= 0 && playerTransform.rotation.eulerAngles.z < 6.429f))
            {
                currentFrame = 0;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 6.429f && playerTransform.rotation.eulerAngles.z < 19.287f)
            {
                currentFrame = 1;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 19.287f && playerTransform.rotation.eulerAngles.z < 32.145f)
            {
                currentFrame = 2;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 32.145f && playerTransform.rotation.eulerAngles.z < 45.003f)
            {
                currentFrame = 3;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 45.003f && playerTransform.rotation.eulerAngles.z < 57.861f)
            {
                currentFrame = 4;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 57.861f && playerTransform.rotation.eulerAngles.z < 70.719f)
            {
                currentFrame = 5;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 70.719f && playerTransform.rotation.eulerAngles.z < 83.557f)
            {
                currentFrame = 6;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 83.557f && playerTransform.rotation.eulerAngles.z < 96.435f)
            {
                currentFrame = 7;
            }
        }
        if (playerTransform.rotation.eulerAngles.z >= 96.435f && playerTransform.rotation.eulerAngles.z < 199.229f)
        {
            if (playerTransform.rotation.eulerAngles.z >= 96.435f && playerTransform.rotation.eulerAngles.z < 109.293f)
            {
                currentFrame = 8;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 109.293f && playerTransform.rotation.eulerAngles.z < 122.151f)
            {
                currentFrame = 9;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 106.875 && playerTransform.rotation.eulerAngles.z < 118.125f)
            {
                currentFrame = 10;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 118.125f && playerTransform.rotation.eulerAngles.z < 135.009f)
            {
                currentFrame = 11;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 135.009f && playerTransform.rotation.eulerAngles.z < 147.867f)
            {
                currentFrame = 12;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 147.867f && playerTransform.rotation.eulerAngles.z < 160.725f)
            {
                currentFrame = 13;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 160.725f && playerTransform.rotation.eulerAngles.z < 173.583f)
            {
                currentFrame = 14;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 186.441f && playerTransform.rotation.eulerAngles.z < 199.229f)
            {
                currentFrame = 15;
            }
        }
        if (playerTransform.rotation.eulerAngles.z >= 199.229f && playerTransform.rotation.eulerAngles.z < 289.302f)
        {
            if (playerTransform.rotation.eulerAngles.z >= 199.229f && playerTransform.rotation.eulerAngles.z < 212.157f)
            {
                currentFrame = 16;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 212.125f && playerTransform.rotation.eulerAngles.z < 225.015f)
            {
                currentFrame = 17;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 225.015f && playerTransform.rotation.eulerAngles.z < 237.873f)
            {
                currentFrame = 18;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 237.873f && playerTransform.rotation.eulerAngles.z < 250.731)
            {
                currentFrame = 19;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 250.731 && playerTransform.rotation.eulerAngles.z < 263.558f)
            {
                currentFrame = 20;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 263.558f && playerTransform.rotation.eulerAngles.z < 276.445f)
            {
                currentFrame = 21;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 276.445f && playerTransform.rotation.eulerAngles.z < 289.302f)
            {
                currentFrame = 22;
            }
    }
        if (playerTransform.rotation.eulerAngles.z >= 289.302f && playerTransform.rotation.eulerAngles.z < 354.375f)
        { 
            if (playerTransform.rotation.eulerAngles.z >= 289.302f && playerTransform.rotation.eulerAngles.z < 302.159f)
            {
                currentFrame = 23;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 302.159f && playerTransform.rotation.eulerAngles.z < 315.016f)
            {
                currentFrame = 24;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 315.016f && playerTransform.rotation.eulerAngles.z < 327.873f)
            {
                currentFrame = 25;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 327.873f && playerTransform.rotation.eulerAngles.z < 340.73f)
            {
                currentFrame = 26;
            }
            else if (playerTransform.rotation.eulerAngles.z >= 340.73f && playerTransform.rotation.eulerAngles.z < 353.587f)
            {
                currentFrame = 27;
            }
        }
    }

    // Something for the Grab and freeze function
}
