using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public int notebookCount;
    public TMP_Text notebookText;

    public bool evilMode;

    public AudioSource bean_aud;

    public AudioSource schoolMusic;

    public GameObject teacherScary;

    public GameObject[] notebooks;

    public GameObject teacherNormal;
    public float currentTeacherSpeed;
    public NavMeshAgent teacher;
    public Animator teacherWalkAnim;
    public float currentAnimSpeed;
    public GameObject bowlingBall, monitor;
    public AudioSource ambience;
    public Color creepyColor;
    public AudioClip chase_intro;
    public AudioClip chase_loop;
    public float fps;
    public TMP_Text fpsText;
    public AudioSource chaseMusic;
    public bool chase;
    public Transform playerTransform;
    public float timeUntilChase;
    public bool preChase;
    public bool endChase;
    public AudioClip bean_end;
    public GameObject teacherCalm, teacherScaryGM;
    public Beangstigend teacherScript;
    public PostProcessVolume postProcessVol;

    public float timeToOpenDoor;

    public float updatedSoundTime;

    //Debug
    public bool isTestingChase;
    public bool endAudioHasPlayed = false;

    public DiscordController discord;
    public bool isUsingDiscord;

    void Start()
    {
        int notebooksUpdated = notebooks.Length - 1;
        notebooks = GameObject.FindGameObjectsWithTag("Notebook");
        UpdateNotebookCount();
        StartCoroutine(LateStart(0.01f));
        postProcessVol.weight = PlayerPrefs.GetFloat("PostProcessingWeight");
        if(isUsingDiscord)
        {
            discord.UpdatePresence("Playing Level 1", notebookCount + "/" + notebooksUpdated + " Computers");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LockMouse(false);
            SceneManager.LoadScene("Menu");
        }
        if(!teacherScript.flashed)
        {
            teacher.speed = currentTeacherSpeed;
            teacherWalkAnim.speed = currentAnimSpeed;
        }
        else if(teacherScript.flashed)
        {
            teacher.speed = 20f;
            teacherWalkAnim.speed = 15;
        }
        if(preChase)
        {
            timeUntilChase -= 1f * Time.deltaTime;
        }
        if (timeUntilChase <= 0f & preChase)
        {
            chaseMusic.clip = chase_loop;
            chaseMusic.loop = true;
            chaseMusic.Play();
            chase = true;
            preChase = false;
            teacher.SetDestination(playerTransform.position);
        }
        if (chase)
        {
            teacher.isStopped = false;
            teacher.SetDestination(playerTransform.position);
            currentTeacherSpeed = 12;
            currentAnimSpeed = 6f;
            playerTransform.GetComponent<Move>().stamina = 100f;
        }
        if(endChase)
        {
            chase = false;
            chaseMusic.gameObject.SetActive(false);
            teacherCalm.gameObject.SetActive(true);
            teacherCalm.gameObject.transform.position = teacherScaryGM.gameObject.transform.position;
            teacherScaryGM.gameObject.SetActive(false);
            bean_aud.clip = bean_end;
            if(!endAudioHasPlayed)
            {
                bean_aud.Play();
                endAudioHasPlayed = true;
            }
        }

        if(notebookCount == notebooks.Length)
        {
            endChase = true;
        }
    }

    // Update is called once per frame
    public void UpdateNotebookCount()
    {
        int notebooksUpdated = notebooks.Length - 1;
        notebookText.text = "Computers: " + notebookCount + "/" + notebooksUpdated;
        if(isUsingDiscord)
        {
            discord.UpdatePresence("Playing Level 1", notebookCount + "/" + notebooksUpdated + " Computers");
        }
        if (notebookCount == notebooksUpdated)
        {
            timeUntilChase = 32f;
            preChase = true;
            RenderSettings.fog = false;
            teacher.isStopped = true;
            ambience.Stop();
            chaseMusic.clip = chase_intro;
            chaseMusic.Play();
        }
        if(chase || preChase)
        {
            notebookText.text = "Computers: " + notebookCount + "/" + notebooks.Length;
            notebookText.color = Color.red;
        }
    }
    public void CollectNotebook()
    {
        notebookCount++;
        currentTeacherSpeed = currentTeacherSpeed + 1.5f;
        currentAnimSpeed = currentAnimSpeed + 0.5f;
        updatedSoundTime = teacherScript.soundTime - 0.1f;
        timeToOpenDoor = timeToOpenDoor - 1;
        teacherScript.doorPause = timeToOpenDoor;
        teacherScript.soundTime = updatedSoundTime;
        UpdateNotebookCount();

        //mark

        if(notebookCount == 2)
        {
            ActivateEvilMode();
        }
    }
    public void LockMouse(bool lockStateVoid)
    {
        if(lockStateVoid)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void ActivateEvilMode()
    {
        evilMode = true;
        RenderSettings.fog = true;
        teacherScary.SetActive(true);
        bowlingBall.SetActive(true);
        monitor.SetActive(true);
        teacherNormal.SetActive(false);
        teacherScript.soundTime = 0.333f;
        RenderSettings.ambientLight = creepyColor;
        schoolMusic.Stop();
        ambience.Play();

    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        notebooks = GameObject.FindGameObjectsWithTag("Notebook");
        UpdateNotebookCount();
    }
    public void TeleportPlayer(Vector3 position)
    {
        playerTransform.position = position;
    }

}
