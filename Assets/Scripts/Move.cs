using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    public float movementSpeed;
    public float sprintModifier;
    public bool isWalking;

    public float stamina = 100f;
    public float staminaRate = 20f;
    float maxStamina = 100f;
    public Slider staminaBar;
    public float deathTimer;
    public bool isDead;
    private float movementCounter;
    private float idleCounter;
    public float jumpForce;

    private Vector3 cameraOrigin;
    private Vector3 targetBobPos;

    public CapsuleCollider beangstigendCollider;
    public Beangstigend beangstigend;

    //Debug
    [SerializeField] float rigidbodyVelocity;

    // Monitor stuff
    public float guilt;
    public string guiltType;

    void Start()
    {
        cameraOrigin = Camera.main.gameObject.transform.localPosition;
    }
    // Update is called once per frame

    private void Update()
    {
        CheckIfWalking();
        //Headbob
        if(isDead)
        {
            deathTimer -= 1f * Time.deltaTime;
            if(deathTimer <= 0f)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
    void FixedUpdate()
    {
        //Input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Booleans
        bool sprint = Input.GetKey(KeyCode.LeftShift) & stamina >= 0f || Input.GetKey(KeyCode.RightShift) & stamina >= 0f;
        bool isSprinting = sprint && y > 0;

        //Movement
        Vector3 direction = new Vector3(x, 0, y);
        direction.Normalize();

        float adjustedSpeed = movementSpeed;
        if (isSprinting)
        {
            adjustedSpeed *= sprintModifier;
        }
        else
        {
            adjustedSpeed = movementSpeed;
        }

        Vector3 targetVelocity = transform.TransformDirection(direction) * adjustedSpeed * Time.deltaTime;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = targetVelocity;

        //Stamina bar (referenced from baldi)
        if (isSprinting)
        {
            if (stamina > 0f)
            {
                stamina -= staminaRate * Time.deltaTime;
            }
            if (stamina < 0f & stamina > -5f)
            {
                stamina = -5f;
            }
        }
        else if (stamina < maxStamina && !isWalking)
        {
            stamina += staminaRate * Time.deltaTime;
        }
        staminaBar.value = stamina / maxStamina * 100f;

        //Headbob
        if(x == 0 && y == 0 || rigidbodyVelocity == 0f)
        {
            HeadBob(idleCounter, 0.15f, 0.15f);
            idleCounter += Time.deltaTime;
            Camera.main.gameObject.transform.localPosition = Vector3.Lerp(Camera.main.gameObject.transform.localPosition, targetBobPos, Time.deltaTime * 2f);
        }
        else if(!isSprinting)
        {
            HeadBob(movementCounter, 0.2f, 0.2f);
            movementCounter += Time.deltaTime * 6f;
            Camera.main.gameObject.transform.localPosition = Vector3.Lerp(Camera.main.gameObject.transform.localPosition, targetBobPos, Time.deltaTime * 8f);
        }
        else
        {
            HeadBob(movementCounter, 0.3f, 0.3f);
            movementCounter += Time.deltaTime * 10f;
            Camera.main.gameObject.transform.localPosition = Vector3.Lerp(Camera.main.gameObject.transform.localPosition, targetBobPos, Time.deltaTime * 10f);
        }
        if(isSprinting)
        {
            ResetGuilt("running", 0.01f);
        }

        //Debug
        rigidbodyVelocity = rb.velocity.magnitude;
    }
    public void CheckIfWalking()
    {
        if (rigidbodyVelocity > 9f && rigidbodyVelocity < 20f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    public void Die()
    {
        Camera.main.gameObject.AddComponent<Rigidbody>();
        Camera.main.gameObject.AddComponent<CapsuleCollider>();
        isDead = true;
    }
    void HeadBob(float p_z, float p_x_intensity, float p_y_intensity)
    {
        targetBobPos = cameraOrigin + new Vector3(Mathf.Cos(p_z) * p_x_intensity, Mathf.Sin(p_z * 2) * p_y_intensity, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MainTeacher"))
        {
            if(!beangstigend.flashed)
            {
                beangstigend.TriggerJumpscare();
            }
        }
    }
    public void GuiltCheck()
    {
        if(guilt > 0f)
        {
            guilt -= Time.deltaTime;
        }
    }
    public void ResetGuilt(string type, float amount)
    {
        if(amount >= guilt)
        {
            guilt = amount;
            guiltType = type;
        }
    }
}
