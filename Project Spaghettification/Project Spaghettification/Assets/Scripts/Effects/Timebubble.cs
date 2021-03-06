using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timebubble : MonoBehaviour {

    // Global Variables
    // Public
    public Timebubble timebubbleScript;
    public float minTime = 0.4f;
    public float rate = 0.01f;
    // Private
    private bool inBubble = false;
    private float timeSpeed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(inBubble == true && Time.timeScale > minTime)
        {
            timeSpeed-= rate;
        }
        else if(inBubble == true && Time.timeScale < minTime)
        {
            timeSpeed = minTime;
        }
        if(inBubble == false && Time.timeScale < 1)
        {
            timeSpeed += rate;
        }
        else if(inBubble == false && Time.timeScale >= 1)
        {
            timeSpeed = 1;
            timebubbleScript.enabled = false;
        }
        Time.timeScale = timeSpeed;
        Time.fixedDeltaTime = timeSpeed * 0.02f;
	}

    // Checks for collision with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            timebubbleScript.enabled = true;
            inBubble = true;
        }
    }

    // checks for exit of player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inBubble = false;
        }
    }
}
