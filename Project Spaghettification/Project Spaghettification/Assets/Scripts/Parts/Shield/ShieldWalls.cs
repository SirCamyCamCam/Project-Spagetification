using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWalls : MonoBehaviour {

    // Global Variables
    // Public
    public Rigidbody2D objectRigidbody2D;
    public ShieldBounce shieldBounceScript;
    public float speed = 15f;
    // Private

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "LeftWallDetector" && objectRigidbody2D.velocity.x < speed)
        {
            shieldBounceScript.EnemyLeftBounce();
        }
        if(collision.gameObject.tag == "RightWallDetector" && objectRigidbody2D.velocity.x > speed)
        {
            shieldBounceScript.EnemyRightBounce();
        }
    }
}
