using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionExpansion : MonoBehaviour {

    // Global Variables
    // Public
    public Transform objectTransform;
    public float rate = 1f;
	
	// Update is called once per frame
	void Update () {
        objectTransform.localScale += new Vector3(rate, rate, 0);
	}
}
