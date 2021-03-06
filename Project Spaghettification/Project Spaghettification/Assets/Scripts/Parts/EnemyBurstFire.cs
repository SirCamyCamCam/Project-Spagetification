using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBurstFire : MonoBehaviour {

    // Global Vairables
    // Public
    public EnemyShoot enemyShootScript;
    public int shotsPerFire = 3;
    public float shotWaitTime = 0.5f;
    public int fireRate = 15;
    // Private
    private int currentShot = 0;
    private float timeToFire = 0;
    private bool calledOnce = false;
	
	// Update is called once per frame
	void Update () {
		if(Time.time > timeToFire && currentShot < shotsPerFire)     // If it'es time ot fire and we can fire
        {
            timeToFire = Time.time + 1 / fireRate;          // Set time
            enemyShootScript.ShootProjectile();         // Shoot projectile
            currentShot++;          // Add
        }
        else
        {
            if(calledOnce == false)     // If we haven;t called yet
            {
                calledOnce = true;
                StartCoroutine(WaitToShoot());
            }
        }
	}

    private IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(shotWaitTime);
        currentShot = 0;
    }
}
