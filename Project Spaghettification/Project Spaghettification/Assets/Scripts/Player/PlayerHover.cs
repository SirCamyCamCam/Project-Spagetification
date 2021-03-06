using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHover : MonoBehaviour {

    // Global Variables
    // Public
    public PlayerThrust playerThrustScript;
    public PlayerGravity playerGravityScript;
    public PlayerDrag playerDragScript;
    public Rigidbody2D playerRigidbody;
    public float linearDrag = 1.1f;
    public float gravity = 0.05f;
    // Private
    private bool hoverEnabled = false;
    private int removeThrustAmmount = 1000;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Hover"))
        {
            if(hoverEnabled == false)
            {
                EnableHoverMode();
            }
            else
            {
                DisableHoverMode();
            }
        }
	}

    // Turns on hover mode
    public void EnableHoverMode()
    {
        hoverEnabled = true;
        playerGravityScript.allowGravity = false;
        playerGravityScript.gravity = gravity;
        playerRigidbody.gravityScale = gravity;
        playerThrustScript.RemoveFromThrustOutside(removeThrustAmmount, true);
        playerDragScript.allowVariableDrag = false;
        playerRigidbody.drag = linearDrag;
    }

    // Turns off hover mode
    public void DisableHoverMode()
    {
        playerGravityScript.allowGravity = true;
        playerThrustScript.ResetThrust();
        playerDragScript.allowVariableDrag = true;
        hoverEnabled = false;
    }
}
