using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnCollision : MonoBehaviour {

    // Global Variables
    // Public
    public string target;
    public float damage = 1.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == target)
        {
            collision.gameObject.GetComponent<Health>().RemoveHealth(damage);
        }
    }
}
