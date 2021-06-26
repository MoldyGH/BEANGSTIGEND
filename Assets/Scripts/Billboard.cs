using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{

	void Start ()
    {
        if (Billboard.cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        }
        freeRotation.y = 1;
        if(needsSpriteMaterial)
        {
            sprMaterial = GameObject.Find("MaterialGet").GetComponent<SpriteRenderer>().material;
            base.GetComponent<SpriteRenderer>().material = sprMaterial;
        }
    }

    public static Transform cam;
    public Vector3 freeRotation;
    Vector3 eangles = Vector3.zero;
    [SerializeField] Material sprMaterial;
    public bool needsSpriteMaterial = true;


    void LateUpdate()
    {
        this.transform.LookAt(Billboard.cam);
        transform.Rotate(0, 180, 0);
        eangles = transform.eulerAngles;
        eangles.x *= freeRotation.x;
        eangles.y *= freeRotation.y;
        eangles.z *= freeRotation.z;
        transform.eulerAngles = eangles;
    }
}
