using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    private float cherry_death_timer;
    private bool DontDestroy;
    // Start is called before the first frame update
    void Start()
    {
        cherry_death_timer = 15f;
        DontDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        cherry_death_timer -= Time.fixedDeltaTime;
        if(cherry_death_timer <= 0)
        {
            if (DontDestroy == false)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Skull")
        {
            DontDestroy = true;
        }
    }
}
