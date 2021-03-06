using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGroundDamage : MonoBehaviour {

    // Global Variables
    // Public
    public Health healthScript;
    public float damage = 1f;
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            healthScript.RemoveHealth(damage);
        }
    }
}
