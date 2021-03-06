using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTimer : MonoBehaviour {

    // Global Variables
    // Public
    public float time = 0.75f;
    public GameObject explosionGameObject;
    public SpriteRenderer[] shockWaves;
    public Collider2D explosionCollider;
    public ParticleSystem fireParticles;
    public ExplosionTimer explosionTimerScript;

	// Use this for initialization
	void Start () {
        StartCoroutine(killExplosion());
        explosionTimerScript.enabled = false;
        //'CameraShake.ShakeCamera(0);
	}

    // Updeats once a frame
    private void Update()
    {
        if(fireParticles.particleCount == 0)
        {
            Destroy(explosionGameObject);
        }
    }

    // Stops explosion force
    private IEnumerator killExplosion()
    {
        yield return new WaitForSeconds(time);
        int i;
        for(i = 0; i < shockWaves.Length; i++)
        {
            shockWaves[i].enabled = false;
        }
        explosionCollider.enabled = false;
        explosionTimerScript.enabled = true;
    }
}
