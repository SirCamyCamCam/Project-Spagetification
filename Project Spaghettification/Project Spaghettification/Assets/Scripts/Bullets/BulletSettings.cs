using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSettings : MonoBehaviour {

    [Header("Integers")]
    public int bulletSpeed = 300;
    public int maxBulletSpeed = 450;
    public int increaseSpeedVal = 50;
    public int knockback = 0;
    public int increaseKnockback = 1000;
    public int reflectCounter = 0;
    public int maxKnockback = 8000;
    [Header("Floats")]
    public float decaySpeed = 5.0f;
    public float spinSpeed = 3.0f;
    public float damage = 10.0f;
    public float maxDamage = 80.0f;
    public float increaseDamage = 10.0f;
    public float increaseSizeMultiplier = 1.0f;
    public float increaseSizeAmmount = 1.0f;
    public float maxIncreaseSizeVal = 4.0f;
    public float minReflectAngle = 135.0f;
    public float maxReflectAngle = 225.0f;
    [Header("Booleans")]
    public bool onFire = false;
    public bool homing = false;
    public bool homingOnReturn = false;
    public bool reflectedBullet = false;
    public bool enemyBullet = false;
    public bool playerBullet = false;
    public bool diesOnContact = false;
    [Header("Objects")]
    public Transform shotFromTransform;
    public Transform prevShotFromTransform;
    public Transform leadTargetTransform;
    public Transform lastLeadTargetTransform;

    // Compares old variables to new and sets them acordingly
    public void SetVariables(BulletSettings otherSettings, Transform targetThatShotTheBullet)
    {
        // Integers
        if (bulletSpeed != otherSettings.bulletSpeed)
        {
            bulletSpeed = otherSettings.bulletSpeed;
        }
        if(maxBulletSpeed != otherSettings.maxBulletSpeed)
        {
            maxBulletSpeed = otherSettings.maxBulletSpeed;
        }
        if(increaseSpeedVal != otherSettings.increaseSpeedVal)
        {
            increaseSpeedVal = otherSettings.increaseSpeedVal;
        }
        if (knockback != otherSettings.knockback)
        {
            knockback = otherSettings.knockback;
        }
        if(reflectCounter != otherSettings.reflectCounter)
        {
            reflectCounter = otherSettings.reflectCounter;
        }
        if (maxKnockback != otherSettings.maxKnockback)
        {
            maxKnockback = otherSettings.maxKnockback;
        }
        if(increaseKnockback != otherSettings.increaseKnockback)
        {
            increaseKnockback = otherSettings.increaseKnockback;
        }
        // Floats
        if (decaySpeed != otherSettings.decaySpeed)
        {
            decaySpeed = otherSettings.decaySpeed;
        }
        if (spinSpeed != otherSettings.spinSpeed)
        {
            spinSpeed = otherSettings.spinSpeed;
        }
        if (damage != otherSettings.damage)
        {
            damage = otherSettings.damage;
        }
        if(maxDamage != otherSettings.maxDamage)
        {
            maxDamage = otherSettings.maxDamage;
        }
        if (increaseDamage != otherSettings.increaseDamage)
        {
            increaseDamage = otherSettings.increaseDamage;
        }
        if (increaseSizeMultiplier != otherSettings.increaseSizeMultiplier)
        {
            increaseSizeMultiplier = otherSettings.increaseSizeMultiplier;
        }
        if (increaseSizeAmmount != otherSettings.increaseSizeAmmount)
        {
            increaseSizeAmmount = otherSettings.increaseSizeAmmount;
        }
        if(maxIncreaseSizeVal != otherSettings.maxIncreaseSizeVal)
        {
            maxIncreaseSizeVal = otherSettings.maxIncreaseSizeVal;
        }
        if(minReflectAngle != otherSettings.minReflectAngle)
        {
            minReflectAngle = otherSettings.minReflectAngle;
        }
        if(maxReflectAngle != otherSettings.maxReflectAngle)
        {
            maxReflectAngle = otherSettings.maxReflectAngle;
        }
        // Booleans
        if (onFire != otherSettings.onFire)
        {
            onFire = otherSettings.onFire;
        }
        if (homing != otherSettings.homing)
        {
            homing = otherSettings.homing;
        }
        if (homingOnReturn != otherSettings.homingOnReturn)
        {
            homingOnReturn = otherSettings.homingOnReturn;
        }
        if (reflectedBullet != otherSettings.reflectedBullet)
        {
            reflectedBullet = otherSettings.reflectedBullet;
        }
        if (enemyBullet != otherSettings.enemyBullet)
        {
            enemyBullet = otherSettings.enemyBullet;
        }
        if (targetThatShotTheBullet.name == "Player")
        {
            playerBullet = true;
        }
        else
        {
            playerBullet = false;
        }
        if(diesOnContact != otherSettings.diesOnContact)
        {
            diesOnContact = otherSettings.diesOnContact;
        }
        prevShotFromTransform = shotFromTransform;
        shotFromTransform = targetThatShotTheBullet;
        lastLeadTargetTransform = leadTargetTransform;
        leadTargetTransform = otherSettings.leadTargetTransform;
    }
}
