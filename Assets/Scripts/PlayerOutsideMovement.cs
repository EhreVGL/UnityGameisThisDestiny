using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class PlayerOutsideMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 velocity;
    private Vector2 scale;

    private bool ontheGround;
    private bool ontheGround2;
    private bool ontheWall;
    private float wallJumpTimer;
    private float vertical_input;
    private bool triggerDoor;
    private bool enterHome;
    private bool end;

    [SerializeField] private GameObject Home;
    private Color color;
    private float color_alpha_timer;
    private Color color_inside;
    [SerializeField] private GameObject playerInside;
    private Vector2 playerInSideOffset;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [HideInInspector] public int woodInInventory;
    [HideInInspector] public int slimeGelInInventory;
    [HideInInspector] public int featherInInventory;
    [HideInInspector] public int boneInInventory;
    [HideInInspector] public int cherryInInventory;
    [HideInInspector] public int upgrade;

    [SerializeField] private Text[] text;

    private bool payFeather;
    private float flyNow;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scale = GetComponent<Transform>().localScale;
        ontheGround = false;
        ontheGround2 = false;
        ontheWall = false;
        wallJumpTimer = 0.1f;
        triggerDoor = false;
        color = new Color(0, 0, 0, 0);
        color_alpha_timer = 0f;
        enterHome = false;
        playerInSideOffset = new Vector2(-2.42f, -6.68f);
        woodInInventory = 0;
        slimeGelInInventory = 0;
        featherInInventory = 0;
        boneInInventory = 0;
        cherryInInventory = 0;
        upgrade = 0;
        payFeather = false;
        flyNow = 0.3f;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerDoor == true && Input.GetKeyDown(KeyCode.E))
        {
            enterHome = true;
        }
    }

    private void FixedUpdate()
    {
        if (!end)
        {
            vertical_input = Input.GetAxis("Vertical");
            velocity.x = Input.GetAxis("Horizontal") * 12f;
            velocity.y = rb.velocity.y;

            if (enterHome == false)
            {
                if (this.gameObject.GetComponent<PlayerMech>().ropeEnabled == false)
                {
                    // Walk
                    if (Input.GetAxis("Horizontal") != 0)
                    {
                        if (Input.GetAxis("Horizontal") > 0)
                        {
                            transform.position += velocity * Time.fixedDeltaTime;
                            scale.x = 16;
                            this.gameObject.transform.localScale = scale;
                            anim.SetBool("walk", true);
                        }
                        else if (Input.GetAxis("Horizontal") < 0)
                        {
                            transform.position += velocity * Time.fixedDeltaTime;
                            scale.x = -16;
                            this.gameObject.transform.localScale = scale;
                            anim.SetBool("walk", true);
                        }
                    }
                    else
                    {
                        anim.SetBool("walk", false);
                    }

                    if (this.gameObject.GetComponent<PlayerMech>().wingCheck == false)
                    {
                        //Jump
                        if (vertical_input > 0.3 && ontheGround == true)
                        {
                            rb.velocity = new Vector2(0, 15);
                            anim.SetBool("jump", true);
                        }
                        else if (ontheGround2 == true)
                        {
                            anim.SetBool("jump", false);
                        }
                        if (ontheGround2 == true)
                        {
                            ontheGround = true;
                            ontheGround2 = false;
                        }

                        // Jump to Wall
                        if (ontheWall == true)
                        {
                            wallJumpTimer = 0.1f;
                            rb.gravityScale = 0;
                            rb.velocity = Vector2.zero;
                        }
                        else
                        {
                            if (wallJumpTimer > 0)
                            {
                                wallJumpTimer -= Time.fixedDeltaTime;
                                if (vertical_input > 0.3f)
                                {
                                    rb.velocity = new Vector2(0, 12);
                                    anim.SetBool("jump", true);
                                }
                            }
                            else
                            {
                                wallJumpTimer = 0;
                                rb.gravityScale = 3;
                            }
                        }
                    }
                    else
                    {
                        flyNow -= Time.fixedDeltaTime;
                        if (vertical_input > 0 && featherInInventory > 0 && flyNow < 0)
                        {
                            flyNow = 0.3f;
                            if (payFeather == false)
                            {
                                ontheGround2 = false;
                                featherInInventory--;
                                payFeather = true;
                            }
                            rb.velocity = new Vector2(0, 15);
                        }
                        if (ontheGround2 == true)
                        {
                            payFeather = false;
                        }
                    }

                    if (ontheGround == false)
                    {
                        vertical_input = 0f;
                    }
                }
            }

            if (enterHome == true)
            {
                EnteringHome();
            }

            playerInside.GetComponent<PlayerMech>().CherryInInventory = cherryInInventory;
            text[0].text = "x" + woodInInventory;
            text[1].text = "x" + slimeGelInInventory;
            text[2].text = "x" + boneInInventory;
            text[3].text = "x" + featherInInventory;
            text[4].text = "x" + cherryInInventory;
        }
    }

    private void EnteringHome()
    {
        Home.SetActive(true);
        color_alpha_timer += Time.fixedDeltaTime;
        if (color.a < 1)
        {
            color.a += color_alpha_timer / 125;
        }
        else
        {
            color.a = 1;
            Home.transform.GetChild(0).gameObject.SetActive(true);
            if(upgrade == 0)
            {
                Home.transform.GetChild(1).gameObject.SetActive(true);
                Home.transform.GetChild(2).gameObject.SetActive(true);
            }
            else if(upgrade == 1)
            {
                Home.transform.GetChild(1).gameObject.SetActive(true);
                Home.transform.GetChild(3).gameObject.SetActive(true);
                Home.transform.GetChild(4).gameObject.SetActive(true);
            }
            else if (upgrade == 2)
            {
                Home.transform.GetChild(1).gameObject.SetActive(true);
                Home.transform.GetChild(3).gameObject.SetActive(true);
                Home.transform.GetChild(5).gameObject.SetActive(true);
                Home.transform.GetChild(6).gameObject.SetActive(true);
            }
            else if (upgrade == 3)
            {
                Home.transform.GetChild(1).gameObject.SetActive(true);
                Home.transform.GetChild(3).gameObject.SetActive(true);
                Home.transform.GetChild(6).gameObject.SetActive(true);
                Home.transform.GetChild(7).gameObject.SetActive(true);
                Home.transform.GetChild(8).gameObject.SetActive(true);
            }
        }

        Home.GetComponentInChildren<Renderer>().material.color = color;


        if(transform.localScale.x < 0)
        {
            scale.x = 16;
            transform.localScale = scale;
        }
        if(scale.x > 4)
        {
            scale.x -= Time.fixedDeltaTime * 4;
            scale.y -= Time.fixedDeltaTime * 4;
            transform.localScale = scale;
        }
        else
        {
            scale.x = 4;
            scale.y = 4;
            transform.localScale = scale;
        }
        if (virtualCamera.m_Lens.OrthographicSize > 7)
        {
            virtualCamera.m_Lens.OrthographicSize -= Time.fixedDeltaTime * 4;
        }
        else
        {

            virtualCamera.m_Lens.OrthographicSize = 7;
            virtualCamera.Follow = playerInside.transform;
            playerInside.transform.localScale = scale;
            enterHome = false;
            color.a = 0;
            scale.x = 16;
            scale.y = 16;
            playerInside.transform.position = playerInSideOffset;
            playerInside.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            ontheGround2 = true;
        }
        if(collision.gameObject.tag == "wall")
        {
            ontheWall = true;
        }
        if (collision.gameObject.name == "End")
        {
            this.gameObject.GetComponent<Dialogue>().active = true;
            end = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {

            ontheGround = false;
        }
        if(collision.gameObject.tag == "wall")
        {
            ontheWall = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "door")
        {
            triggerDoor = true;
        }

        if (collision.gameObject.tag == "Wood")
        {
            woodInInventory++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "SlimeGel")
        {
            slimeGelInInventory++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Feather")
        {
            featherInInventory++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Bone")
        {
            boneInInventory++;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "door")
        {
            triggerDoor = false;
        }
    }
}
