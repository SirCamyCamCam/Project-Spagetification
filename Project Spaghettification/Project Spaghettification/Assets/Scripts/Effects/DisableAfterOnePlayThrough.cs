using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterOnePlayThrough : MonoBehaviour {

    // Global Variables
    // Public
    public SpriteRenderer objectSprite;
    public Animator objectAnimator;
    public Transform objectTransfrom;
    public GameObject objectGameObject;
    public bool playerOnStart = false;
    // Use this for initialization
    private float waitTime;
    private float delay = 0.0f;

	void Start () {
        waitTime = objectAnimator.GetCurrentAnimatorStateInfo(0).length;
        if(playerOnStart == true)
        {
            PlayAnimation();
        }
	}

    // Plays animation when called
    public void PlayAnimation()
    {
        objectTransfrom.parent = null;
        objectTransfrom.rotation = Quaternion.Euler(0, 0, 0);
        objectAnimator.enabled = true;
        objectAnimator.Play(0);
        objectSprite.enabled = true;
        StartCoroutine(DisableObject());
    }

    // Disables animation after time
    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(waitTime + delay);
        Destroy(objectGameObject);
    }
}
