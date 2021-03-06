using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {

    // Global Variables
    // Public
    public Canvas deathCanvas;
    public PlayerUncontrollableSpin playerUncontrollableSpinScript;
    public DisablePlayer disablePlayerScript;
    public PlayerDeath playerDeathScript;
    public Rigidbody2D playerRigidbody;
    public GlobalSpawer globalSpawerScript;
    public Collider2D physicalCollider;
    public LeadInfrontTarget leadPlayer;
    public Transform explosionTransform;
    public Transform playerTransform;
    public DisableAfterOnePlayThrough explosionAnimationScript;
    public float respawnTime = 1;
    // Private
    private bool isPlayerDead = false;

	// Use this for initialization
	void Start () {
        deathCanvas.enabled = false;
        playerDeathScript.enabled = false;
	}

    // Is run once a frame
    private void Update()
    {
        if(Input.anyKeyDown)
        {
            Application.LoadLevel(0);
        }
    }

    // Called when the player dies
    public void PlayerDied()
    {
        deathCanvas.enabled = true;
        playerUncontrollableSpinScript.MakePlayerSpin(10, 100000, false);
        FindEnemies();
        isPlayerDead = true;
        physicalCollider.enabled = false;
        leadPlayer.multiplyer = 0.1f;
        StartCoroutine(WaitToRespawn());
        Instantiate(explosionTransform, playerTransform.position, playerTransform.rotation);
        //CameraShake.ShakeCamera(0);
    }

    // Waits to enableb respawn
    private IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        playerDeathScript.enabled = true;
    }

    // Finds types of gameobjects
    public void FindEnemies()
    {
        if(globalSpawerScript.recentSpawn == "Viper")
        {
            GameObject[] vipers = GameObject.FindGameObjectsWithTag("Viper");
            int i, viperLen = vipers.Length;
            for(i = 0; i < viperLen; i++)
            {
                vipers[i].GetComponent<AimWithRandomness>().playerDead = true;
            }
        }
        else if(globalSpawerScript.recentSpawn == "Bee")
        {
            GameObject[] bees = GameObject.FindGameObjectsWithTag("Bee");
            int i, beeLen = bees.Length;
            for(i = 0; i < beeLen; i++)
            {
                bees[i].GetComponent<AimAtTarget>().playerDead = true;
            }
        }
        else if(globalSpawerScript.recentSpawn == "Dragonfly")
        {
            GameObject[] dragonflies = GameObject.FindGameObjectsWithTag("Dragonfly");
            int i, draLen = dragonflies.Length;
            for(i = 0; i < draLen; i++)
            {
                dragonflies[i].GetComponent<MoveRelativeToPlayer>().playerDead = true;
            }
        }
    }
    
    // Checks for collision with the ground
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPlayerDead == true && collision.gameObject.tag == "Ground")
        {
            playerRigidbody.simulated = false;
            playerUncontrollableSpinScript.enabled = false;
            disablePlayerScript.DisableAllControls();
            playerDeathScript.enabled = true;
            explosionAnimationScript.PlayAnimation();
        }
    }
}
