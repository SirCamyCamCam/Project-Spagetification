using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUncontrollableSpin : MonoBehaviour {

    // Global Variables
    // Public
    public DisablePlayer disablePlayerScript;
    public Transform playerTransform;
    public PlayerUncontrollableSpin playerUncontrollableSpinScript;
    public Rigidbody2D playerRigidbody;
    // Private
    private bool stopSpinning = false;
    private bool spinning = false;
    private float speed;
    private Coroutine wait;

    private void Start()
    {
        playerUncontrollableSpinScript.enabled = false;
    }

    private void Update()
    {
        if(stopSpinning == true)        // If finished spinning disable
        {
            stopSpinning = false;
            spinning = false;
            playerUncontrollableSpinScript.enabled = false;     // Disable script on completetion
        }
        if(spinning == true)
        {
            if (playerRigidbody.velocity.x >= 0)
            {
                playerTransform.Rotate(Vector3.back * speed);       // Rotate
            }
            else
            {
                playerTransform.Rotate(Vector3.back * -speed);       // Rotate
            }
        }
    }

    // Makes player spin
    public void MakePlayerSpin(float rotateSpeed, float sec, bool ignoreShield)
    {
        playerUncontrollableSpinScript.enabled = true;
        if (spinning == false)      // If not already spinning
        {
            wait  = StartCoroutine(WaitToKillSpinningTimer(sec, ignoreShield));       // Call disable timer
            if (ignoreShield == false)
            {
                disablePlayerScript.DisableAllControls();       // disable controls
            }
            else
            {
                disablePlayerScript.DisableControlsWithoutShield();
            }
            spinning = true;
            speed = rotateSpeed;
        }
        else             // Reset timer
        {
            StopCoroutine(wait);
            wait = StartCoroutine(WaitToKillSpinningTimer(sec, ignoreShield));
        }
    }

    // Stops the spinning after sec seconds
    private IEnumerator WaitToKillSpinningTimer(float sec, bool ignoreShield)
    {
        yield return new WaitForSeconds(sec);
        stopSpinning = true;
        if (ignoreShield == false)
        {
            disablePlayerScript.EnableAllControls();
        }
        else
        {
            disablePlayerScript.AllowControlsWithoutShield();
        }
    }
}
