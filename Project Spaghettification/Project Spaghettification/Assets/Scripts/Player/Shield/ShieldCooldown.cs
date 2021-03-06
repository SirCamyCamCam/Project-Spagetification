using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCooldown : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public ShieldController shieldControllerScript;
    public SpriteRenderer shieldSprite;
    public PlayerUncontrollableSpin playerUncontrollableSpinScript;
    [Header("Variables")]
    public int shieldRate = 3;
    public float shieldDisableTime = 4.5f;
    public float cooldownSpinSpeed = 0f;
    public float cooldownSpinTime = 0.5f;
    [Header("Enemy")]
    public EnemyUncontrollableSpin enemyUncontrollableSpinScript;
    // Private
    private int frameCounter = 0;
    private int changeColorOnFrame = 3;
    private int colorMultiplyer = 255;
    private bool callRenableOnce = false;
    private float shieldBlinkTime = 0.1f;
    private int shiledBlinkIterations = 0;

    private void Update()
    {
        if(shieldControllerScript.shieldStatus == true)     // If we are shielding
        {  
            if(colorMultiplyer > 0)     // If the color is greater than 0
            {
                colorMultiplyer -= shieldRate;      // Subtract by rate
            }
            else
            {
                colorMultiplyer = 0;        // Otherwise set to 0
            }
        }
        else
        {
            if(colorMultiplyer < 255)       // If color is less than 255
            {
                colorMultiplyer += shieldRate;      // Increase by rate 
            }
            else
            {
                colorMultiplyer = 255;      // Set to max
            }
        }
        if(frameCounter % changeColorOnFrame == 0)      // Every 3 frames
        {
            shieldSprite.color = new Color32(0, (byte)colorMultiplyer, 0, 255);       // Apply color
            frameCounter = 0;
        }
        frameCounter += 1;      // Add every frame

        if(colorMultiplyer == 0 && callRenableOnce == false)        // If shield is at 0 and has not been called to disable
        {
            shieldControllerScript.allowShield = false;     // Disable use
            shieldControllerScript.TurnOffShield();
            shieldControllerScript.ForceOff(true);
            callRenableOnce = true;                         // Call only once
            StartCoroutine(RenableShield(shieldDisableTime));       // Call renable timer
            StartCoroutine(BlinkShield(shieldBlinkTime));       // Blink shield
            if (playerUncontrollableSpinScript != null)
            {
                playerUncontrollableSpinScript.MakePlayerSpin(cooldownSpinSpeed, cooldownSpinTime, true);
            }
            else if(enemyUncontrollableSpinScript != null)
            {
                enemyUncontrollableSpinScript.MakeEnemySpin(cooldownSpinSpeed, cooldownSpinTime, false);
            }
        }
    }

    // Blinks he shield
    private IEnumerator BlinkShield(float sec)
    {
        if(shiledBlinkIterations == 6)      // If we have blinked 6 times
        {
            shieldSprite.enabled = false;       // Disable visual
            shiledBlinkIterations = 0;          // Reset
            yield break;        // Exit BlinkShield(sec)
        }
        yield return new WaitForSeconds(sec);       // Wait
        if(shieldSprite.enabled == true)        // If enabled
        {   
            shieldSprite.enabled = false;       // Disable
        }
        else
        {
            shieldSprite.enabled = true;        // Else enable
        }
        shiledBlinkIterations++;        // Increment
        StartCoroutine(BlinkShield(sec));       // Recursion
    }

    // Renables the shield after sec seconds
    private IEnumerator RenableShield(float sec)
    {
        yield return new WaitForSeconds(sec);
        shieldControllerScript.allowShield = true;
        shieldControllerScript.ForceOff(false);
        callRenableOnce = false;
        colorMultiplyer = 255;
    }
}
