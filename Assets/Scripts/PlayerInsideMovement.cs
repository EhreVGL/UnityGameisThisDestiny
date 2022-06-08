using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerInsideMovement : MonoBehaviour
{
    private Animator anim;
    private Vector2 movement;
    private Rigidbody2D rb;

    private bool triggerDoor;
    private bool exitHome;
    private bool upgradeTable;
    [HideInInspector] public int upgrade;
    private Vector2 scale;

    [SerializeField] private GameObject Home;
    private Color color;
    private float color_alpha_timer;
    [SerializeField] private GameObject playerOutside;
    private Vector2 playerOutSideOffset;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private GameObject triggerObject;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        triggerDoor = false;
        exitHome = false;
        upgradeTable = false;
        upgrade = 0;
        scale = GetComponent<Transform>().localScale;
        color = Home.GetComponent<Renderer>().material.color;
        playerOutSideOffset = new Vector2(-2.42f, -6.68f);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerDoor == true && Input.GetKeyDown(KeyCode.E))
        {
            exitHome = true;
        }
    }

    private void FixedUpdate()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if(exitHome == false)
        {
            if (movement.x != 0 && movement.y != 0)
            {
                rb.MovePosition(rb.position + movement * (3 * 0.835f) * Time.fixedDeltaTime);
                anim.SetBool("walk", true);
            }
            else if(movement.x != 0 || movement.y != 0)
            {
                rb.MovePosition(rb.position + movement * 3 * Time.fixedDeltaTime);
                anim.SetBool("walk", true);
            }
            else
            {
                anim.SetBool("walk", false);
            }
            if (movement.x < 0)
            {
                scale.x = -4;
                transform.localScale = scale;
            }
            else
            {
                scale.x = 4;
                transform.localScale = scale;
            }
        }

        if(exitHome == true)
        {
            ExitHome();
        }

        if(upgradeTable == true && Input.GetKey(KeyCode.E))
        {
            triggerObject.GetComponent<UpgradeTable>().active = true;
            upgradeTable = false;
        }

        playerOutside.GetComponent<PlayerOutsideMovement>().cherryInInventory = this.gameObject.GetComponent<PlayerMech>().CherryInInventory;
    }

    private void ExitHome()
    {
        foreach(Transform child in Home.transform)
        {
            child.gameObject.SetActive(false);
        }
        color_alpha_timer += Time.fixedDeltaTime;
        if (color.a > 0)
        {
            color.a -= color_alpha_timer / 125;
        }
        else
        {
            color.a = 0;
        }
        Home.GetComponent<Renderer>().material.color = color;
        Home.transform.GetChild(0).gameObject.SetActive(false);
        if (upgrade == 0)
        {
            Home.transform.GetChild(1).gameObject.SetActive(false);
            Home.transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (upgrade == 1)
        {
            Home.transform.GetChild(1).gameObject.SetActive(false);
            Home.transform.GetChild(3).gameObject.SetActive(false);
            Home.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if (upgrade == 2)
        {
            Home.transform.GetChild(1).gameObject.SetActive(false);
            Home.transform.GetChild(3).gameObject.SetActive(false);
            Home.transform.GetChild(5).gameObject.SetActive(false);
            Home.transform.GetChild(6).gameObject.SetActive(false);
        }
        else if (upgrade == 3)
        {
            Home.transform.GetChild(1).gameObject.SetActive(false);
            Home.transform.GetChild(3).gameObject.SetActive(false);
            Home.transform.GetChild(6).gameObject.SetActive(false);
            Home.transform.GetChild(7).gameObject.SetActive(false);
            Home.transform.GetChild(8).gameObject.SetActive(false);
        }
        if (transform.localScale.x < 0)
        {
            scale.x = 4;
            transform.localScale = scale;
        }
        if(scale.x < 16)
        {
            scale.x += Time.fixedDeltaTime * 4;
            scale.y += Time.fixedDeltaTime * 4;
            transform.localScale = scale;
        }
        else
        {
            scale.x = 16;
            scale.y = 16;
            transform.localScale = scale;
        }

        if (virtualCamera.m_Lens.OrthographicSize < 23)
        {
            virtualCamera.m_Lens.OrthographicSize += Time.fixedDeltaTime * 4;
        }
        else
        {
            virtualCamera.m_Lens.OrthographicSize = 23;
            virtualCamera.Follow = playerOutside.transform;
            playerOutside.transform.localScale = scale;
            exitHome = false;
            color.a = 1;
            scale.x = 4;
            scale.y = 4;
            playerOutside.transform.position = playerOutSideOffset; 
            playerOutside.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "door")
        {
            triggerDoor = true;
        }
        if(collision.gameObject.tag == "upgradetable")
        {
            collision.gameObject.GetComponent<UpgradeTable>().touch = true;
            triggerObject = collision.gameObject;
            upgradeTable = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "door")
        {
            triggerDoor = false;
        }
        if (collision.gameObject.tag == "upgradetable")
        {
            collision.gameObject.GetComponent<UpgradeTable>().touch = false;
            upgradeTable = false;

        }
    }
}
