using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHome : MonoBehaviour
{
    [HideInInspector] public bool trigger;
    [SerializeField] private GameObject Slime;
    private int health;
    private float timer = 30f;
    private float animation_timer = 4f;
    private float mushroomDrop_timer = 1f;
    private Vector3 rotation;
    private bool spawnSlime;
    // Start is called before the first frame update
    void Start()
    {
        health = 2;
        trigger = false;
        rotation = Vector3.zero;
        spawnSlime = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {

        if (health < 0)
        {
            GetComponent<Collider2D>().enabled = false;
            foreach(Transform child in transform)
            {
                if(child.tag != "Slime")
                {
                    child.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                health = 2;
                timer = 30f;
                spawnSlime = false;
                GetComponent<Collider2D>().enabled = true;
                foreach (Transform child in transform)
                {
                    child.gameObject.GetComponent<Renderer>().enabled = true;
                    if(child.tag == "Slime")
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
            else if(timer <= 15)
            {
                foreach (Transform child in transform)
                {
                    if (child.tag == "Slime")
                    {
                        Destroy(child.gameObject);
                    }
                }
                transform.GetChild(4).GetComponent<Renderer>().enabled = true;
                transform.GetChild(3).GetComponent<Renderer>().enabled = true;
            }
        }
        if (trigger == true)
        {
            DropMushroom();
        }
    }

    private void DropMushroom()
    {
        if(health == 2)
        {
            animation_timer -= 0.1f;
            if (animation_timer > 2)
            {
                if (animation_timer > 3)
                {
                    
                    transform.GetChild(0).transform.Rotate(new Vector3(0, 0, 1), 3f);
                    transform.GetChild(1).transform.Rotate(new Vector3(0, 0, 1), 3f);

                }
                else
                {
                    transform.GetChild(0).transform.Rotate(new Vector3(0, 0, 1), -3f);
                    transform.GetChild(1).transform.Rotate(new Vector3(0, 0, 1), -3f);
                }

            }
            else if (animation_timer > 0)
            {
                if (animation_timer > 1)
                {
                    transform.GetChild(0).transform.Rotate(new Vector3(0, 0, 1), -3f);
                    transform.GetChild(1).transform.Rotate(new Vector3(0, 0, 1), -3f);
                }
                else
                {
                    transform.GetChild(0).transform.Rotate(new Vector3(0, 0, 1), 3f);
                    transform.GetChild(1).transform.Rotate(new Vector3(0, 0, 1), 3f);
                }
            }
            else
            {
                mushroomDrop_timer -= 0.1f;
                if (mushroomDrop_timer > 0)
                {
                    transform.GetChild(0).transform.position -= new Vector3(0, 0.1f, 0);
                    transform.GetChild(1).transform.position -= new Vector3(0, 0.1f, 0);
                }
                else
                {
                    transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                    transform.GetChild(1).GetComponent<Renderer>().enabled = false;
                    transform.GetChild(0).transform.position += new Vector3(0, 1f, 0);
                    transform.GetChild(1).transform.position += new Vector3(0, 1f, 0);
                    mushroomDrop_timer = 1f;
                    health--;
                    trigger = false;
                    animation_timer = 2f;
                    rotation.z = 0;
                }
            }
        }
        else if (health == 1)
        {
            animation_timer -= 0.1f;

            if (animation_timer > 2)
            {
                if (animation_timer > 3)
                {
                    transform.GetChild(2).transform.Rotate(new Vector3(0, 0, 1), 3f);
                    transform.GetChild(3).transform.Rotate(new Vector3(0, 0, 1), 3f);

                }
                else
                {
                    transform.GetChild(2).transform.Rotate(new Vector3(0, 0, 1), -3f);
                    transform.GetChild(3).transform.Rotate(new Vector3(0, 0, 1), -3f);
                }

            }
            else if (animation_timer > 0)
            {
                if (animation_timer > 1)
                {
                    transform.GetChild(2).transform.Rotate(new Vector3(0, 0, 1), -3f);
                    transform.GetChild(3).transform.Rotate(new Vector3(0, 0, 1), -3f);
                }
                else
                {
                    transform.GetChild(2).transform.Rotate(new Vector3(0, 0, 1), 3f);
                    transform.GetChild(3).transform.Rotate(new Vector3(0, 0, 1), 3f);
                }
            }
            else
            {
                mushroomDrop_timer -= 0.1f;
                if(mushroomDrop_timer > 0)
                {
                    transform.GetChild(2).transform.position -= new Vector3(0, 0.1f, 0);
                    transform.GetChild(3).transform.position -= new Vector3(0, 0.1f, 0);
                }
                else
                {
                    transform.GetChild(2).GetComponent<Renderer>().enabled = false;
                    transform.GetChild(3).GetComponent<Renderer>().enabled = false;
                    transform.GetChild(2).transform.position += new Vector3(0, 1f, 0);
                    transform.GetChild(3).transform.position += new Vector3(0, 1f, 0);
                    mushroomDrop_timer = 1f;
                    health--;
                    trigger = false;
                    animation_timer = 2f;
                    rotation.z = 0;
                }

            }
        }
        else
        {
            animation_timer -= 0.1f;

            if (animation_timer > 2)
            {
                if (animation_timer > 3)
                {
                    transform.GetChild(4).transform.Rotate(new Vector3(0, 0, 1), 3f);

                }
                else
                {
                    transform.GetChild(4).transform.Rotate(new Vector3(0, 0, 1), -3f);
                }

            }
            else if (animation_timer > 0)
            {
                if (animation_timer > 1)
                {
                    transform.GetChild(4).transform.Rotate(new Vector3(0, 0, 1), -3f);
                }
                else
                {
                    transform.GetChild(4).transform.Rotate(new Vector3(0, 0, 1), 3f);
                }
            }
            else
            {
                mushroomDrop_timer -= 0.1f;
                if (mushroomDrop_timer > 0)
                {
                    transform.GetChild(4).transform.position -= new Vector3(0, 0.1f, 0);
                }
                else
                {
                    transform.GetChild(4).GetComponent<Renderer>().enabled = false;
                    transform.GetChild(4).transform.position += new Vector3(0, 1f, 0);
                    mushroomDrop_timer = 1f;
                    health--;
                    trigger = false;
                    animation_timer = 2f;
                    rotation.z = 0;
                    DropSlime();
                }
            }
        }
    }
    private void DropSlime()
    {
        if(spawnSlime == false)
        {
            spawnSlime = true;
            GameObject SlimeClone = Instantiate(Slime, transform.position, transform.rotation);
            Rigidbody2D rbSlime = SlimeClone.GetComponent<Rigidbody2D>();
            rbSlime.velocity = new Vector2(Random.Range(-2, 2), Random.Range(10, 20));
            SlimeClone.transform.SetParent(this.gameObject.transform);
        }
    }
}
