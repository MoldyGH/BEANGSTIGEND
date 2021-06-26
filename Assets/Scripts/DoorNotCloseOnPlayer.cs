using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNotCloseOnPlayer : MonoBehaviour
{
    public Collider player;
    public Door door;
    public bool isTouchingDoor;
    public float openTime = 1.5f;

    void OnTriggerExit(Collider other)
    {
        if(door.doorOpen & other == player)
        {
            door.CloseDoor();
            isTouchingDoor = false;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (door.doorOpen & other == player)
        {
            isTouchingDoor = true;
        }
    }
    void Update()
    {
        if(!isTouchingDoor && door.doorOpen)
        {
            openTime -= 1f * Time.deltaTime;

            if(openTime <= 0f)
            {
                door.CloseDoor();
            }
        }
    }
}
