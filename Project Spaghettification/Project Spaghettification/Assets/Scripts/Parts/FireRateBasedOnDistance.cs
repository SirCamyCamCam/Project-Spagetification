using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBasedOnDistance : MonoBehaviour {

    // Global Variables
    // Public
    public EnemyShoot enemyShootScript;
    public Transform targetTransform;
    public Transform objectTransform;
    public float timeToUpdate = 0.2f;
    public float distance = 3;
    public float lowDistanceMax = 12;
    public float lowDistanceMin = 8;
    public float highDistanceMax = 8;
    public float highDistanceMin = 4;

    // Use this for initalization
    private void Start()
    {
        StartCoroutine(SetNewRates());
    }

    private IEnumerator SetNewRates()
    {
        yield return new WaitForSeconds(timeToUpdate);
        float distanceBetween = Vector2.Distance(targetTransform.position, objectTransform.position);
        if (distanceBetween < distance)
        {
            enemyShootScript.minRateOfFire = lowDistanceMin;
            enemyShootScript.maxRateOfFire = lowDistanceMax;
        }
        else
        {
            enemyShootScript.minRateOfFire = highDistanceMin;
            enemyShootScript.maxRateOfFire = highDistanceMax;
        }
        StartCoroutine(SetNewRates());
    }
}
