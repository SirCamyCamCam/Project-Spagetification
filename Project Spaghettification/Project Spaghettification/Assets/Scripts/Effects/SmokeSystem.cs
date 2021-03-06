using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSystem : MonoBehaviour {

    // Global Variables
    // Public
    public Health healthScript;
    public ParticleSystem smokeParticleSystemy;
    public float checkHealthRate = 1;
    public int health1 = 50;
    public int health2 = 30;
    public int health3 = 20;
    public int health4 = 10;
    public int rate1 = 4;
    public int rate2 = 10;
    public int rate3 = 15;
    public int rate4 = 20;
    // Private
    private ParticleSystem.EmissionModule smokeParticleSystem;

	// Use this for initialization
	void Start () {
        smokeParticleSystem = smokeParticleSystemy.emission;
        smokeParticleSystem.rateOverDistance = 0;
        CheckHealth();
	}
	
    // Waits to check health again
	private IEnumerator WaitToCheckHealth()
    {
        yield return new WaitForSeconds(checkHealthRate);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(healthScript.health <= health1 && healthScript.health > health2)
        {
            smokeParticleSystem.rateOverDistance = rate1;
        }
        else if(healthScript.health <= health2 && healthScript.health > health3)
        {
            smokeParticleSystem.rateOverDistance = rate2;
        }
        else if(healthScript.health <= health3 && healthScript.health > health4)
        {
            smokeParticleSystem.rateOverDistance = rate3;
        }
        else if(healthScript.health <= health4)
        {
            smokeParticleSystem.rateOverDistance = rate4;
        }
        else
        {
            smokeParticleSystem.rateOverDistance = 0;
        }
        StartCoroutine(WaitToCheckHealth());
    }
}
