using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BowlingBall : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float timerUntilActivate;
    public GameManager gameManager;
    public bool isActivated;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.evilMode && !gameManager.endChase)
        {
            timerUntilActivate -= 1f * Time.deltaTime;
            if(timerUntilActivate <= 0f && !isActivated)
            {
                isActivated = true;
            }
        }
        if(isActivated)
        {
            agent.SetDestination(player.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other = player.gameObject.GetComponent<CapsuleCollider>())
        {
            if(isActivated)
            {
                player.GetComponent<Move>().Die();
            }
        }
    }
}
