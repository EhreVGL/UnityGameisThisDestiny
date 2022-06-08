using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMech : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    private Transform crosshairTransform;
    private bool isTree;
    private bool isBriad;
    private bool isSlime;
    private bool isCherryTree;
    private bool isRat;
    private bool isBat;
    private Animator anim;
    private GameObject triggerEnterObject;
    private float animation_timer;
    [HideInInspector] public int CherryInInventory;
    [SerializeField] private Text text;
    private int which_mech;
    [SerializeField] private GameObject Cherry;
    [SerializeField] private GameObject Point;
    private int launchForce;
    private GameObject[] Points;
    private int numberOfPoints;
    private float spaceBetweenPoints;
    private Color color;
    private Vector2 position;
    [HideInInspector] public bool ropeEnabled;

    //Hook Mech
    [SerializeField] private LayerMask ropeLayerMask;
    private float distance;
    private LineRenderer line;
    private DistanceJoint2D rope;
    Vector2 lookDirection;
    private float mx;
    private float my;
    private Rigidbody2D rb;
    private float hook_timer;
    private bool giveBone;
    private bool hookCheck;
    [HideInInspector] public bool wingCheck;

    // Start is called before the first frame update
    void Start()
    {
        crosshairTransform = crosshair.transform;
        CherryInInventory = 0;
        animation_timer = 0.1f;
        anim = GetComponent<Animator>();
        isTree = false;
        isBriad = false;
        isSlime = false;
        isCherryTree = false;
        isRat = false;
        isBat = false;
        triggerEnterObject = new GameObject();
        which_mech = 0;

        launchForce = 20;
        color = Color.white;
        color.a = 0.33f;
        numberOfPoints = 10;
        spaceBetweenPoints = 0.15f;
        Points = new GameObject[numberOfPoints];
        for(int i = 0; i < numberOfPoints; i++)
        {
            Points[i] = Instantiate(Point, transform.position, Quaternion.identity);
            Points[i].GetComponentInChildren<Renderer>().material.color = color;
            Points[i].gameObject.SetActive(false);
        }

        rope = gameObject.AddComponent<DistanceJoint2D>();
        rope.enableCollision = true;
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        distance = 30f;
        rope.enabled = false;
        line.enabled = false;
        hook_timer = 5f;
        ropeEnabled = false;
        giveBone = false;
        hookCheck = false;
        wingCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.name == "PlayerInside")
        {
            which_mech = 0;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                which_mech = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                which_mech = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                which_mech = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                which_mech = 3;
            }
        }

        if (which_mech == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                crosshair.transform.position = crosshairTransform.position;
                crosshair.transform.rotation = crosshairTransform.rotation;
                crosshair.transform.localScale = crosshairTransform.localScale;
                Melee();
                crosshairTransform = crosshair.transform;
            }
        }
        else if (which_mech == 1)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Range();
            }
            else
            {
                for (int i = 0; i < numberOfPoints; i++)
                {
                    Points[i].gameObject.SetActive(false);
                }
                if (this.gameObject.GetComponent<PlayerOutsideMovement>().cherryInInventory != 0)
                {
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        this.gameObject.GetComponent<PlayerOutsideMovement>().cherryInInventory--;
                        GameObject newCherry = Instantiate(Cherry, transform.position, transform.rotation);
                        newCherry.GetComponent<Rigidbody2D>().velocity = crosshair.transform.right * launchForce;
                    }
                }
            }
        }
        else if (which_mech == 2)
        {
            line.SetPosition(0, transform.position);

            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (Input.GetMouseButton(0))
            {
                if (hookCheck == false)
                {
                    Hook();
                }
            }
            else
            {
                rope.enabled = false;
                ropeEnabled = false;
                line.enabled = false;
                giveBone = false;
                hookCheck = false;
                anim.SetBool("hook", false);
            }

            if (rope.enabled)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    Vector2 grapplePos = Vector2.Lerp(transform.position, rope.connectedAnchor, 1f * Time.deltaTime);
                    transform.position = grapplePos;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Vector2 grapplePos = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y) + (new Vector2(transform.position.x, transform.position.y) - rope.connectedAnchor), 1f * Time.deltaTime);
                    transform.position = grapplePos;
                }
            }

            if (Vector2.Distance(transform.position, rope.connectedAnchor) < 0.5f)
            {
                rope.enabled = false;
                ropeEnabled = false;
                line.enabled = false;
                giveBone = false;
                hookCheck = false;
                anim.SetBool("hook", false);
            }

            mx = Input.GetAxisRaw("Horizontal");
            my = Input.GetAxisRaw("Vertical");


        }
        else if (which_mech == 3)
        {
            wingCheck = true;
        }

        if(which_mech != 3)
        {
            wingCheck = false;
        }

        if (anim.GetBool("melee") == true)
        {
            animation_timer -= Time.deltaTime;
            if (animation_timer < 0)
            {
                anim.SetBool("melee", false);
                animation_timer = 0.1f;
            }
        }

        if (isSlime == true && Input.GetKeyDown(KeyCode.E))
        {
            SlimeFight();
        }
        if (isRat == true && Input.GetKeyDown(KeyCode.E))
        {
            RatFight();
        }
        if (isBat == true && Input.GetKeyDown(KeyCode.E))
        {
            BatFight();
        }
    }

    private void FixedUpdate()
    {
        if(rope.enabled)
        {
            rb.velocity += new Vector2(mx, 0) * 5f * Time.fixedDeltaTime;
        }
    }

    private void Melee()
    {
        anim.SetBool("melee", true);
        if (isTree == true)
        {
            triggerEnterObject.GetComponent<Tree>().trigger = true;
        }
        else if(isBriad == true)
        {
            triggerEnterObject.GetComponent<SlimeHome>().trigger = true;
        }
        else if (isCherryTree == true)
        {
            triggerEnterObject.GetComponent<CherryTree>().trigger = true;
            CherryInInventory++;
            text.text = "x" + CherryInInventory;
        }
    }

    private void Range()
    {
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;
        position.x = transform.position.x;
        position.y = transform.position.y;
        crosshair.transform.right = direction;

        for(int i = 0; i < numberOfPoints; i++)
        {
            Points[i].gameObject.SetActive(true);
            Points[i].transform.position = position + (direction.normalized * launchForce * (i * spaceBetweenPoints)) + 1f * Physics2D.gravity * ((i * spaceBetweenPoints) * (i * spaceBetweenPoints));
        }
    }

    private void Hook()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, distance, ropeLayerMask);
        if (hit.collider != null && this.gameObject.GetComponent<PlayerOutsideMovement>().boneInInventory != 0)
        {
            if(giveBone == false)
            {
                giveBone = true;
                this.gameObject.GetComponent<PlayerOutsideMovement>().boneInInventory--;
            }
            Vector2 newPos;

            rope.enabled = true;
            ropeEnabled = true;
            hookCheck = true;
            rope.connectedAnchor = hit.point;

            anim.SetBool("hook",true);

            line.enabled = true;

            for(float i = 0; i < hook_timer; i += 10f * Time.deltaTime)
            {
                newPos = Vector2.Lerp(transform.position, rope.connectedAnchor, i / hook_timer);
                line.SetPosition(0, transform.position);
                line.SetPosition(1, newPos);
            }
            line.SetPosition(1, rope.connectedAnchor);
        }
    }
    private void SlimeFight()
    {
        // Savaþla ilgili þeyler.
        triggerEnterObject.GetComponent<dropItem>().fight = true;

    }
    private void RatFight()
    {
        // Savaþla ilgili þeyler.
        triggerEnterObject.GetComponent<dropItem>().fight = true;

    }
    private void BatFight()
    {
        // Savaþla ilgili þeyler.
        triggerEnterObject.GetComponent<dropItem>().fight = true;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CherryTree")
        {
            triggerEnterObject = collision.gameObject;
            isCherryTree = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CherryTree")
        {
            isCherryTree = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tree")
        {
            triggerEnterObject = collision.gameObject;
            isTree = true;
        }
        if(collision.gameObject.tag == "Briad")
        {
            triggerEnterObject = collision.gameObject;
            isBriad = true;
        }
        if(collision.gameObject.tag == "Slime")
        {
            triggerEnterObject = collision.gameObject;
            isSlime = true;
        }
        if (collision.gameObject.tag == "Rat")
        {
            triggerEnterObject = collision.gameObject;
            isRat = true;
        }
        if (collision.gameObject.tag == "Bat")
        {
            triggerEnterObject = collision.gameObject;
            isBat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tree")
        {
            isTree = false;
        }
        if(collision.gameObject.tag == "Briad")
        {
            isBriad = false;
        }
        if (collision.gameObject.tag == "Slime")
        {
            isSlime = false;
        }
        if (collision.gameObject.tag == "Rat")
        {
            isRat = false;
        }
        if (collision.gameObject.tag == "Bat")
        {
            isBat = false;
        }
    }
}
