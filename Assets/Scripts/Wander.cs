using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public GameObject[] newLocation;
    public int id;

    private void Start()
    {
        newLocation = GameObject.FindGameObjectsWithTag("WanderPoints");
    }
    public void GetNewTarget()
    {
        id = Mathf.RoundToInt(Random.Range(0f, newLocation.Length - 1));
    }
}
