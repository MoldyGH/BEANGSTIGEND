using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    public float openingDistance;

    public Transform player;

    public GameManager gameManager;

    public GameObject[] notebookSpawns;

    [SerializeField] bool startingBook;
 
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        notebookSpawns = GameObject.FindGameObjectsWithTag("Spawnpoint");

        if(!startingBook)
        {
            TeleportNotebook();
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit) && (raycastHit.transform.tag == "Notebook" & Vector3.Distance(player.position, base.transform.position) < openingDistance))
            {
                base.transform.position = new Vector3(base.transform.position.x, -20f, base.transform.position.z);
                gameManager.CollectNotebook();
                player.GetComponent<Move>().stamina = 100f;
            }
        }
    }
    public void TeleportNotebook()
    {
        transform.position = notebookSpawns[Random.Range(0, notebookSpawns.Length - 1)].transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Notebook"))
        {
            TeleportNotebook();
        }
    }
}
