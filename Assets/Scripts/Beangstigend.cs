using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Beangstigend : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public SpriteRenderer bean_spr;
    public Sprite idle;
    public AudioClip SCR;
    public float jumpscareTime;
    public bool jumpscared;
    public GameObject jumpscareImage;
    public Sprite jumpscareSPR;

    public bool flashed;
    public GameObject closetTarget;

    public float closetTime = 30f;

    public AudioClip flashedAudio;
    public AudioSource bean;

    public GameManager gameManager;
    public CapsuleCollider playerCollider;

    public AudioClip step;
    public float soundTime = 1.1f;

    public float doorPause;

    private bool isTouchingDoor;

    void Update()
    {
        if (!flashed && !gameManager.endChase)
        {
            agent.SetDestination(player.position);
        }
        if(jumpscared)
        {
            jumpscareTime -= 1f * Time.deltaTime;
        }
        if(jumpscareTime <= 0f)
        {
            SceneManager.LoadScene("Menu");
        }
        if(flashed)
        {
            agent.SetDestination(closetTarget.transform.position);
            closetTime -= 1f * Time.deltaTime;
            bean.clip = flashedAudio;
            bean.Play();

            if (closetTime <= 0f)
            {
                flashed = false;
                closetTime = 30f;
            }
        }
        soundTime -= 1f * Time.deltaTime;
        if(soundTime <= 0f)
        {
            bean.clip = step;
            bean.Play();
            soundTime = gameManager.updatedSoundTime;
        }
    }
    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Door"))
        {
            if(isTouchingDoor && !flashed)
            {
                doorPause -= 1f * Time.deltaTime;
            }
            
            if(doorPause <= 0f && isTouchingDoor)
            {
                isTouchingDoor = false;
                collider.GetComponent<Door>().OpenDoor();
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Door"))
        {
            doorPause = gameManager.timeToOpenDoor;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Door"))
        {
            isTouchingDoor = true;

            if(flashed)
            {
                collider.GetComponent<Door>().OpenDoor();
            }
        }
    }
    public void TriggerJumpscare()
    {
        jumpscared = true;
        Camera.main.gameObject.AddComponent<AudioDistortionFilter>();
        Camera.main.gameObject.GetComponent<AudioDistortionFilter>().distortionLevel = 1;
        base.GetComponent<AudioSource>().clip = SCR;
        base.GetComponent<AudioSource>().Play();
        bean_spr.sprite = jumpscareSPR;
        jumpscareImage.SetActive(true);
        LockMouse(false);

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
    public void Flashed()
    { 
        flashed = true;
    }
}
