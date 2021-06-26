using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Warning : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform stop;
    public Transform afterContiune;

    public AudioSource beanAudio;
    public AudioClip exit;

    public GameObject buttons;

    void Start()
    {
        agent.SetDestination(stop.position);
        StartCoroutine(PlayAudio(5.4f));
    }

    // Update is called once per frame
    IEnumerator PlayAudio(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        beanAudio.Play();
        StartCoroutine(ShowButtons(18.747f));
    }
    IEnumerator ShowButtons(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        buttons.SetActive(true);
    }
    public void Continue()
    {
        buttons.SetActive(false);
        beanAudio.clip = exit;
        beanAudio.Play();
        agent.SetDestination(afterContiune.position);
        StartCoroutine(MenuWait(5.4f));
    }
    IEnumerator MenuWait(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
