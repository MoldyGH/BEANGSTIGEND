using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderTestChar : MonoBehaviour
{
    public NavMeshAgent agent;
    public Wander wanderManager;
    public bool isMoving;

    private void Start()
    {
        StartCoroutine(LateStart(0.01f));
    }
    private void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
    public void Wander()
    {
        wanderManager.GetNewTarget();
        agent.SetDestination(wanderManager.newLocation[wanderManager.id].transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Spawnpoint"))
        {
            Wander();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(!isMoving)
        {
            Wander();
        }
    }
    IEnumerator LateStart(float time)
    {
        yield return new WaitForSeconds(time);
        Wander();
    }
}
