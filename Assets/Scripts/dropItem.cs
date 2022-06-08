using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItem : MonoBehaviour
{
    [SerializeField] private GameObject DropItem;
    [HideInInspector] public bool fight;
    // Start is called before the first frame update
    void Start()
    {
        fight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {        
        if(fight == true)
        {
            fight = false;
            GameObject dropItemClone = Instantiate(DropItem, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
            dropItemClone.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2, 2), Random.Range(6, 10));
            this.gameObject.SetActive(false);
        }

    }
}
