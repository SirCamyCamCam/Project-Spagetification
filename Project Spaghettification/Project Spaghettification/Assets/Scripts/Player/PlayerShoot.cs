using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    // Global Variables
    // Public 
    [Header("Objects")]
    public ShieldController shieldScript;
    public Transform playerBulletPrefab;
    public Transform firePoint;
    public BulletSettings bulletSettingsScript;
    public ShotGunShot shotGunShotScript;
    public Transform targetRelfectBullets;
    public Transform targetProjectile;
    [Header("Variables")]
    public bool allowShooting = true;
    public float fireRate = 10.0f;
    public int maxBulletCooldown = 100;
    public int bulletCooldownRate = 1;
    [Header("Types - Ignore")]
    public bool shotgunShot = false;
    public int numShotGunShots = 0;
    public bool oneShot = false;
    // Private
    private bool bulletCoolDownDisable = false;
    private float timeToFire = 0;
    private int bulletCooldown;

	// Use this for initialization
	void Start () {
        targetProjectile = playerBulletPrefab;      // Set target bullets to player bullet originally
        targetRelfectBullets = transform;
        bulletCooldown = maxBulletCooldown;         // Set bullet cooldown to max
	}
	
	// Update is called once per frame
	void Update () {
		if(allowShooting == true)       // Enables and disables shooting
        {
            // Set bulletCoolDownDisable off and replace with bulletCooldown != 0 if automatic reuse and not have to wait till max to use again
            if((Input.GetButton("Shoot") || Input.GetAxis("ShootJoystick") > 0) && bulletCoolDownDisable == false && Time.time > timeToFire && shieldScript.shieldStatus == false)        // Check for input, bullet cooldown, and time to fire
            {
                timeToFire = Time.time + 1 / fireRate;      // Creates firerate
                ShootProjectile(targetProjectile, numShotGunShots);      // Shoot
                if(bulletCooldown > 0)      // If greater than 0
                {
                    bulletCooldown -= bulletCooldownRate;   // Subtract by rate
                }
                else
                {
                    bulletCooldown = 0;
                    bulletCoolDownDisable = true;       // Disable shooting
                }
            }
        }
        if((!Input.GetButton("Shoot") || Input.GetAxis("ShootJoystick") <= 0.0f) || allowShooting == false || shieldScript.shieldStatus == true)    // If no input or not allowed to shoot
        {
            if(bulletCooldown < maxBulletCooldown)      // If less than max, add
            {
                bulletCooldown += bulletCooldownRate;
            }
            else
            {
                bulletCooldown = maxBulletCooldown;     // Set to max
                bulletCoolDownDisable = false;      // Re-enable shooting
            }
        }
        if(shotgunShot == true && numShotGunShots <= 0)     // If no more shot gun shots left, then disable
        {
            shotgunShot = false;
            numShotGunShots = 0;
        }
	}

    // Shoots projectile
    private void ShootProjectile(Transform bullet, int numberOfShotGunShots)
    {
        if (shotgunShot == false)       // Normal bullets
        {
            Transform shotProjectile = Instantiate(bullet, firePoint.position, firePoint.rotation);     // Spawns bullet
            shotProjectile.GetComponent<BulletSettings>().SetVariables(bulletSettingsScript, targetRelfectBullets);     // Assign settings
            if(oneShot == true)                                 // For upgrades
            {
                oneShot = false;                                
                targetProjectile = playerBulletPrefab;          // Set projectile
            }
        }
        else if(shotgunShot == true && numShotGunShots > 0)
        {
            shotGunShotScript.ShootShotGunShot(bullet, bulletSettingsScript, numberOfShotGunShots, targetRelfectBullets);   // Call shot gun function
            numShotGunShots -= 1;       // Remove shots left
        }
    }
}
