using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monitor : MonoBehaviour
{
    public NavMeshAgent agent;
    public Wander wanderManager;

    bool isMoving;

    public Transform player;
    public Move playerScript;

    private AudioQueue audioQueue;

    //Aud
    public AudioClip noRunning;

    //Bools
    public bool angry;
    public bool inOffice;
    public bool seesRuleBreak;

    //ints n stuff
    public int detentions;

    private void Start()
    {
        StartCoroutine(LateStart(0.01f));
        audioQueue = GetComponent<AudioQueue>();
    }
    private void Update()
    {
        CheckIfMoving();
    }
    public void Wander()
    {
        wanderManager.GetNewTarget();
        agent.SetDestination(wanderManager.newLocation[wanderManager.id].transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spawnpoint"))
        {
            Wander();
        }
        if(other.CompareTag("Door"))
        {
            other.GetComponent<Door>().OpenDoor();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isMoving)
        {
            Wander();
        }
        if(other.name == "Office Trigger")
        {
            inOffice = true;
        }
        if(other.CompareTag("Player") & angry & !inOffice)
        {
            inOffice = true;
            agent.Warp(new Vector3(36f, 0f, 130f));
            agent.isStopped = true;

            other.transform.position = new Vector3(30f, 0f, 130f);
            other.transform.LookAt(new Vector3(transform.position.x, other.transform.position.y, transform.position.z));

            int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 2f));
            angry = false;

            detentions++;
            if(detentions > 4)
            {
                detentions = 4;
            }
        }
    }
    IEnumerator LateStart(float time)
    {
        yield return new WaitForSeconds(time);
        Wander();
    }
    public void CheckIfMoving()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
    public void TargetPlayer()
    {
        agent.SetDestination(player.position);
    }
    public void CorrectPlayer()
    {
        audioQueue.ClearAudioQueue();
        if(playerScript.guiltType == "running")
        {
            audioQueue.QueueAudio(noRunning);
        }
    }
    private void FixedUpdate()
    {
        if(!angry)
        {
            Vector3 direction = player.position - transform.position;
            RaycastHit raycastHit;

            if (Physics.Raycast(transform.position, direction, out raycastHit) && raycastHit.transform.tag == "Player" & playerScript.guilt > 0f & !inOffice & !angry)
            {
                seesRuleBreak = true;
            }
            else
            {
                seesRuleBreak = false;
                if(agent.velocity.magnitude <= 1f)
                {
                    Wander();
                }
            }
        }
        else
        {
            TargetPlayer();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Office Trigger")
        {
            inOffice = false;
        }
    }
}
