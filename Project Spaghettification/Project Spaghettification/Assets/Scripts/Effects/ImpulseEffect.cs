using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseEffect : MonoBehaviour {

    // Global Variables
    // Public
    public Transform objectTransform;
    public Transform largestRing;
    public float rate = -1f;
    // Private
    private Vector3 originalSize;

    // Called upon initalization
    private void Start()
    {
        originalSize = largestRing.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(objectTransform.localScale.x < 0 || objectTransform.localScale.y < 0)
        {
            objectTransform.localScale = originalSize;
        }
        objectTransform.localScale += new Vector3(rate, rate, 0);
    }
}
