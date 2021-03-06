using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    // Global Variables
    // Public
    public Health healthScript;
    public Slider healthBarSlider;
    // Private
    private float currentHealth = 0;
    private float maxHealth = 0;
	
    private void Start()
    {
        maxHealth = healthScript.health;
        currentHealth = maxHealth;
    }

	// Update is called once per frame
	void Update () {
        currentHealth = healthScript.health;
        healthBarSlider.value = CurrentHealth();
	}

    // GEts the current health
    private float CurrentHealth()
    {
        return currentHealth / maxHealth;
    }
}
