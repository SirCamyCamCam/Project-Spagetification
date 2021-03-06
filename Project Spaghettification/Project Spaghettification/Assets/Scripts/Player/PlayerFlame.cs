using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlame : MonoBehaviour {

    public SpriteRenderer flameSprite;

	// Use this for initialization
	void Start () {
        flameSprite.enabled = false;
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

    public void EnableFlame()
    {
        flameSprite.enabled = true;
    }

    public void DisableFlame()
    {
        flameSprite.enabled = false;
    }
}
