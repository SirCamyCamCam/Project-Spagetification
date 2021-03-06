using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {

    // Global Variables
    // Public 
    public SpriteRenderer flashSprite;
    public float seconds = 0.1f;
    public int flashes = 3;
    // Private 
    private int count = 0;
	
    // Flashes the muzzle
	public void FlashMuzzle()
    {
        StartCoroutine(TurnOffFlash());
    }

    // Enaables and disables the sprite
    private IEnumerator TurnOffFlash()
    {   
        count++;                           // Add
        if(count > flashes)                // If we have reached max
        {
            count = 0;                      // Reset
            yield break;
        }
        yield return new WaitForSeconds(seconds);           // Wait
        StartCoroutine(TurnOffFlash());                     // Repeat
    }
}
