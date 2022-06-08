using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTable : MonoBehaviour
{
    [HideInInspector] public bool active;
    [HideInInspector] public bool touch;

    [SerializeField] private GameObject playerOut;
    [SerializeField] private GameObject playerIn;
    [SerializeField] private GameObject[] requirements;
    [SerializeField] private GameObject[] OutsideUpgrades;
    [SerializeField] private GameObject[] InsideUpgrades;
    [SerializeField] private GameObject[] Colliders;
    [SerializeField] private Text[] text;
    [SerializeField] private int level;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        touch = false;
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(touch)
        {
            if(level == 0)
            {
                requirements[0].SetActive(true);
                if (active && playerOut.GetComponent<PlayerOutsideMovement>().woodInInventory >= 30 && playerOut.GetComponent<PlayerOutsideMovement>().slimeGelInInventory >= 6)
                {
                    touch = false;
                    active = false;
                    level = 1;
                    playerOut.GetComponent<PlayerOutsideMovement>().upgrade = 1;
                    playerIn.GetComponent<PlayerInsideMovement>().upgrade = 1;
                    playerOut.GetComponent<PlayerOutsideMovement>().woodInInventory -= 30;
                    playerOut.GetComponent<PlayerOutsideMovement>().slimeGelInInventory -= 6;
                    text[0].text = "x" + (playerOut.GetComponent<PlayerOutsideMovement>().woodInInventory);
                    text[1].text = "x" + (playerOut.GetComponent<PlayerOutsideMovement>().slimeGelInInventory);
                    OutsideUpgrades[0].SetActive(true) ;
                    InsideUpgrades[0].SetActive(true);
                    Colliders[0].SetActive(false);
                    Colliders[1].SetActive(true);
                }
            }
            else if(level == 1)
            {
                requirements[1].SetActive(true);
                if (active && playerOut.GetComponent<PlayerOutsideMovement>().woodInInventory >= 40 && playerOut.GetComponent<PlayerOutsideMovement>().boneInInventory >= 6)
                {
                    touch = false;
                    active = false;
                    level = 2;
                    playerOut.GetComponent<PlayerOutsideMovement>().upgrade = 2;
                    playerIn.GetComponent<PlayerInsideMovement>().upgrade = 2;
                    playerOut.GetComponent<PlayerOutsideMovement>().woodInInventory -= 40;
                    playerOut.GetComponent<PlayerOutsideMovement>().boneInInventory -= 6;
                    text[0].text = "x" + (playerOut.GetComponent<PlayerOutsideMovement>().woodInInventory);
                    text[2].text = "x" + (playerOut.GetComponent<PlayerOutsideMovement>().boneInInventory);
                    OutsideUpgrades[1].SetActive(true);
                    InsideUpgrades[1].SetActive(true);
                    Colliders[1].SetActive(false);
                    Colliders[2].SetActive(true);
                }
            }
            else if (level == 2)
            {
                requirements[2].SetActive(true);
                if (active && playerOut.GetComponent<PlayerOutsideMovement>().woodInInventory >= 40 && playerOut.GetComponent<PlayerOutsideMovement>().featherInInventory >= 6)
                {
                    touch = false;
                    active = false;
                    level = 3;
                    playerOut.GetComponent<PlayerOutsideMovement>().upgrade = 3;
                    playerIn.GetComponent<PlayerInsideMovement>().upgrade = 3;
                    playerOut.GetComponent<PlayerOutsideMovement>().woodInInventory -= 40;
                    playerOut.GetComponent<PlayerOutsideMovement>().featherInInventory -= 6;
                    text[0].text = "x" + (playerOut.GetComponent<PlayerOutsideMovement>().woodInInventory);
                    text[3].text = "x" + (playerOut.GetComponent<PlayerOutsideMovement>().featherInInventory);
                    OutsideUpgrades[2].SetActive(true);
                    InsideUpgrades[2].SetActive(true);
                    Colliders[2].SetActive(false);
                    Colliders[3].SetActive(true);
                }
            }
            else if(level == 3)
            {
                touch = false;
            }
        }
        else
        {
            foreach(GameObject req in requirements)
            {
                req.SetActive(false);
            }
        }
    }
}
