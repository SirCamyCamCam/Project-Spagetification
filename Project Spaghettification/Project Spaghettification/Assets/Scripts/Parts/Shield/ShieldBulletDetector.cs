using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBulletDetector : MonoBehaviour {

    // Global Variables
    // Public
    public ShieldController shielldController;
    public Transform objectTransform;
    // Private 
    private BulletSettings tempBulletSettingsScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet" && shielldController.allowShield == true)
        {
            tempBulletSettingsScript = collision.gameObject.GetComponent<BulletSettings>();
            if (tempBulletSettingsScript.shotFromTransform != objectTransform)
            {
                if(tempBulletSettingsScript.reflectedBullet == true)
                {
                    shielldController.DetectedBullet(true);
                }
                else
                {
                    shielldController.DetectedBullet(false);
                }
            }
        }
    }
}
