using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour
{
    private AudioSource audioDevice;
    private int audioInQueue;
    private AudioClip[] audioQueue = new AudioClip[100];
    void Start()
    {
        audioDevice = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioDevice.isPlaying && audioInQueue > 0)
        {
            PlayQueue();
        }
    }
    public void PlayQueue()
    {
        audioDevice.PlayOneShot(audioQueue[0]);
        UnqueueAudio();
    }
    public void UnqueueAudio()
    {
        for (int i = 1; i < audioInQueue; i++)
        {
            audioQueue[i - 1] = audioQueue[i];
        }
        audioInQueue--;
    }
    public void QueueAudio(AudioClip sound)
    {
        audioQueue[audioInQueue] = sound;
        audioInQueue++;
    }
    public void ClearAudioQueue()
    {
        audioInQueue = 0;
    }
}
