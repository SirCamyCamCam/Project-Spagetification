using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {

    // Global Variables
    // Public
    [Header("Objects")]
    public BulletSettings bulletSettingsScript;
    public Fire fireScript;
    public TrailRenderer bulletTrail;
    public Transform bulletPrefab;
    public Collider2D bulletCollider;
    public Transform objectTransform;
    public Bullets bulletScript;
    // Private
    private bool callFireOnce = false;

    // Use this for initialization
    void Start () {
	    if(bulletSettingsScript.reflectedBullet == true)     // If the bullet is reflected, go to target                                             -- May need a knockbackAllow boolean
        {
            // Set Rotation
            /*if (bulletSettingsScript.leadTargetTransform != null)
            {
                TurnTowardTarget(bulletSettingsScript.leadTargetTransform);
            }
            else
            {*/
                TurnTowardTarget(bulletSettingsScript.prevShotFromTransform);
            //}
            // Set Knockback
            Rigidbody2D knockbackRigidbody = bulletSettingsScript.shotFromTransform.GetComponent<Rigidbody2D>();    //  Find rigidbody????????????????????????????????????
            if (bulletSettingsScript.knockback != 0 && knockbackRigidbody != null)      // Check for null
            {
                knockbackRigidbody.AddForce((bulletSettingsScript.shotFromTransform.position - transform.position) * bulletSettingsScript.knockback);        // Apply knockback
            }
            if(bulletSettingsScript.shotFromTransform.tag == "Player")
            {
                bulletSettingsScript.playerBullet = true;
            }
            else
            {
                bulletSettingsScript.playerBullet = false;
            }
        }
        if(bulletSettingsScript.onFire == true && callFireOnce == false)      // If on fire
        {
            fireScript.TurnOnFire();
            callFireOnce = true;
        }
        if(bulletSettingsScript.reflectCounter >= 3)        // If more than 3 reflections
        {
            GameObject.Find("Main Camera").GetComponent<CameraShake>().ShakeCamera(0); // INEFFECIENT!!!!
        }
        transform.localScale = new Vector3(transform.localScale.x * (bulletSettingsScript.increaseSizeMultiplier), transform.localScale.y * (bulletSettingsScript.increaseSizeMultiplier), transform.localScale.z);      // Increase the size
        if (bulletSettingsScript.playerBullet == false)
        {
            objectTransform.tag = "EnemyBullet";
            bulletTrail.startColor = Color.yellow;
        }
        else
        {
            objectTransform.tag = "PlayerBullet";
            bulletTrail.startColor = Color.blue;
        }
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.right * Time.deltaTime * bulletSettingsScript.bulletSpeed);      // Move bullets
        if(bulletSettingsScript.homing == false)     // Destroy over time if not homing
        {
            Destroy(gameObject, bulletSettingsScript.decaySpeed);
        }
        else if(bulletSettingsScript.homing == true)     // If the bullet is homing
        {
            //Homing(shotTarget);     // !!!!!! Needs different target
        }
        if (bulletSettingsScript.onFire == true && callFireOnce == false)        // If on fire
        {
            fireScript.TurnOnFire();
            if (bulletTrail != null)        // Disable trail rendered when on fire
            {
                bulletTrail.enabled = false;
            }
        }
        if(bulletTrail.isVisible == false)
        {
            //Destroy(objectTransform.gameObject);
        }
    }

    // Checks for trigger collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Shield" && bulletSettingsScript.diesOnContact == false && bulletSettingsScript.shotFromTransform != collision.transform.parent)    // Collision with a shield that creates reflected bullet
        {
            CreateReflectBullet(collision.gameObject.transform.parent);     // Create a reflect bullet
        }
        if(collision.gameObject.tag == "Shield" && bulletSettingsScript.diesOnContact == true)    // Collision with a shield without reflected bullets
        {
            Destroy(gameObject);
        }
        if((collision.gameObject.tag == "Player") && bulletSettingsScript.shotFromTransform.tag != collision.transform.tag)        // Collision with the player
        {
            DiesOnContact(collision, true);
        }
        if ((collision.gameObject.tag == "PlayerVisual") && bulletSettingsScript.shotFromTransform.tag != collision.transform.parent.tag)        // Collision with the player
        {
            DiesOnContact(collision, true);
        }
        if (collision.gameObject.tag == "Tower" && bulletSettingsScript.diesOnContact == false)      // Collision with tower and can relfect
        {
            NonShieldRefelection(collision.gameObject.transform, collision);
        }
        if(collision.gameObject.tag == "Tower" && bulletSettingsScript.diesOnContact == true)       // Collision with tower and can't reflect
        {
            DiesOnContact(collision, true);
        }
        if(collision.gameObject.tag == "LeftWall" || collision.gameObject.tag == "RightWall" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "TopWall")      // Collision with walls
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Bee")
        {
            DiesOnContact(collision, false);
        }
        if(collision.gameObject.tag == "Dragonfly" && bulletSettingsScript.shotFromTransform.tag != collision.transform.tag)
        {
            DiesOnContact(collision, false);
        }
        if(collision.gameObject.tag == "Viper" && bulletSettingsScript.shotFromTransform.tag != collision.transform.tag && collision.gameObject.tag != "Shield")
        {
            DiesOnContact(collision, false);
        }
        if(collision.gameObject.tag == "FloatingIsland")
        {
            Destroy(gameObject);
        }
    }

    // For when bullets die on contact
    private void DiesOnContact(Collider2D collision, bool flamable)
    {
        bulletCollider.enabled = false;     // Disable the collider
        RemoveHealthFromTarget(collision.gameObject);       // Calls remove health
        if (flamable == true)
        {
            CanCatchFlame(collision.gameObject);        // Check for on fire
        }
        Destroy(gameObject);
    }

    // Checks if on fire and enables fire if so
    private void CanCatchFlame(GameObject target)
    {
        if(bulletSettingsScript.onFire == true)
        {
            target.GetComponent<Fire>().TurnOnFire();
        }
    }

    // Removes health
    private void RemoveHealthFromTarget(GameObject target)
    {
        target.GetComponentInParent<Health>().RemoveHealth(bulletSettingsScript.damage);  // Remove health
    }

    // Non-Shield reflection
    private void NonShieldRefelection(Transform collided, Collider2D collision)
    {
        bulletCollider.enabled = false;     // Disable the collider
        Transform reflected= Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(bulletSettingsScript.minReflectAngle, bulletSettingsScript.minReflectAngle)));      // Reflect and random angle variations
        reflected.GetComponent<BulletSettings>().SetVariables(bulletSettingsScript, collided);      // Asigns settings for new bullet
        RemoveHealthFromTarget(collision.gameObject);       // Calls remove health
        CanCatchFlame(collision.gameObject);        // Check for on fire
        Destroy(gameObject);
    }

    // Creates a reflected bullet
    private void CreateReflectBullet(Transform collided)
    {
        Transform reflectedBullet = Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 180));        // Spawn and flip 180
        bulletCollider.enabled = false;         // Disable the collide
        // Awlays true for reflected bullets
        bulletSettingsScript.reflectedBullet = true;    // Is a relfected bullet
        bulletSettingsScript.reflectCounter += 1;       // Increase relfect counter 
        // Damage
        if (bulletSettingsScript.damage < bulletSettingsScript.maxDamage)        // If damage is less than max damage, add damage
        {
            bulletSettingsScript.damage += bulletSettingsScript.increaseDamage;
        }
        else
        {
            bulletSettingsScript.damage = bulletSettingsScript.maxDamage;       // Otherwise set to max
        }
        // Speed
        if (bulletSettingsScript.bulletSpeed < bulletSettingsScript.maxBulletSpeed)         // If speed is less than max, increase
        {
            bulletSettingsScript.bulletSpeed += bulletSettingsScript.increaseSpeedVal;
        }
        else
        {
            bulletSettingsScript.bulletSpeed = bulletSettingsScript.maxBulletSpeed;
        }
        // Size
        if(bulletSettingsScript.increaseSizeMultiplier < bulletSettingsScript.maxIncreaseSizeVal)       // If increase size ammoutn is less than max
        {
            // !!!!!!!!!! Check player distances before adding
            bulletSettingsScript.increaseSizeMultiplier += bulletSettingsScript.increaseSizeAmmount;
        }
        else
        {
            bulletSettingsScript.increaseSizeMultiplier = bulletSettingsScript.maxIncreaseSizeVal;
        }
        // Knockback
        if((bulletSettingsScript.increaseSizeMultiplier == bulletSettingsScript.maxIncreaseSizeVal || bulletSettingsScript.reflectCounter > 3) && bulletSettingsScript.knockback < bulletSettingsScript.maxKnockback)       // If biggest size or relfected more than 3 times, increase knockback
        {
            bulletSettingsScript.knockback += bulletSettingsScript.increaseKnockback;
        }
        else
        {
            bulletSettingsScript.knockback = bulletSettingsScript.maxKnockback;
        }
        reflectedBullet.GetComponent<BulletSettings>().SetVariables(bulletSettingsScript, collided);        // Assign settings to new bullet
        Destroy(gameObject);
    }

    // Homing function
    public void Homing(Transform target)
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * bulletSettingsScript.spinSpeed);
    }

    // Turns toward tagrte after relfect
    private void TurnTowardTarget(Transform target)
    {
        Vector3 FacePlayer = target.transform.position - transform.position;
        float angle = Mathf.Atan2(FacePlayer.y, FacePlayer.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }
}
