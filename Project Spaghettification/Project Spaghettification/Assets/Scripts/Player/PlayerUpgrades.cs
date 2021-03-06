using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public Health playerHealth;
    public PlayerShoot playerShootScript;
    public BulletSettings playerBulletSettings;
    public Transform vaccuumBombPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "HealthUpgrade")         // Collision with health upgrade
        {
            playerHealth.AddHealth(collision.GetComponent<HealthUpgrade>().healthIncrease);       // Add health
            Destroy(collision.gameObject);      // Destroy
        }
        if(collision.gameObject.tag == "VaccuumBombUpgrade")        // Collision with vaccuum bomb upgrade
        {
            playerShootScript.oneShot = true;       // Set to true
            playerShootScript.targetProjectile = vaccuumBombPrefab;     // Assign new bullet to be shot
            Destroy(collision.gameObject);      // Destroy
        }
        if(collision.gameObject.tag == "FireBulletUpgrade")         // Collision with homing bullets upgrade
        {
            if (playerBulletSettings.onFire == false)       // If no fire bullets, turn them on
            { 
                playerBulletSettings.onFire = true;
                StartCoroutine(DisableFireBullets(10));     // !!!!!!!!!!!!! Needs variable instead of int 10
            }
            else        // Otherwise make kill timer reset
            {
                StopCoroutine(DisableFireBullets(1));
                StartCoroutine(DisableFireBullets(10));     // !!!!!!!!!!!! Same as above
            }
            Destroy(collision.gameObject);      // Destory
        }
        if(collision.gameObject.tag == "ShotGunUpgrade")        // Collision with shot gun upgrade
        {
            playerShootScript.shotgunShot = true;       // Enable shot fun shot
            playerShootScript.numShotGunShots += 5;     // Increase ammount of shots
            Destroy(collision.gameObject);      // Destroy
        }
        if(collision.gameObject.tag == "HomingUpgrade")     // Collision with homing upgrade
        {
            if(playerBulletSettings.homing == false)    // If bullets are not already homing
            {
                playerBulletSettings.homing = true;
                StartCoroutine(DisableHomingBullets(10));       // !!!!!!!!!! Needs variable
            }
            else         // Otherwise reset kill timer
            {
                StopCoroutine(DisableHomingBullets(10));
                StartCoroutine(DisableHomingBullets(10));       // !!!!!!!!!! same as above
            }
        }
    }

    // Disables fire bullets after x seconds
    private IEnumerator DisableFireBullets(int sec)
    {
        yield return new WaitForSeconds(sec);
        playerBulletSettings.onFire = false;
    }

    // Disables homing bullets after x seconds
    private IEnumerator DisableHomingBullets(int sec)
    {
        yield return new WaitForSeconds(sec);
        playerBulletSettings.homing = false;
    }
}
