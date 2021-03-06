using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour {

    // Global Variables
    // Public
    public DisablePlayer disablePlayerScript;
    public Transform playerTransform;
    public PlayerFreeze playerFreezeScript;
    // Private
    private bool freezePlayerAtPoint;
    private Transform freezeTransform;
	
	// Update is called once per frame
	void Update () {
		if(freezePlayerAtPoint == true)     // If the player shold be frozen
        {
            playerTransform.position = freezeTransform.position;
        }
	}

    // Freezes the player
    public void FreezeThePlayer(Transform position, float sec)
    {
        disablePlayerScript.DisableAllControls();       // Disables player controls
        freezePlayerAtPoint = true;
        freezeTransform = position;
        if(sec != 0)        // If it should unfreeze after sec seconds
        {
            StartCoroutine(UnFreezePlayerTimer(sec));
        }
    }

    // Unfreezes the player and disables script
    public void UnFreezeThePlayer()
    {
        disablePlayerScript.EnableAllControls();
        freezePlayerAtPoint = false;
        playerFreezeScript.enabled = false;
    }

    // Timer to unfreeze player
    private IEnumerator UnFreezePlayerTimer (float sec)
    {
        yield return new WaitForSeconds(sec);
        UnFreezeThePlayer();
    }
}
