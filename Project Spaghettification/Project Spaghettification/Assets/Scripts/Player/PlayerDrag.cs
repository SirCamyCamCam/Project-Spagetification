using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrag : MonoBehaviour {

    // Public
    [Header("Objects")]
    public Rigidbody2D playerRigidbody2D;
    [Header("Variables")]
    public float multiplier = 0.2f;
    public float maxDrag = 1f;
    public float maxSpeed = 20;
    public bool allowVariableDrag = true;
    private float maxDragVelocity = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerRigidbody2D.drag < 1 && allowVariableDrag == true)
        {
            playerRigidbody2D.drag = Mathf.Sqrt(playerRigidbody2D.velocity.magnitude * playerRigidbody2D.velocity.magnitude * playerRigidbody2D.velocity.magnitude) * multiplier;
            maxDragVelocity = playerRigidbody2D.velocity.magnitude;
        }
        else if(allowVariableDrag == true)
        {
            playerRigidbody2D.drag = maxDrag;
            if(playerRigidbody2D.velocity.magnitude < maxDragVelocity)
            {
                playerRigidbody2D.drag = 0.99f;
            }
        }
	}
}
