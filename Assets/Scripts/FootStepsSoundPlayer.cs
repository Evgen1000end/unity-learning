using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSoundPlayer : MonoBehaviour
{
    public AudioSource footStepAudioSource;
    public AudioClip footStepClip;
    private float lastTime = 0;
    private float duration;
    
    void Start()
    {
        duration = footStepClip.length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootStepSound()
    {
        if (lastTime == 0)
        {
            footStepAudioSource.PlayOneShot(footStepClip);
        }

        if (Time.time - lastTime >= duration)
        {
            lastTime = Time.time;
            footStepAudioSource.PlayOneShot(footStepClip);
        }
    }
}
