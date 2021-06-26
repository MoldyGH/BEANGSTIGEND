using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject[] items;
    private int itemLength;
    // Start is called before the first frame update
    void Start()
    {
        itemLength = items.Length;
        Instantiate(items[UnityEngine.Random.Range(0, itemLength)], transform.position, transform.rotation, transform);
    }
}
