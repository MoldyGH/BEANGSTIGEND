using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject screen1;
    public GameObject screen2;

    public string scene;

    public AudioMixer audioMixer;
    public TMP_Text verText;

    public void LoadSceneButton()
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadNewScreen()
    {
        screen1.SetActive(false);
        screen2.SetActive(true);
    }
    public void Volume(float vol)
    {
        audioMixer.SetFloat("volume", vol);
    }
    public void Sensitivity(float sens)
    {
        PlayerPrefs.SetFloat("Sensitivity", sens);
    }
    public void PostProcessing(float weight)
    {
        PlayerPrefs.SetFloat("PostProcessingWeight", weight);
    }
    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }
}
