using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTriggerForDoor : MonoBehaviour
{
    void Start()
    {
        base.GetComponentInChildren<Door>().trigger = base.GetComponentInChildren<DoorNotCloseOnPlayer>();
    }
}
