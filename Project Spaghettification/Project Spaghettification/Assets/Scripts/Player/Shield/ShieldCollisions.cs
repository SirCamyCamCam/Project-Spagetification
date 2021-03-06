using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollisions : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public PlayerUncontrollableSpin playerUncontrollableSpinScript;
    public EnemyUncontrollableSpin enemyUncontrollableSpinScript;
    public GameObject objectGameObject;
    public Rigidbody2D objectRigidbody;
    [Header("Variables")]
    public float shieldCollisionSpinSpeed = 20;
    public float shieldCollisionSpinTime = 0.75f;
    public float pushSpinSpeed = 10;
    public float pushSpinTime = 0.4f;
    public float force = 5000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Shield")
        {
            if(playerUncontrollableSpinScript != null)
            {
                playerUncontrollableSpinScript.MakePlayerSpin(shieldCollisionSpinSpeed, shieldCollisionSpinTime, false);
                //CameraShake.ShakeCamera(0);
                objectRigidbody.AddForce(Vector2.right * force * Time.deltaTime);
            }
            else if(enemyUncontrollableSpinScript != null)
            {
                enemyUncontrollableSpinScript.MakeEnemySpin(shieldCollisionSpinSpeed, shieldCollisionSpinTime, true);
                objectRigidbody.AddForce(Vector2.right * force * Time.deltaTime);
            }
        }
        if (collision.gameObject.tag == "Player" && objectGameObject.tag != collision.gameObject.tag)
        {
            collision.gameObject.GetComponent<PlayerUncontrollableSpin>().MakePlayerSpin(pushSpinSpeed, pushSpinTime, false);
        }
        if(collision.gameObject.tag == "Viper" && objectGameObject.tag != collision.gameObject.tag)
        {
            collision.gameObject.GetComponent<EnemyUncontrollableSpin>().MakeEnemySpin(pushSpinSpeed, pushSpinTime, true);
        }
    }
}
