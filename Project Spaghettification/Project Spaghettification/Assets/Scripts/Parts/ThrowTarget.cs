using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTarget : MonoBehaviour {

    // Global Variables
    // Public
    public AreaEffector2D launchAreaEffector;
    public ThrowTarget throwTargetScript;

    private void Start()
    {
        launchAreaEffector.enabled = false;
        throwTargetScript.enabled = false;
    }

    public void Throw(float thrust, float sec)
    {
        launchAreaEffector.enabled = true;
        launchAreaEffector.forceMagnitude = thrust;
        StartCoroutine(TurnOffAreaEffector(sec));
    }

    private IEnumerator TurnOffAreaEffector(float sec)
    {
        yield return new WaitForSeconds(sec);
        launchAreaEffector.enabled = false;
        throwTargetScript.enabled = false;
    }
}
