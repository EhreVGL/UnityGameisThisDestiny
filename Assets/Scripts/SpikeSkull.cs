using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSkull : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject Rat;
    private int rat_count;
    private Vector2[] ratMovement = new Vector2[3];
    private Vector2[] ratNewPos = new Vector2[3];
    private bool[] ratRemove = new bool[3];
    private float ratSpeed = 1.0f;
    private bool findCherry;
    private bool goRatActive;
    private GameObject goRat;
    private Vector2 holeMovement;
    private bool searching;
    private float searching_timer;
    private GameObject cherry;
    private bool waitingRat;
    private float waitingRat_timer;
    // Start is called before the first frame update
    void Start()
    {
        rat_count = 0;
        for(int i = 0; i < 3; i++)
        {
            ratMovement[i] = Vector2.zero;
            ratRemove[i] = true;
            ratNewPos[i] = Vector2.zero;
        }
        findCherry = false;
        goRatActive = false;
        searching = false;
        searching_timer = 25.0f;
        waitingRat = false;
        waitingRat_timer = 20f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (rat_count == 0)
        {
            for(int i = 0; i < 3; i++)
            {
                ratRemove[i] = true;
            }
            SpawnRat();
        }

        MovementRat();

        if (findCherry)
        {
            GoRat();
        }

        if (searching)
        {
            SearchPlayer();
        }
        if (waitingRat)
        {
            WaitingPlayer();
        }
    }

    private void SpawnRat()
    {
        for(int i = rat_count; i < 3; i++)
        {
            GameObject RatClone = Instantiate(Rat, this.gameObject.transform.GetChild(0).transform.position, this.gameObject.transform.GetChild(0).transform.rotation);
            RatClone.transform.SetParent(this.gameObject.transform);
            rat_count++;
        }

    }

    private void MovementRat()
    {
        for(int i = 0; i < rat_count; i++)
        {   
            if (ratRemove[i] == true)
            {
                ratRemove[i] = false;
                ratNewPos[i].x = Random.Range(this.gameObject.transform.GetChild(3).transform.position.x, this.gameObject.transform.GetChild(2).transform.position.x);
                ratNewPos[i].y = Random.Range(this.gameObject.transform.GetChild(5).transform.position.y, this.gameObject.transform.GetChild(4).transform.position.y);
                ratMovement[i].x = ratNewPos[i].x - this.gameObject.transform.GetChild(6 + i).transform.position.x;
                ratMovement[i].y = ratNewPos[i].y - this.gameObject.transform.GetChild(6 + i).transform.position.y;
            }
        }

        for(int i = 0;i < rat_count; i++)
        {
            if ((int)((ratNewPos[i].x - this.gameObject.transform.GetChild(6 + i).transform.position.x)) == 0 && (int)((ratNewPos[i].y - this.gameObject.transform.GetChild(6 + i).transform.position.y)) == 0)
            {
                ratRemove[i] = true;
            }
            //+x, -x, +y, -y
            else
            {
                if ((int)((this.gameObject.transform.GetChild(2).transform.position.x - this.gameObject.transform.GetChild(6 + i).transform.position.x)) == 0)
                {
                    ratMovement[i].x = -1;
                    ratRemove[i] = true;
                }
                else if ((int)((this.gameObject.transform.GetChild(3).transform.position.x - this.gameObject.transform.GetChild(6 + i).transform.position.x)) == 0)
                {
                    ratMovement[i].x = 1;
                    ratRemove[i] = true;
                }
                else if ((int)((this.gameObject.transform.GetChild(4).transform.position.y - this.gameObject.transform.GetChild(6 + i).transform.position.y)) == 0)
                {
                    ratMovement[i].y = -1;
                    ratRemove[i] = true;
                }
                else if ((int)((this.gameObject.transform.GetChild(5).transform.position.y - this.gameObject.transform.GetChild(6 + i).transform.position.y)) == 0)
                {
                    ratMovement[i].x = 1;
                    ratRemove[i] = true;
                }
                else
                {
                    if (ratNewPos[i].x - this.gameObject.transform.GetChild(6 + i).transform.position.x == 0)
                    {
                        ratMovement[i].x = 0;
                    }
                    else
                    {
                        ratMovement[i].x = ratMovement[i].x / Mathf.Abs(ratMovement[i].x);
                    }
                    if (ratNewPos[i].y - this.gameObject.transform.GetChild(6 + i).transform.position.y == 0)
                    {
                        ratMovement[i].y = 0;
                    }
                    else
                    {
                        ratMovement[i].y = ratMovement[i].y / Mathf.Abs(ratMovement[i].y);
                    }
                }

                if (ratMovement[i].x != 0 && ratMovement[i].y != 0)
                {
                    this.gameObject.transform.GetChild(6 + i).gameObject.GetComponent<Rigidbody2D>().MovePosition(this.gameObject.transform.GetChild(6 + i).gameObject.GetComponent<Rigidbody2D>().position + ratMovement[i] * (ratSpeed / 0.835f) * Time.fixedDeltaTime);
                }
                else
                {
                    this.gameObject.transform.GetChild(6 + i).gameObject.GetComponent<Rigidbody2D>().MovePosition(this.gameObject.transform.GetChild(6 + i).gameObject.GetComponent<Rigidbody2D>().position + ratMovement[i] * ratSpeed * Time.fixedDeltaTime);
                }
            }
        }
    }

    private void GoRat()
    {
        if (goRatActive == false)
        {
            goRatActive = true;
            goRat = this.gameObject.transform.GetChild(this.gameObject.transform.GetChildCount() - 1).gameObject;
            rat_count--;
        }
        
        holeMovement.x = this.gameObject.transform.GetChild(1).transform.position.x - goRat.transform.position.x;
        holeMovement.y = this.gameObject.transform.GetChild(1).transform.position.y - goRat.transform.position.y;

        if ((int)(this.gameObject.transform.GetChild(1).transform.position.x*10 - goRat.transform.position.x*10) == 0)
        {
            holeMovement.x = 0;
        }
        else
        {
            holeMovement.x = holeMovement.x / Mathf.Abs(holeMovement.x);

        }
        if ((int)(this.gameObject.transform.GetChild(1).transform.position.y*10 - goRat.transform.position.y*10) == 0)
        {
            holeMovement.y = 0;
        }
        else
        {
            holeMovement.y = holeMovement.y / Mathf.Abs(holeMovement.y);

        }
        if (holeMovement.x != 0)
        {

            goRat.gameObject.GetComponent<Rigidbody2D>().MovePosition(goRat.gameObject.GetComponent<Rigidbody2D>().position + holeMovement * (ratSpeed / 0.835f) * Time.fixedDeltaTime);
        }
        else
        {
            if(holeMovement.y != 0)
            {
                goRat.gameObject.GetComponent<Rigidbody2D>().MovePosition(goRat.gameObject.GetComponent<Rigidbody2D>().position + holeMovement * ratSpeed * Time.fixedDeltaTime);

            }
            else
            {
                holeMovement.y = 5;
                goRat.gameObject.GetComponent<Rigidbody2D>().MovePosition(goRat.gameObject.GetComponent<Rigidbody2D>().position + holeMovement * ratSpeed * Time.fixedDeltaTime);
                goRat.gameObject.GetComponent<Rigidbody2D>().MovePosition(goRat.gameObject.GetComponent<Rigidbody2D>().position + holeMovement * ratSpeed * Time.fixedDeltaTime);

                searching = true;
                findCherry = false;
            }
        }
    }

    private void SearchPlayer()
    {
        goRat.GetComponent<Animator>().SetBool("searchPlayer", true);
        searching_timer -= 0.1f;
        searching_timer = Mathf.Round(searching_timer * 10.0f) * 0.1f;

        if(searching_timer == 20)
        {
            goRat.transform.localScale = new Vector3(-(goRat.transform.localScale.x), goRat.transform.localScale.y, goRat.transform.localScale.z);
        }
        else if(searching_timer == 15)
        {
            goRat.transform.localScale = new Vector3(-(goRat.transform.localScale.x), goRat.transform.localScale.y, goRat.transform.localScale.z);
        }
        else if(searching_timer == 10)
        {
            goRat.transform.localScale = new Vector3(-(goRat.transform.localScale.x), goRat.transform.localScale.y, goRat.transform.localScale.z);
        }
        else if(searching_timer > 0)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.name == "PlayerOutside")
                {
                    Player = player;
                }
            }
            if(Mathf.Abs(Player.transform.position.x - this.gameObject.transform.position.x) < 25)
            {
                Destroy(cherry);
                Destroy(goRat);
                searching = false;
                goRatActive = false;
            }
        }
        else if(searching_timer < 0)
        {
            goRat.GetComponent<Animator>().SetBool("searchPlayer", false);
            goRat.GetComponent<Animator>().SetBool("attack", true);
            if(cherry.transform.position.x - goRat.transform.position.x < 0)
            {

                holeMovement.x = -3;
                holeMovement.y = 0;
                goRat.gameObject.GetComponent<Rigidbody2D>().MovePosition(goRat.gameObject.GetComponent<Rigidbody2D>().position + holeMovement * ratSpeed);
            }
            else
            {
                goRat.transform.localScale = new Vector3(-(goRat.transform.localScale.x), goRat.transform.localScale.y, goRat.transform.localScale.z);
                holeMovement.x = 3;
                holeMovement.y = 0;
                goRat.gameObject.GetComponent<Rigidbody2D>().MovePosition(goRat.gameObject.GetComponent<Rigidbody2D>().position + holeMovement * ratSpeed);
            }
            searching = false;
            waitingRat = true;
            goRat.GetComponent<Animator>().SetBool("attack", false);
            goRat.GetComponent<Animator>().SetBool("waiting", true);
            Destroy(cherry);
        }
    }

    private void WaitingPlayer()
    {
        waitingRat_timer -= Time.fixedDeltaTime;
        if (waitingRat_timer < 0)
        {
            waitingRat_timer = 20f;
            waitingRat = false;
            Destroy(goRat);
            goRatActive = false;
        }   

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cherry")
        {
            findCherry = true;
            cherry = collision.gameObject;
        }
    }
}
