using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuzzleSlot : MonoBehaviour {

    // Global Variables
    // Public
    public Image keyImage;
    // Private

	// Use this for initialization
	void Start () {
        ToggleKey();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Enables and disables the key image
    public void ToggleKey()
    {
        keyImage.enabled = !keyImage.enabled;
    }
}
