using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Transform player;
    public BoxCollider doorCollider;

    public float openingDistance;
    public bool isLocked;
    public bool doorOpen;
    private float openTime;

    private Animator animator;
    public AudioClip openSound;
    public AudioSource doorSource;
    public GameManager gameManager;
    public PickupItemScript pickupItemScript;
    public bool redDoor, blueDoor, endDoor, redRoomDoor, transitDoor;

    public DoorNotCloseOnPlayer trigger;

    private void Start()
    {
        animator = base.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        doorSource = base.GetComponent<AudioSource>();
        pickupItemScript = gameManager.gameObject.GetComponent<PickupItemScript>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) & Time.timeScale != 0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider == doorCollider & Vector3.Distance(player.position, base.transform.position) < openingDistance & !isLocked))
            {
                if (redDoor && pickupItemScript.items[pickupItemScript.SelectedItem] == 6)
                {
                    OpenDoor();
                }
                else if(blueDoor && pickupItemScript.items[pickupItemScript.SelectedItem] == 7)
                {
                    OpenDoor();
                }
                else if(!redDoor && !blueDoor)
                {
                    OpenDoor();
                }
                else if(redRoomDoor)
                {
                    Vector3 redRoomPos = new Vector3(-196, 0, 287);
                    gameManager.TeleportPlayer(redRoomPos);
                }
                else if(transitDoor)
                {
                    Vector3 transitPos = new Vector3(5, 0, 7);
                    gameManager.TeleportPlayer(transitPos);
                }
            }
        }
        if (openTime > 0f)
        {
            openTime -= 1f * Time.deltaTime;
        }
        if(gameManager.evilMode && isLocked)
        {
            isLocked = false;
        }
    }
    public void CloseDoor()
    {
        doorOpen = false;
        animator.SetBool("open", false);
        PlayClip(openSound, doorSource);
    }
    public void OpenDoor()
    {
        doorOpen = true;
        animator.SetBool("open", true);
        PlayClip(openSound, doorSource);
        trigger.openTime = 1.5f;

        if(gameManager.endChase & endDoor)
        {
            LockMouse(false);
            SceneManager.LoadScene("Menu");
        }
    }
    public void PlayClip(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }
    public void StopClip(AudioSource source)
    {
        source.Stop();
    }
    public void LockMouse(bool lockStateVoid)
    {
        if (lockStateVoid)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


}
