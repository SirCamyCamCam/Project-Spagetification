using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePlayer : MonoBehaviour {

    // Global Variables
    // Public
    public Transform targetTransform;
    public Transform objectTransform;
    public PlayerFreeze playerFreezeScript;
    public float seconds = 5.0f;        // 0 For forever
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTransform.tag)
        {
            playerFreezeScript.enabled = true;
            playerFreezeScript.FreezeThePlayer(objectTransform, seconds);
        }
    }
}
