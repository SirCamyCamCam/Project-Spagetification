using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour {

    // Global Variables
    // Public
    public GameObject objectTransform;
    [Header("Variables")]
    public int healthIncrease = 50;


	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    /*void Update () {
        /*float player1Distance = Vector3.Distance(player1.transform.position, transform.position);
        float player2Distance = Vector3.Distance(player2.transform.position, transform.position);
        if(player1Distance < 70 && player2Distance < 70)
        {
            if (player1Distance < player2Distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player1.transform.position, speed * Time.deltaTime);
                lastTarget = player1;
                if (speed < 100)
                {
                    speed += 2;
                }
                else
                {
                    speed = 100;
                }
            }
            else if(player2Distance < player1Distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player2.transform.position, speed * Time.deltaTime);
                lastTarget = player2;
                if (speed < 100)
                {
                    speed += 2;
                }
                else
                {
                    speed = 100;
                }
            }
        }
        else if(player1Distance < 70 && player2Distance > 70)
        {
            transform.position = Vector3.MoveTowards(transform.position, player1.transform.position, speed * Time.deltaTime);
            lastTarget = player1;
            if (speed < 100)
            {
                speed += 2;
            }
            else
            {
                speed = 100;
            }
        }
        else if(player2Distance < 70 && player1Distance > 70)
        {
            transform.position = Vector3.MoveTowards(transform.position, player2.transform.position, speed * Time.deltaTime);
            lastTarget = player2;
            if (speed < 100)
            {
                speed += 2;
            }
            else
            {
                speed = 100;
            }
        }
        else
        {
            if(speed > 0)
            {
                speed -= 1;
                transform.position = Vector3.MoveTowards(transform.position, lastTarget.transform.position, speed * Time.deltaTime);
            }
            else
            {
                speed = 0;
            }
        }*/
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().health += healthIncrease;
            Destroy(objectTransform);
        }
        if (collision.gameObject.tag == "PlayerVisual")
        {
            collision.gameObject.GetComponentInParent<Health>().AddHealth(healthIncrease);
            Destroy(objectTransform);
        }
    }
}
