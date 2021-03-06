using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  ------  Uknown Author - Modified by Cameron Carstens
public class CameraShake : MonoBehaviour {

    // Global Variables
    // Public
    public MonoBehaviour motionBLur;
    public Transform cameraTransform;
    public float shake_decay;
    public float shake_intensity;
    public float ShakeLength = 0.007f;
    // Private
    private Quaternion originRotation;
    private bool fixPos = false;
    private bool allowCameraShake = false;


    // Update is called once per frame
    void Update()
    {
        if (allowCameraShake == true)
        {
            if (shake_intensity > 0)
            {
                if (motionBLur.enabled == false)
                {
                    cameraTransform.rotation = new Quaternion(
                    originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .20f,
                    originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .20f,
                    originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
                    originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
                    shake_intensity -= shake_decay;
                    fixPos = true;
                }
            }
            else
            {
                if (fixPos == true)
                {
                    cameraTransform.rotation = Quaternion.Euler(0, 0, 0);
                    fixPos = false;
                    allowCameraShake = false;
                }
            }
        }
    }

    public void ShakeCamera(float intensity)
    {
        originRotation = Quaternion.Euler(0, 0, 0);
        if (intensity == 0)
        {
            shake_intensity = 0.2f;
        }
        else
        {
            shake_intensity = intensity;
        }
        shake_decay = ShakeLength;
        allowCameraShake = true;
    }
}
