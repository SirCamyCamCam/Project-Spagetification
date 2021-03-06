using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public ParticleSystem fireParticlesSystem;
    public ParticleSystem.EmissionModule fireParticles;
    public Health healthScript;
    public GameObject parentGameObject;
    public Fire fireScript;
    [Header("Variables")]
    public int seconds = 15;
    public int damageFrequency = 60;    // Higher number = less frequent, every x frames
    public bool takesDamage = true;
    public float damage = 5.0f;
    public bool deleteOnNoParticles = false;
    public bool indefinite = false;
    public bool contagious = true;
    //Private
    private bool didEmit = false;
    private bool onFire = false;
    private int currentFrame = 0;

	// Use this for initialization
	void Start () {
        fireParticles.enabled = false;       // Turn off particles on start
        fireScript.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		if(didEmit == true)     // After fire particles have stopped
        {
            if (fireParticlesSystem.particleCount == 0)   //  When we have no more particles, delete
            {
                if(deleteOnNoParticles == true)
                {
                    Destroy(parentGameObject);      // Delete parent
                }
                Destroy(gameObject);        // Self destruction
            }
        }
        if(onFire == true && takesDamage == true)      // If the object is on fire and takes damage
        {
            if(currentFrame % damageFrequency == 0)     // Every x frames
            {
                healthScript.RemoveHealth(damage);      // Call damage function
            }
        }
	}
    // Turns on the fireParticles
    public void TurnOnFire()
    {
        fireParticles.enabled = true;        // Turn on fire particles
        if (indefinite == false)
        {
            StartCoroutine(KillEmissionRate(seconds));      // Call kill emission 
        }
    }
    // Turns off fire after x seconds
    public IEnumerator KillEmissionRate(int sec)
    {
        yield return new WaitForSeconds(sec);       // wait sec seconds
        StopEmission();     // Kill emission
    }
    // Stops emitting fire particles
    private void StopEmission()
    {
        fireParticles.enabled = false;       // Disables emission
        didEmit = true;     // Finsihed emitting
    }

    // Checks for collision with other on fire things
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (contagious == true && collision.gameObject.GetComponent<Fire>().onFire == true)     // Or use "FireParticles" tag
        {
            TurnOnFire();
        }
    }
}
