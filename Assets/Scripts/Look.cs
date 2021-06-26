using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity;

    private float x = 0f;
    private float y = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        x += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        y += Input.GetAxis("Mouse X") * mouseSensitivity;

        //Clamp camera
        x = Mathf.Clamp(x, -90, 90);

        //Rotate camera to axis
        transform.localRotation = Quaternion.Euler(x, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, y, 0);
    }
}
