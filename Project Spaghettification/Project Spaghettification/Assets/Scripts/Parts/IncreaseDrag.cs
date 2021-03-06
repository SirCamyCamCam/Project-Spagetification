using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDrag : MonoBehaviour {

    // Global Variables
    // Public
    public IncreaseDrag increaseDragScript;
    public Rigidbody2D targetRigidbody;
    // Private
    private bool stopDrag = false;
    private bool increaseTheDragy = false;
    private float dragMax = 0;
    private float dragIncreaseRate = 0;
    private float dragDecreaseRate = 0;
    private float originialDrag = 0;

    private void Start()
    {
        increaseDragScript.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		if(stopDrag == true)    // If we should stop the drag
        {
            if(targetRigidbody.drag > originialDrag)        // If drag is less than the original
            {
                targetRigidbody.drag -= dragDecreaseRate;       // Remove by rate
            }
            else
            {
                targetRigidbody.drag = originialDrag;       // Set to original drag
                stopDrag = false;       // Stop drag
                increaseDragScript.enabled = false;     // Disable script
            }
        }
        if (increaseTheDragy == true)       // If we should increase the drag
        {
            if(targetRigidbody.drag < dragMax)      // If drag is less than max
            {
                targetRigidbody.drag += dragIncreaseRate;       // Add drag
            }
            else
            {
                targetRigidbody.drag = dragMax;     // Else set drag to max
            }
        }
	}

    // Starts the increase drag function
    public void IncreaseTheDrag(float sec, float ammount, float rateIncrease, float rateDecrease)
    {
        StartCoroutine(StopDragTimer(sec));         // Call stop drag timer
        increaseTheDragy = true;        //  Start increasing drag
        originialDrag = targetRigidbody.drag;       // Find the original drag
        dragIncreaseRate = rateIncrease;        // Apply rate
        dragDecreaseRate = rateDecrease;        // Apply rate
        dragMax = ammount;      // Apply max
        increaseDragScript.enabled = true;
    }


    // Stops drag after sec seconds
    private IEnumerator StopDragTimer(float sec)
    {
        yield return new WaitForSeconds(sec);
        stopDrag = true;
        increaseTheDragy = false;
    }
}
