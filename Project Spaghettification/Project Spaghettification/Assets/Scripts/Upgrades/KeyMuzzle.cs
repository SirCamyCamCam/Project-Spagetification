using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMuzzle : MonoBehaviour {

    // Global Variables
    // Public
    public MuzzleSlot muzzleSlotScript;
    public KeyMuzzle keyMuzzleScript;
    public ShieldController shieldControllerScript;
    public Transform playerTransform;
    public Rigidbody2D playerRigidbody;
    public Rigidbody2D keyRigidbody;
    public SpriteRenderer keySprite;
    public Collider2D keyCollider;
    public Transform keyTransform;
    // Private
    private bool keyEnabled = false;
    private bool pickupEnabled = true;
    private float reEnableTime = 0.5f;
    private float launchForce = 500f;
    private float originalGravity;
    
	// Use this for initialization
	void Start () {
        //muzzleSlotScript = GameObject.Find("MuzzleSlot").GetComponent<MuzzleSlot>();
        keyMuzzleScript = this;
        keyMuzzleScript.enabled = false;
        originalGravity = keyRigidbody.gravityScale;
	}

    // Update is called once per frame
    void Update()
    {
        if(keyEnabled == true && shieldControllerScript.shieldStatus == true)
        {
            keyEnabled = false;
            DisableKey();
        }
    }

    // Collisions with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pickupEnabled == true && collision.gameObject.tag == "Player")
        { 
            EnableKey();
            // Assign shield controller script and player transform
            if(shieldControllerScript == null)
            {
                shieldControllerScript = collision.gameObject.GetComponentInChildren<ShieldController>();
            }
            if(playerTransform == null)
            {
                playerTransform = collision.gameObject.transform;
            }
            if(playerRigidbody == null)
            {
                playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            }
        }
        if(pickupEnabled == true && collision.gameObject.tag == "PlayerVisual")
        {
            EnableKey();
            // Assign shield controller script and player transform
            if (shieldControllerScript == null)
            {
                shieldControllerScript = collision.gameObject.transform.parent.GetComponentInChildren<ShieldController>();
            }
            if(playerTransform == null)
            {
                playerTransform = collision.gameObject.transform.parent;
            }
            if(playerRigidbody == null)
            {
                playerRigidbody = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            }
        }
    }

    // Called when the key is picked up
    private void EnableKey()
    {
        muzzleSlotScript.ToggleKey();
        keyMuzzleScript.enabled = true;
        keyEnabled = true;
        keySprite.enabled = false;
        keyCollider.enabled = false;
        keyRigidbody.gravityScale = 0;
        pickupEnabled = false;
    }

    // Called when the key is dropped
    private void DisableKey()
    {
        muzzleSlotScript.ToggleKey();
        StartCoroutine(ReEnablePickup());
        keyTransform.position = playerTransform.position;
        keyRigidbody.velocity = playerRigidbody.velocity;
        keySprite.enabled = true;
        keyRigidbody.AddForce(-playerTransform.right * launchForce);
    }

    // Timer to reenable key
    private IEnumerator ReEnablePickup()
    {
        yield return new WaitForSeconds(reEnableTime);
        keyEnabled = false;
        keyCollider.enabled = true;
        keyRigidbody.gravityScale = originalGravity;
        pickupEnabled = true;
    }
}
