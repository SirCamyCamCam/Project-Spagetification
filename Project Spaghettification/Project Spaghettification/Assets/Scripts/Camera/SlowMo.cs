using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour {

    // Global Variables
    // Public
    public SlowMo slowMoScript;
    public MonoBehaviour motionBlur;
    public float slowTimeSpeed = 0.5f;
    public float slowTimeRate = 0.02f;
    public float slowTimeTime = 1.5f;
    // Private
    private float timeSpeed = 1;
    private bool slowTime = false;
    private bool updateTime = false;
	
	// Update is called once per frame
	void Update () {
        if (updateTime == true)     // If we should be chaning time
        {
            SetTime();              // Update the current timestep
            motionBlur.enabled = true;      // Turn on motion blur
            if (slowTime == true)        // If we shold slow time
            {
                if (timeSpeed > slowTimeSpeed)      // If time is greater than slow speed
                {
                    timeSpeed -= slowTimeRate;      // Remove by rate
                }
                else
                {  
                    timeSpeed = slowTimeSpeed;      // Set to slow speed
                }
            }
            else             // If we should not be slowing time
            {
                if (timeSpeed < 1)          // If less than normal speed
                {
                    timeSpeed += slowTimeRate;      // add to time
                }
                else
                {
                    timeSpeed = 1;      // Set to normal speed
                    updateTime = false;     // Disable updating time
                    motionBlur.enabled = false;     // Turn off motion blur
                }
            }
        }
	}

    // Sets the time step
    private void SetTime()
    {
        Time.timeScale = timeSpeed;                 // Update time to timeSpeed
        Time.fixedDeltaTime = timeSpeed * 0.02f;        // Update physics to timeSpeed
    }

    // Checks for enter collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")    // If collision with player
        {
            if (slowTime == false)      // If slowmo isn't already enabled
            {
                slowTime = true;        // Enable slow time
                updateTime = true;
                StartCoroutine(ReturnToNormalSpeed(slowTimeTime));
            }
            else
            {
                StopCoroutine(ReturnToNormalSpeed(slowTimeTime));       // Reset timer
                StartCoroutine(ReturnToNormalSpeed(slowTimeTime));
            }
        }
    }

    // Checks for exit collisions
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")        // If collision with player
        {
            slowTime = false;       // Disable slow time
            StopCoroutine(ReturnToNormalSpeed(slowTimeTime));
        }
    }

    // Timer to reset slow speed
    private IEnumerator ReturnToNormalSpeed(float sec)
    {
        yield return new WaitForSeconds(sec);
        slowTime = false;
    }

    // Calls to turn on slow mo
    public void TurnOnSlowMo(float sec)
    {
        if (slowTime == false)      // If slow mo is not already on
        {
            if (sec != 0)       // If we should stop after certain time
            {
                StartCoroutine(ReturnToNormalSpeed(sec));       // Call timer
            }
            slowTime = true;
            updateTime = true;
        }
        else
        {
            if (sec != 0)   // Make sure only stop after certain time
            {
                StopCoroutine(ReturnToNormalSpeed(sec));        // Reset timer
                StartCoroutine(ReturnToNormalSpeed(sec));
            }
        }
    }

    // Calls to turn off slow mo
    public void TurnOffSlowMo(float sec)
    {
        if(sec != 0)
        {
            StopCoroutine(ReturnToNormalSpeed(sec));
        }
        slowTime = false;
    }
}
