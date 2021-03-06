using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    // Global Variables
    // Public
    public GameObject objectGameObject;
    public SelfDestruct selfDestructScript;
	// Use this for initialization
	void Start () {
        selfDestructScript.enabled = false;
	}

    public void Delete(float sec)
    {
        StartCoroutine(WaitToDelete(sec));
    }

    private IEnumerator WaitToDelete(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(objectGameObject);
    }
}
