using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingShieldDisableVisual : MonoBehaviour {

    // Global Variables
    // Public
    public ShieldController shieldControllerScript;
    public TouchingDisableShield touchingDisableShieldScript;
    public float timeOffGroundEnable = 0.3f;
    public string[] tags;
    public bool shouldDisable = false;
    // Private
    private int tagLength = 0;
    private int i = 0;
    private Coroutine leftTheGround = null;

    private void Start()
    {
        tagLength = tags.Length;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (i = 0; i < tagLength; i++)
        {
            if (collision.gameObject.tag == tags[i])
            {
                leftTheGround = StartCoroutine(LeftGround());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(i = 0; i < tagLength; i++)
        {
            if(collision.gameObject.tag == tags[i])
            {
                shieldControllerScript.allowShield = false;
                shouldDisable = true;
                if(leftTheGround != null)
                {
                    StopCoroutine(leftTheGround);
                }
            }
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (shieldControllerScript.allowShield == true)
        {
            for (i = 0; i < tagLength; i++)
            {
                if (collision.gameObject.tag == tags[i])
                {
                    shieldControllerScript.allowShield = false;
                    shouldDisable = true;
                    if (leftTheGround != null)
                    {
                        StopCoroutine(leftTheGround);
                    }
                }
            }
        }
    }*/

    private IEnumerator LeftGround()
    {
        yield return new WaitForSeconds(timeOffGroundEnable);
        shouldDisable = true;
        if (touchingDisableShieldScript.shouldDisable == false)
        {
            shieldControllerScript.allowShield = true;
        }
        leftTheGround = null;
    }
}
