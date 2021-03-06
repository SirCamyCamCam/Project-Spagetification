using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    // Global Variables
    // Public
    [Header("Objects")]
    public SpriteRenderer shieldSprite;
    public Collider2D shieldCollider;
    public AreaEffector2D shieldAreaEffector;
    public Collider2D shieldAreaEffectorCollider;
    public ShieldBounce shieldBounceScript;
    [Header("Variables")]
    public bool shieldStatus = false;
    public bool allowShield = true;
    public bool enemy = false;
    [Header("Enemy")]
    public float rapidBulletsTime = 1f;
    public int shieldAfterBulletsNum = 4;
    public float normalChancePercentage = 50;
    public float shieldChancePercentage = 80;
    public float shieldTime = 1f;
    // Private
    private int bulletCount = 0;
    private Coroutine shieldDisableTimer;
    private bool forceShieldOff = false;
    private bool forceShieldOn = false;

    // Use this for initialization
    void Start()
    {
        TurnOffShield();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == false)
        {
            if (allowShield == true && (Input.GetButton("Shield") || Input.GetAxis("ShieldJoystick") < 0 || Input.GetMouseButton(1)))
            {
                TurnOnShield();
            }
            else if (allowShield == true && !(Input.GetButton("Shield") && Input.GetAxis("ShieldJoystick") >= 0 && Input.GetMouseButton(1)))
            {
                TurnOffShield();
            }
            else if (allowShield == false)
            {
                TurnOffShield();
            }
        }
        else
        {
            if(bulletCount >= shieldAfterBulletsNum)
            {
                EnemyShieldingSequence(shieldTime);
            }
        }
        if(forceShieldOff == true)
        {
            TurnOffShield();
        }
        if(forceShieldOn == true)
        {
            TurnOnShield();
        }
        if(allowShield == false)
        {
            TurnOffShield();
        }
    }

    // Turns the shield on
    public void TurnOnShield()
    {
        if (forceShieldOff == false)
        {
            shieldSprite.enabled = true;
            shieldCollider.enabled = true;
            shieldAreaEffector.enabled = true;
            shieldAreaEffectorCollider.enabled = true;
            shieldStatus = true;
            shieldBounceScript.allowRaycast = true;
        }
    }

    // Turns shield off
    public void TurnOffShield()
    {
        if (forceShieldOn == false)
        {
            shieldSprite.enabled = false;
            shieldCollider.enabled = false;
            shieldAreaEffector.enabled = false;
            shieldAreaEffectorCollider.enabled = false;
            shieldStatus = false;
            shieldBounceScript.allowRaycast = false;
        }
    }

    // Forces the shield to turn ans stay off
    public void ForceOff(bool status)
    {
        if(status == true)
        {
            forceShieldOff = true;
            forceShieldOn = false;
        }
        else
        {
            forceShieldOff = false;
        }
    }

    // Detected a bullet
    public void DetectedBullet(bool reflected)
    {
        if (reflected == true)                                   // If the bullet has been relfected, turn on shield 
        {
            RandomChanceToShield(shieldChancePercentage);
        }
        else
        {
            RandomChanceToShield(normalChancePercentage);
        }
        bulletCount++;
        StartCoroutine(ResetBulletCounter());
    }

    // Resets the bullet counter after rapidBulletsTime time
    private IEnumerator ResetBulletCounter()
    {
        yield return new WaitForSeconds(rapidBulletsTime);
        bulletCount = 0;
    }

    // Random chance to shield bullet
    private void RandomChanceToShield(float chance)
    {
        float random = Random.value * 100;
        if(random <= chance)
        {
            EnemyShieldingSequence(shieldTime);
        }
    }

    // Enemy shielding sequence
    private void EnemyShieldingSequence(float timeToShield)
    {
        if (allowShield == true)
        {
            if (shieldDisableTimer != null)
            {
                StopCoroutine(shieldDisableTimer);
            }
            TurnOnShield();
            shieldDisableTimer = StartCoroutine(TurnOffShieldAfterTime(timeToShield));
        }
    }

    // Turn off shield after timeToShield time
    private IEnumerator TurnOffShieldAfterTime(float timeToShield)
    {
        yield return new WaitForSeconds(timeToShield);
        TurnOffShield();
    }

    // Forces the shield to be on or off
    public void ForceShieldOn(bool on)
    {
        if(on == true)
        {
            forceShieldOn = true;
            forceShieldOff = false;
        }
        else
        {
            forceShieldOn = false;
            TurnOffShield();
        }
    }
}