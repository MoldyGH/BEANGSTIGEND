using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        base.name = "ITM_" + ItemName;
        player = GameObject.FindWithTag("Player");
        PS = GameObject.FindWithTag("GameManager").GetComponent<PickupItemScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Input.GetKeyDown(KeyCode.E) & Time.timeScale != 0f) //if (Input.GetKey(KeyCode.E) & Time.timeScale != 0f)
        {
            if (Physics.Raycast(ray, out raycastHit)) // if (Physics.Raycast(ray, out raycastHit))
            {
             if (raycastHit.transform.tag == "Item" & Vector3.Distance(this.player.transform.position, raycastHit.transform.position) <= 10f)
            {
                if (raycastHit.transform.name == "ITM_" + ItemName)
                {
                    raycastHit.transform.gameObject.SetActive(false);
                    PS.AddItem(ItemToCollect);
                }
             }
           }
           
         }
         
    }

    public PickupItemScript PS;
     public GameObject player;
    public int ItemToCollect;
    public string ItemName;
    

}
