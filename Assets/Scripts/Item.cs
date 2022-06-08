using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Vector2 distance;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(player.name == "PlayerOutside")
            {
                Player = player;
            }
        }
        distance = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        distance.x = Player.transform.position.x - transform.position.x;
        distance.y = Player.transform.position.y - transform.position.y;
        if(distance.x > 0)
        {
            if ((distance.x < 15 && distance.x > 0) && Mathf.Abs(distance.y) < 15)
            {
                rb.velocity += new Vector2(Time.fixedDeltaTime * 10, Time.fixedDeltaTime * 5);
            }
        }
        else if(distance.x > -15)
        {
            if (Mathf.Abs(distance.x) < 6 && Mathf.Abs(distance.y) < 6)
            {
                rb.velocity += new Vector2(Time.fixedDeltaTime * -10, Time.fixedDeltaTime * 5);
            }
        }

    }
}
