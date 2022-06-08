using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textComponent;
    [SerializeField] private Button[] button;
    [SerializeField] private Image image;
    [SerializeField] private Image[] talkcloud;
    private string line;
    private float textSpeed;
    private int index;
    [HideInInspector] public bool active = false;
    private int whostalk;
    private bool waitTalk;
    private int talkCount;
    private bool chooseToDoing;
    private bool killIt;
    private bool returnHome;
    private float movementTimer;
    [SerializeField] private GameObject boss;
    private bool bossattack;
    private float colortimer;
    // Start is called before the first frame update
    void Start()
    {
        line = string.Empty;
        textComponent[0].text = string.Empty;
        textComponent[1].text = string.Empty;
        textSpeed = 0.1f;
        whostalk = 0;
        waitTalk = false;
        talkCount = 0;
        chooseToDoing = false;
        killIt = false;
        returnHome = false;
        movementTimer = 0.5f; ;
        bossattack = false;
        image.GetComponent<Image>().color = new Color(0,0,0,0);

        colortimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (active)
        {
            foreach(Image img in talkcloud)
            {
                img.gameObject.SetActive(true);
            }
            this.gameObject.GetComponent<Animator>().SetBool("jump", false);
            this.gameObject.GetComponent<Animator>().SetBool("walk", false);
            StartDialogue();
        }
        if(chooseToDoing)
        {
            chooseToDoing = false;
            ActivateButton();
        }
        if (killIt)
        {
            kill();
        }
        else if (returnHome)
        {
            dontkill();
        }
    }
    private void StartDialogue()
    { 
        index = 0;

        if (waitTalk == false && talkCount == 4)
        {
            waitTalk = true;
            textComponent[whostalk].text = string.Empty;
            line = "Peki þimdi ne yapacaksýn?";
            whostalk = 0;
            StartCoroutine(TypeLine());
        }
        else if (waitTalk == false && talkCount == 3)
        {
            waitTalk = true;
            textComponent[whostalk].text = string.Empty;
            line = "Artýk uçabiliyorum ve uçarak geldim.";
            whostalk = 1;
            StartCoroutine(TypeLine());
        }
        else if (waitTalk == false && talkCount == 2)
        {
            waitTalk = true;
            textComponent[whostalk].text = string.Empty;
            line = "Canlýlarý öldürüp kendimi geliþtirdim.";
            whostalk = 1;
            StartCoroutine(TypeLine());

        }
        else if (waitTalk == false && talkCount == 1)
        {
            waitTalk = true;
            textComponent[whostalk].text = string.Empty;
            line = "Aðaçlarý kestim.";
            whostalk = 1;
            StartCoroutine(TypeLine());
        }
        else if (waitTalk == false && talkCount == 0)
        {
            waitTalk = true;
            textComponent[whostalk].text = string.Empty;
            line = "Buraya nasýl geldin?";
            whostalk = 0;
            StartCoroutine(TypeLine());
        }
    }

    private void ActivateButton()
    {
        foreach (Button btn in button)
        {
            btn.gameObject.SetActive(true);
        }
    }

    public void killHimButtonClick()
    {
        killIt = true;
        foreach (Button btn in button)
        {
            btn.gameObject.SetActive(false);
        }
    }    

    public void returnHomeButtonClick()
    {
        returnHome = true;
        foreach (Button btn in button)
        {
            btn.gameObject.SetActive(false);
        }
    }

    private void kill()
    {
        movementTimer -= Time.fixedDeltaTime;
        if (movementTimer < -6)
        {
            Application.Quit();
        }
        else if (movementTimer < -3)
        {
            image.gameObject.SetActive(true);
            this.gameObject.GetComponent<Renderer>().enabled = false;
            colortimer += Time.fixedDeltaTime * 1f;
            if(colortimer > 1)
            {
                colortimer = 1;
            }
            image.GetComponent<Image>().color = new Color(0,0,0, colortimer);
        }
        else if (movementTimer < -2)
        {
            boss.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
            this.gameObject.GetComponent<Animator>().SetBool("escape", true);
        }
        else if (movementTimer < -1)
        {
            boss.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
        }
        else if (movementTimer < 0 && bossattack == false)
        {
            bossattack = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(1, 0) * -10;
            boss.GetComponent<Animator>().SetTrigger("Attack");
        }
    }
    private void dontkill()
    {

        movementTimer -= Time.fixedDeltaTime;
        if (movementTimer < -6)
        {
            Application.Quit();
        }
        else if(movementTimer < -3)
        {
            image.gameObject.SetActive(true);
            this.gameObject.GetComponent<Renderer>().enabled = false;
            colortimer += Time.fixedDeltaTime * 1f;
            if (colortimer > 1)
            {
                colortimer = 1;
            }
            image.GetComponent<Image>().color = new Color(0, 0, 0, colortimer);
        }
        else if (movementTimer < -2)
        {
            boss.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
            this.gameObject.GetComponent<Animator>().SetBool("escape", true);
        }
        else if (movementTimer < -1)
        {
            boss.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
        }
        else if (movementTimer < 0 && bossattack == false)
        {
            bossattack = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(1, 0) * 10;
            boss.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in line.ToCharArray())
        {
            textComponent[whostalk].text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(1f);
        talkCount++;
        waitTalk = false;
        if(talkCount == 5)
        {
            active = false;
            chooseToDoing = true;
        }
    }
}
