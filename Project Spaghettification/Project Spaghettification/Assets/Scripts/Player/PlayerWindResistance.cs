using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindResistance : MonoBehaviour {

    // Global Variable
    // Public
    [Header("Objects")]
    public Rigidbody2D playerRigidbody;
    public PlayerThrust playerThrustScript;
    public PlayerTurn playerTurnScript;
    // Turning script
    [Header("Variables")]
    public int minimumSpeed = 100;
    public float windResistance = 6.0f; 
    public float decreaseRate = 2.0f;
    public float increaseRate = 0.4f;
    public float originaLinearDrag = 0.5f;
    public float waitInputTime = 0.4f;
    public float dragTime = 0.6f;
    // Private
    private bool disableDragForInput = false;
    private bool callDisableDragForInputOnce = false;
    private bool callStopDragOnce = false;
    private bool stopDrag = false;
    private bool dragReset = true;

	// Use this for initialization
	void Start () {
        playerRigidbody.drag = originaLinearDrag;   // Assign orginal drag
        if(playerThrustScript == null)      // Check for null
        {
            Debug.Log("Missing playerThrustScript in PlayerWindResistance at: " + transform);
        }
        if(playerRigidbody == null)     // Check for null
        {
            Debug.Log("Missing playerRigidbody in PlayerWindResistence at: " + transform);
        }
        if(playerTurnScript == null)    // Check for null
        {
            Debug.Log("Missing playerTurnScript in PlayerWindResistence at: " + transform);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Left Wind Resistance
        if ((transform.rotation.eulerAngles.z > 135 && transform.rotation.eulerAngles.z < 225) && playerRigidbody.velocity.x < -minimumSpeed && playerThrustScript.isThrusting == false)    // Checks for anlges, speed, and is thrusting
        {
            if (stopDrag == false)
            {
                DisableIfThereIsInput();    // Check for holding down input and if so disable
                AddDrag();
                dragReset = false;      // Drag is not static
            }
            else
            {
                RemoveDrag();   // Remove drag until normal
            }
        }
        // Right Wind Resistance
        else if ((transform.rotation.eulerAngles.z < 45 || transform.rotation.eulerAngles.z > 315) && playerRigidbody.velocity.x > minimumSpeed && playerThrustScript.isThrusting == false)     // Checks for angles, speed, and is thrusting
        {
            if (stopDrag == false)
            {
                DisableIfThereIsInput();        // Check for holding down input and if so disable
                AddDrag();
                dragReset = false;      // Drag is not static at original
            }
            else
            {
                RemoveDrag();      // Remove drag until normal
            }
        }
        else
        {
            if (dragReset == false)     // If drag is not static at original drag
            {
                RemoveDrag();
            }
        }

        // Reset disable drag after thrusted again
        if (disableDragForInput == true && playerThrustScript.isThrusting == true)    
        {
            disableDragForInput = false;
        }
    }

    // Waits for key input and resets if not released
    private IEnumerator WaitForNoInput(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        disableDragForInput = true;
        callDisableDragForInputOnce = false;
        playerRigidbody.drag = originaLinearDrag;
    }

    // If there is a turning input, disable drag
    private void DisableIfThereIsInput()
    {
        if (playerTurnScript.isRotating == true && disableDragForInput == false)       // Checks if rotating and wind resisteace is not yet disabled
        {
            if (callDisableDragForInputOnce == false)      // Calls Cotoutine once
            {
                callDisableDragForInputOnce = true;
                StartCoroutine(WaitForNoInput(waitInputTime));      // Wait this ammount of time while turning to disable
            }
        }
        else
        {
            callDisableDragForInputOnce = false;
            StopCoroutine(WaitForNoInput(waitInputTime));   // Disables called coroutine when no turning input
        }
    }

    // Adds drag to the player
    private void AddDrag()
    {
        if (playerRigidbody.drag < windResistance && disableDragForInput == false)      // If less than reistance and not disabled
        {
            playerRigidbody.drag += increaseRate;
            if (callStopDragOnce == false)      // If not already called once
            {
                callStopDragOnce = true;
                StartCoroutine(StopDrag(dragTime));     // Calls disable drag
            }
        }
        else if(playerRigidbody.drag > windResistance && disableDragForInput == false)      // Don't go over max wind resistance
        {
            playerRigidbody.drag = windResistance;
        }
    }

    // Timer to stop drag
    private IEnumerator StopDrag(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        stopDrag = true;
    }

    // Removes drag from player
    private void RemoveDrag()
    {
        if (playerRigidbody.drag > originaLinearDrag)   // If greater than original
        {
            playerRigidbody.drag -= decreaseRate;
        }
        else
        {
            playerRigidbody.drag = originaLinearDrag;   // Set to original
            dragReset = true;   // Drag static to original again
            stopDrag = false;   // Drag reset
        }
    }
}
