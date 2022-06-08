using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [HideInInspector] public bool trigger;
    private int health;
    private float timer = 5f;
    private float animation_timer = 4f;
    private Vector3 rotation;
    [SerializeField] private GameObject DropItem;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        trigger = false;
        rotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(health < 0)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            timer -= Time.fixedDeltaTime;
            if(timer <= 0)
            {
                health = 5;
                timer = 5f;
                GetComponent<Renderer>().enabled = true;
                GetComponent<Collider2D>().enabled = true;
            }
        }
        if(trigger == true)
        {
            animation_timer -= 0.1f;

            if (animation_timer > 2)
            {
                if(animation_timer > 3)
                {
                    transform.Rotate(new Vector3(0, 0, 1), 0.1f);
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, 1), -0.1f);
                }

            }
            else if (animation_timer > 0)
            {
                if (animation_timer > 1)
                {
                    transform.Rotate(new Vector3(0, 0, 1), -0.1f);
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, 1), 0.1f);
                }
            }
            else
            {
                trigger = false;
                animation_timer = 2f;
                rotation.z = 0;
                DropWood();
            }

        }
    }

    private void DropWood()
    {
        health--;  
            
        GameObject dropItemClone = Instantiate(DropItem, transform.position + new Vector3(0,5,0), Quaternion.identity);
        dropItemClone.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2, 2), Random.Range(6, 12));
    }
}
