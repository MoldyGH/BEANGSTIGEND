using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Subtitles : MonoBehaviour
{
    public GameObject[] textBox;
    public TMP_Text[] subtitleTextBoxes;

    public AudioSource[] characterAudioSources;
    public AudioClip[] characterAudio;
    private void Start()
    {
    }
    private void Update()
    {
        BeanSubtitles();
        BeanGameSubtitles();
    }
    public void BeanSubtitles()
    {
        if(characterAudioSources[0].isPlaying)
        {
            textBox[0].SetActive(true);
            if(characterAudioSources[0].clip = characterAudio[0])
            {
                subtitleTextBoxes[0].text = "Hello. Welcome to the computer class. Please use the computer in next classroom and answer few questions.";
            }
        }
        else
        {
            textBox[0].SetActive(false);
        }
    }
    public void BeanGameSubtitles()
    {
        if (characterAudioSources[0].isPlaying)
        {
            textBox[0].SetActive(true);
            if(characterAudioSources[0].clip = characterAudio[1])
            {
                subtitleTextBoxes[0].text = "Problem 1: In first problem you must do what I said earlier.";
            }
            else if(characterAudioSources[0].clip = characterAudio[2])
            {
                subtitleTextBoxes[0].text = "Problem 2: Repeat instructions. Also do not make mistakes because they anger me very much.";
            }
            else if(characterAudioSources[0].clip = characterAudio[3])
            {
                subtitleTextBoxes[0].text = "Problem 3: You are final problem. Answer this final question and answer other 10 computer problems.";
            }
            else if(characterAudioSources[0].clip = characterAudio[4])
            {
                subtitleTextBoxes[0].text = "Nice job! You complete my tasks.";
            }
        }
        else
        {
            textBox[0].SetActive(false);
        }
    }
    
}
