using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoost : MonoBehaviour {

    // Global Varibles
    // Public
    public PlayerThrust playerThrustScript;
    public Slider boostSlider;
    public int ammout = 100;
    public float increaseRate = 10;
    public float increaseTime = 0.5f;
    public float inputTime = 0.1f;
    public int thrustIncreaseVal = 1000;
    public float boostTime = 1.0f;
    public bool instantThrust = true;
    public bool allowThrust = true;
    // Private
    private float current;
    private bool pressed = false;
    private Coroutine inputWaiter;

	// Use this for initialization
	void Start ()
    { 
        current = ammout;                   // Set ammount
    }

    // Increase the boost amount
    private IEnumerator IncreaseBoost()
    {
        current += increaseRate;                            // Increase rate
        yield return new WaitForSeconds(increaseTime);      // Wait
        if (current < ammout)                               // If less than, add again
        {
            StartCoroutine(IncreaseBoost());                // Recall function
        }
        else
        {
            current = ammout;                               // Reset current
        }
    }

    // Resets the wait time for another press
    private void WaitForInput()
    {
        if(pressed == false)
        {
            pressed = true;
            inputWaiter = StartCoroutine(ResetWaitTimer());
        }
        else
        {
            StopCoroutine(inputWaiter);
            inputWaiter = StartCoroutine(ResetWaitTimer());
        }
    }

    // Wait to reset for input
    private IEnumerator ResetWaitTimer()
    {
        yield return new WaitForSeconds(inputTime);
        pressed = false;
    }

    // Increase the players thrust
    private void increaseThrust()
    {
        playerThrustScript.AddToThrustOutside(thrustIncreaseVal, instantThrust);
        playerThrustScript.ForceThrust(true);
        current = 0;
        StartCoroutine(TurnOffBoostTimer());
    }

    // Turns boost off after certain time
    private IEnumerator TurnOffBoostTimer()
    {
        yield return new WaitForSeconds(boostTime);
        TurnOffBoost();
    }

    // Turns boost off
    private void TurnOffBoost()
    {
        playerThrustScript.RemoveFromThrustOutside(thrustIncreaseVal, instantThrust);
        playerThrustScript.ForceThrust(false);
    }
       
    // Returns the current value of boost
    private float CurrentValue()
    {
        return current;
    }

    // Updates once a frame
    private void Update()
    {
        if(allowThrust == true && (Input.GetButtonDown("Thrust") || Input.GetButtonDown("ThrustJoystick")) && current == ammout)
        {
            if (pressed == true)
            {
                increaseThrust();
            }
            else
            {
                WaitForInput();
            }
        }
        if (current == 0)
        {
            StartCoroutine(IncreaseBoost());
        }
        boostSlider.value = CurrentValue();
    }
}
