using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnDeath : MonoBehaviour {

    // Global Variables
    public Health healthScript;
    public ParticleSystem particles;
    public ParticlesOnDeath particlesOnDeathScript;
    public SpriteRenderer sprite;
    public Collider2D objectCollider;
    // Private
    private ParticleSystem.EmissionModule emissionModule;
	// Use this for initialization
	void Start () {
        particlesOnDeathScript.enabled = false;
        particles.Pause();
        emissionModule = particles.emission;
        emissionModule.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(particles.particleCount == 0)
        {
            healthScript.BeeDeath();
            particlesOnDeathScript.enabled = false;
        }
	}

    // Turns on particles
    public void TurnOnParticles()
    {
        emissionModule.enabled = true;
        particles.Play();
        particlesOnDeathScript.enabled = true;
        sprite.enabled = false;
        objectCollider.enabled = false;
    }
}
