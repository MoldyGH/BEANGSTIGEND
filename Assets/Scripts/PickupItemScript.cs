using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PickupItemScript : MonoBehaviour
{

    public int SelectedItem = 1;
    public int lastone;
    public int MaxSlots;
    public List<Image> backgrounds;
    public Color BackgroundColor;
    public List<Sprite> ItemImages;
    public List<Image> ItemSlotImages;
    public List<int> items;
    public List<String> names;
    public TMP_Text itemtext;
    public int ID;
    public GameObject player;
    public Color normalColor;
    public Move playerScript;

    //Items
    public int randomPillEffect;
    public float pillEffectTime;
    public bool hasPillEffect;
    public float lockTime;
    public Material lockedMaterial;
    public Material normalDoor;
    [SerializeField] Door raycastHitDoor;
    [SerializeField] MeshRenderer doorChild;
    private bool isLock;
    public GameObject[] itemList;
    public AudioClip BOMB;
    public AudioSource massDestruction;
    public SpriteRenderer hand;
    public GameObject lightFlashlight;
    public Transform teacherScary;

    public GameManager gameManager;

    //Debug
    public string[] pillLogMessages;


    // Start is called before the first frame update
    void Start()
    {
        SelectedItem = 0;
        backgrounds[SelectedItem].color = BackgroundColor;
        lastone = SelectedItem;
    }


       

    public void UseItem()
    {
        switch(items[SelectedItem])
        {
            case 1:
                playerScript.stamina = 100f;
                break;
            case 2:
                playerScript.stamina = 300f;
                break;
            case 3:
                randomPillEffect = UnityEngine.Random.Range(0, 6);
                switch(randomPillEffect)
                {
                    case 0:
                        playerScript.Die();
                        break;
                    case 1:
                        RenderSettings.fog = false;
                        break;
                    case 2:
                        playerScript.stamina = 9999f;
                        break;
                    case 3:
                        randomPillEffect = UnityEngine.Random.Range(0, 6);
                        return;
                    case 4:
                        break;
                    case 5:
                        AddItem(UnityEngine.Random.Range(1, 8));
                        break;
                    case 6:
                        playerScript.movementSpeed = 300f;
                        playerScript.staminaRate = 40f;
                        break;
                }
                Debug.Log(pillLogMessages[randomPillEffect]);
                hasPillEffect = true;
                pillEffectTime = 30f;
                break;
            case 4:
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit) && (raycastHit.transform.tag == "Door" & Vector3.Distance(player.transform.position, raycastHit.transform.position) < 10f))
                {
                    doorChild = raycastHit.transform.gameObject.GetComponentInChildren<MeshRenderer>();
                    raycastHitDoor = raycastHit.transform.gameObject.GetComponent<Door>();
                    doorChild.material = lockedMaterial;
                    raycastHitDoor.isLocked = false;
                    isLock = true;
                    lockTime = 30f;
                }
                break;
            case 5:
                PlayAudio(massDestruction, BOMB);
                GameObject[] all = FindObjectsOfType<GameObject>();
                for (int i = 0; i < all.Length; i++)
                {
                    all[i].AddComponent<Rigidbody>();
                    all[i].GetComponent<Rigidbody>().isKinematic = true;
                }
                break;
            case 8:               
                if(!gameManager.chase || !gameManager.endChase)
                {
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit raycastHit2;
                    if (Physics.Raycast(ray2, out raycastHit2) && (raycastHit2.transform.tag == "MainTeacher" & Vector3.Distance(player.transform.position, teacherScary.position) < 10f))
                    {
                        teacherScary.GetComponent<Beangstigend>().Flashed();
                        ReplaceItem(SelectedItem, 0);
                    }
                }
                break;
        }
        if (items[SelectedItem] != 6 && items[SelectedItem] != 7 && items[SelectedItem] != 8)
        {
            ReplaceItem(SelectedItem, 0);
        }
        UpdateName();
    }


    public void ReplaceItem(int slot, int itemtoreplacewith)
    {
         items[slot] = itemtoreplacewith;
         ItemSlotImages[slot].sprite = ItemImages[itemtoreplacewith];
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetMouseButtonDown(1) & Time.timeScale != 0f)
            {
                    UseItem();
            }
            if(Input.GetKeyDown(KeyCode.R) & Time.timeScale != 0f)
        {
            Drop();
        }
        if (isLock)
        {
            lockTime -= 1f * Time.deltaTime;
            if (lockTime <= 0f)
            {
                raycastHitDoor.isLocked = false;
                doorChild.material = normalDoor;
                isLock = false;
            }
        }
        else if(raycastHitDoor = null)
        {
            return;
        }
        if (hasPillEffect)
        {

            pillEffectTime -= 1f * Time.deltaTime;
            if (pillEffectTime <= 0f)
            {
                hasPillEffect = false;
                RenderSettings.fog = true;
                playerScript.stamina = 100f;
                playerScript.staminaRate = 20f;
                playerScript.movementSpeed = 500f;
            }
        }
        if(items[SelectedItem] == 8)
        {
            lightFlashlight.SetActive(true);
        }
        else
        {
            lightFlashlight.SetActive(false);
        }


    	if ((Input.GetAxis("Mouse ScrollWheel") > 0f & SelectedItem > -0))
	{
            backgrounds[lastone].color = normalColor;
	    SelectedItem--;
            backgrounds[SelectedItem].color = BackgroundColor;
            lastone = SelectedItem;
            this.UpdateName();
	}
    	if ((Input.GetAxis("Mouse ScrollWheel") < 0f & SelectedItem < MaxSlots))
	 {
            backgrounds[lastone].color = normalColor;
	    SelectedItem++;
            backgrounds[SelectedItem].color = BackgroundColor;
            lastone = SelectedItem;
            this.UpdateName();
	 } 

     if (Input.GetKeyDown(KeyCode.Alpha1))
     {
         backgrounds[lastone].color = normalColor;
          SelectedItem = 0;
          backgrounds[SelectedItem].color = BackgroundColor;
          lastone = SelectedItem;
          this.UpdateName();
     }
     if (Input.GetKeyDown(KeyCode.Alpha2))
     {
         backgrounds[lastone].color = normalColor;
          SelectedItem = 1;
          backgrounds[SelectedItem].color = BackgroundColor;
          lastone = SelectedItem;
          this.UpdateName();
     }
     if (Input.GetKeyDown(KeyCode.Alpha3))
     {
         backgrounds[lastone].color = normalColor;
          SelectedItem = 2;
          backgrounds[SelectedItem].color = BackgroundColor;
          lastone = SelectedItem;
          this.UpdateName();
     }
     if (Input.GetKeyDown(KeyCode.Alpha4))
     {
         backgrounds[lastone].color = normalColor;
          SelectedItem = 3;
          backgrounds[SelectedItem].color = BackgroundColor;
          lastone = SelectedItem;
          this.UpdateName();
     }

     

     

    }


   public void UpdateName()
   {
         itemtext.text = names[items[SelectedItem]];
        hand.sprite = ItemImages[items[SelectedItem]];
   }
    public void DropItem(int item)
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y = player.transform.position.y + 4;

        Instantiate(itemList[item], playerPos, player.transform.rotation);
        ReplaceItem(SelectedItem, 0);
    }
    public void Drop()
    {
        DropItem(items[SelectedItem]);
        UpdateName();
    }
    public void PlayAudio(AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }
    
    

    public void AddItem(int item)
    {

        if (this.items[0] == 0)
	{
	        items[0] = item;
		ItemSlotImages[0].sprite = ItemImages[item];
        }
        else if (this.items[1] == 0)
	{
	        items[1] = item;
		ItemSlotImages[1].sprite = ItemImages[item];
        }
        else if (this.items[2] == 0)
	{
	        items[2] = item;
		ItemSlotImages[2].sprite = ItemImages[item];
        }
        else if (this.items[3] == 0)
	{
	        items[3] = item;
		ItemSlotImages[3].sprite = ItemImages[item];
        }
        else
        {
                items[SelectedItem] = item;
		ItemSlotImages[SelectedItem].sprite = ItemImages[item];   
        }
        UpdateName();
        }
    }
