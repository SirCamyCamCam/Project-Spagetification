using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public Transform bulletPrefab;
    public BulletSettings bulletSettingsScript;
    public ShotGunShot shotGunShotScript;
    public Transform firePoint;
    public Transform shooterTransform;
    public AimAtTarget aimAtTargetScript;
    public AimWithRandomness aimWithRandomnessScript;
    public ShieldController shieldControllerScript;
    [Header("Variables")]
    public bool allowShooting = true;
    public float rateOfFire = 10.0f;
    public float maxRateOfFire = 12.0f;
    public float minRateOfFire = 8.0f;
    public float newFireRateTime = 2.0f;
    public float maxFireRateTime = 3.0f;
    public float minFireRateTime = 1.0f;
    public bool useRandomFireRate = false;
    public bool useRandomFireTime = false;
    public bool playerWithinRange = false;
    public bool useAngle = false;
    public float minAngle = 30;
    public float maxAngle = 270;
    public bool useMaxOrMin = false;
    public bool useMaxAndMin = true;
    [Header("Types")]
    public bool shotgunShot = false;
    public int numShotGunShots = 5;
    // Private
    private bool forceShoot = false;
    private float timeToFire = 0;
    private Quaternion q;
    private bool usesShield = false;

	// Use this for initialization
	void Start () {
		if(useRandomFireRate == true)
        {
            StartCoroutine(newFireRate());
        }
        if(shieldControllerScript != null)
        {
            usesShield = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(aimAtTargetScript != null)
        {
            q = aimAtTargetScript.q;
        }
        if(aimWithRandomnessScript != null)
        {
            q = aimWithRandomnessScript.q;
        }
        if (usesShield == false)
        {
            if (useAngle == false && allowShooting == true && Time.time > timeToFire)      // Checks for allowShooting, time to fire, and not using angles
            {
                ShootProjectile();
            }
            else if (useAngle == true && allowShooting == true && useMaxAndMin == true && Time.time > timeToFire && q.eulerAngles.z > minAngle && q.eulerAngles.z < maxAngle)       // Checks for angles, allow shooting, time to fire, and UseMinANDMax
            {
                ShootProjectile();
            }
            else if (useAngle == true && allowShooting == true && useMaxOrMin == true && Time.time > timeToFire && (q.eulerAngles.z > maxAngle || q.eulerAngles.z < minAngle))      // Checks for angles, allow shooting, time to fire, and UseMinORmax
            {
                ShootProjectile();
            }
            if (forceShoot == true && Time.time > timeToFire)        // Turn on to force shooting
            {
                ShootProjectile();
            }
        }
        else if(shieldControllerScript.shieldStatus == false)
        {
            if (useAngle == false && allowShooting == true && Time.time > timeToFire)      // Checks for allowShooting, time to fire, and not using angles
            {
                ShootProjectile();
            }
            else if (useAngle == true && allowShooting == true && useMaxAndMin == true && Time.time > timeToFire && q.eulerAngles.z > minAngle && q.eulerAngles.z < maxAngle)       // Checks for angles, allow shooting, time to fire, and UseMinANDMax
            {
                ShootProjectile();
            }
            else if (useAngle == true && allowShooting == true && useMaxOrMin == true && Time.time > timeToFire && (q.eulerAngles.z > maxAngle || q.eulerAngles.z < minAngle))      // Checks for angles, allow shooting, time to fire, and UseMinORmax
            {
                ShootProjectile();
            }
            if (forceShoot == true && Time.time > timeToFire)        // Turn on to force shooting
            {
                ShootProjectile();
            }
        }
    }

    // Shoots the projectile
    public void ShootProjectile()
    {
        timeToFire = Time.time + 1 / rateOfFire;    // Update timeToFire
        if (shotgunShot == false)       // Not shotgun
        {
            Transform shotProjectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            shotProjectile.GetComponent<BulletSettings>().SetVariables(bulletSettingsScript, shooterTransform);
        }
        else if (shotgunShot == true)       // Use shot gun
        {
            shotGunShotScript.ShootShotGunShot(bulletPrefab, bulletSettingsScript, numShotGunShots, shooterTransform);
        }
    }

    // Forces to shoot
    public void ForceToShoot()
    {
        forceShoot = true;
        allowShooting = false;
    }
    
    // Disables Force shooting
    public void DisableForceShoot()
    {
        forceShoot = false;
        allowShooting = true;
    }

    // Sets a new rate of fire after seconds
    private IEnumerator newFireRate()
    {
        yield return new WaitForSeconds(newFireRateTime);                       // Waits
        rateOfFire = Random.Range(minRateOfFire, maxRateOfFire);                // Creates new fire rate
        if(useRandomFireTime == true)                       
        {
            newFireRateTime = Random.Range(minFireRateTime, maxFireRateTime);   // Creates new wait time
        }
        StartCoroutine(newFireRate());                                          // Calls upon itself
    }
}
