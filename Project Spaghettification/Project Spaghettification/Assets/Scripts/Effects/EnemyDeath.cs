using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    // Global Variables
    // Public
    public EnemyUncontrollableSpin enemyUncontrollableSpinScript;
    public DisableEnemyControls disableEnemyControlsScript;
    public AvoidGround avoidGroundScript;
    public Collider2D physicalCollider;
    public EnemyDeath enemyDeathScript;
    public ParticleSystem smokeTrail;
    public GameObject objectGameObject;
    public SpriteRenderer objectSprite;
    public Rigidbody2D objectrigidbody2D;
    public PlayerGravity playerGravityScript;
    public Transform explosionTransform;
    public DisableAfterOnePlayThrough explosionAnimationScript;
    // Private
    private bool enemyDeath = false;
    private ParticleSystem.EmissionModule particleEmittor;

	// Use this for initialization
	void Start () {
        enemyDeathScript.enabled = false;
        particleEmittor = smokeTrail.emission;
	}
	
	// Update is called once per frame
	void Update () {
		if(smokeTrail.particleCount == 0)
        {
            Destroy(objectGameObject);
        }
	}

    // For when the enemy dies
    public void EnemyDied()
    {
        physicalCollider.enabled = false;
        enemyUncontrollableSpinScript.MakeEnemySpin(10, 10000, true);
        disableEnemyControlsScript.DisableControls(true);
        enemyDeath = true;
        objectGameObject.tag = "DeadViper";
        playerGravityScript.maximumGravity = 4;
        Instantiate(explosionTransform, objectGameObject.transform.position, objectGameObject.transform.rotation);
        avoidGroundScript.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyDeath == true && (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "FloatingIsland"))
        {
            objectSprite.enabled = false;
            enemyDeathScript.enabled = true;
            playerGravityScript.enabled = false;
            objectrigidbody2D.simulated = false;
            particleEmittor.rateOverDistance = 0;
            particleEmittor.enabled = false;
            disableEnemyControlsScript.DisableControls(true);
            //objectGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            //explosionAnimationScript.PlayAnimation();
        }
    }
}
