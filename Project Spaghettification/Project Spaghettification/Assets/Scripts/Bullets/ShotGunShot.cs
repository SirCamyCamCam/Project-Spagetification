using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShot : MonoBehaviour {

    // Global Variable
    // Public
    public Transform[] firePoints;
    public ShotGunShot shotGunShotScript;
    // Private

    private void Start()
    {
        shotGunShotScript.enabled = false;
    }

    public void ShootShotGunShot(Transform bullet, BulletSettings bulletSettings, int numberOfShots, Transform shotTheBullet)
    {
        int i;
        for(i = 0; i < numberOfShots; i++)
        {
            Transform shotProjectile = Instantiate(bullet, firePoints[i].position, firePoints[i].rotation);
            shotProjectile.GetComponent<BulletSettings>().SetVariables(bulletSettings, shotTheBullet);
        }
    }
}
