using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUncontrollableSpin : MonoBehaviour {

    // Global Variables
    // Public
    public DisableEnemyControls disableEnemyControlsScript;
    public Transform objectTransform;
    public Rigidbody2D objectRigidbody2D;
    public EnemyUncontrollableSpin enemyUncontrollableSpinScript;
    // Private
    private bool stopSpinning = false;
    private bool spinning = false;
    private float speed = 0;
    private Coroutine wait;

	// Use this for initialization
	void Start () {
        enemyUncontrollableSpinScript.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (stopSpinning == true)        // If finished spinning disable
        {
            stopSpinning = false;
            spinning = false;
            enemyUncontrollableSpinScript.enabled = false;     // Disable script on completetion
        }
        if (spinning == true)
        {
            if (objectRigidbody2D.velocity.x >= 0)
            {
                objectTransform.Rotate(Vector3.back * speed);       // Rotate
            }
            else
            {
                objectTransform.Rotate(Vector3.back * -speed);       // Rotate
            }
        }
    }

    // Makes player spin
    public void MakeEnemySpin(float rotateSpeed, float sec, bool includeShield)
    {
        if (spinning == false)      // If not already spinning
        {
            wait = StartCoroutine(WaitToKillSpinningTimer(sec, includeShield));       // Call disable timer
            disableEnemyControlsScript.DisableControls(includeShield);
            spinning = true;
            speed = rotateSpeed;
            enemyUncontrollableSpinScript.enabled = true;
        }
        else             // Reset timer
        {
            StopCoroutine(wait);
            wait = StartCoroutine(WaitToKillSpinningTimer(sec, includeShield));
        }
    }

    // Stops the spinning after sec seconds
    private IEnumerator WaitToKillSpinningTimer(float sec, bool includeShield)
    {
        yield return new WaitForSeconds(sec);
        stopSpinning = true;
        disableEnemyControlsScript.ReEnableControls(includeShield);
    }
}
