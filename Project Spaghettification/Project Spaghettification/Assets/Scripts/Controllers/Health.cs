using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    // Global Variables
    // Public
    public GameObject healthGameObject;
    public GlobalSpawer globalSpawerScript;
    public ParticlesOnDeath particlesOnDeathScript;
    public EnemyDeath enemyDeathScript;
    public Health healthScript;
    public PlayerDeath playerDeathScript;
    public float health = 100;
    // Private
    private float originalHealth;

	// Use this for initialization
	void Start () {
        originalHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
        {
            if(healthGameObject.tag == "Player")
            {
                playerDeathScript.PlayerDied();
                healthScript.enabled = false;
            }
            else if(healthGameObject.tag == "Viper")
            {
                globalSpawerScript.EnemyDied(healthGameObject.transform);
                enemyDeathScript.EnemyDied();
                healthScript.enabled = false;
            }
            else if(healthGameObject.tag == "Bee")
            {
                particlesOnDeathScript.TurnOnParticles();
            }
            else if(healthGameObject.tag == "Dragonfly")
            {
                globalSpawerScript.DragonflyDeath(healthGameObject.transform);
                Destroy(healthGameObject);
            }
            
        }
	}

    // Adds to health
    public void AddHealth(float ammount)
    {
        if ((health + ammount) >= originalHealth)
        {
            health = originalHealth;
        }
        else
        {
            health += ammount;
        }
    }

    // Removes from health
    public void RemoveHealth(float ammount)
    {
        health -= ammount;
    }

    // Calls the bee death 
    public void BeeDeath()
    {
        globalSpawerScript.BeeDeath(healthGameObject.transform);
        Destroy(healthGameObject);
    }
}
