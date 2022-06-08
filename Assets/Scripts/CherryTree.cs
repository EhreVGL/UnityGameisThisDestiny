using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryTree : MonoBehaviour
{
    [HideInInspector] public bool trigger;
    private int health;
    private float cherryDropTimer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        health = 2;
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (health < 0)
        {

            health = 2;
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
        if (trigger)
        {
            DropCherry();
        }
    }

    private void DropCherry()
    {
        if(health == 2)
        {
            cherryDropTimer -= 0.1f;
            if (cherryDropTimer > 0)
            {
                transform.GetChild(0).transform.position -= new Vector3(0, 0.1f, 0);
            }
            else
            {
                transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                transform.GetChild(0).transform.position += new Vector3(0, 1f, 0);
                cherryDropTimer = 1f;
                health--;
                trigger = false;
            }
        }
        else if (health == 1)
        {
            cherryDropTimer -= 0.1f;
            if (cherryDropTimer > 0)
            {
                transform.GetChild(1).transform.position -= new Vector3(0, 0.1f, 0);
            }
            else
            {
                transform.GetChild(1).GetComponent<Renderer>().enabled = false;
                transform.GetChild(1).transform.position += new Vector3(0, 1f, 0);
                cherryDropTimer = 1f;
                health--;
                trigger = false;
            }
        }
        else
        {
            cherryDropTimer -= 0.1f;
            if (cherryDropTimer > 0)
            {
                transform.GetChild(2).transform.position -= new Vector3(0, 0.1f, 0);
            }
            else
            {
                transform.GetChild(2).GetComponent<Renderer>().enabled = false;
                transform.GetChild(2).transform.position += new Vector3(0, 1f, 0);
                cherryDropTimer = 1f;
                health--;
                trigger = false;
            }
        }
    }
}
