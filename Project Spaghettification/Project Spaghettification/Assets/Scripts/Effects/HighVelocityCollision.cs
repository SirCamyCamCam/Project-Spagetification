using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighVelocityCollision : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public Rigidbody2D targetRigidbody;
    public Health healthScript;
    public ShieldController shieldControllerScript;
    public IncreaseDrag increaseDragScript;
    [Header("Variables")]
    public float collisionSpeed = 200;
    public int collisionDamage = 80;
    public bool increasedDragOnGroundCollision = true;
    public float dragMax = 6.0f;
    public float dragIncreaseRate = 0.5f;
    public float dragDecreaseRate = 0.1f;
    public float dragTime = 3.0f;
    public bool hasShield = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" && targetRigidbody.velocity.y <= -collisionSpeed)       // Checks for ground collision and velocity
        {
            if(hasShield == true && shieldControllerScript.shieldStatus == false)       // If there is a shield, check thats it's off
            {
                DoDamage(collisionDamage);      // Do damage
            }
            else if(hasShield == false)     // If it doesn't have a shield
            {
                DoDamage(collisionDamage);      // Do damage
            }
            if(increasedDragOnGroundCollision == true)        // Check if it should increase drag
            {
                increaseTheDrag(dragTime, dragMax, dragIncreaseRate, dragDecreaseRate);       // Increases the drag
            }
        }
    }

    // Calls the increase drag function
    private void increaseTheDrag(float sec, float drag, float rateIncrease, float rateDescrease)
    {
        increaseDragScript.enabled = true;
        increaseDragScript.IncreaseTheDrag(sec, drag, rateIncrease, rateDescrease);
    }

    // Calls the damage script
    private void DoDamage(int damage)
    {
        healthScript.RemoveHealth(damage);
    }
}
