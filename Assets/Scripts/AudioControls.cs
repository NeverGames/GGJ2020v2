using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioControls : MonoBehaviour
{
    AudioSource audio;

    [SerializeField]
    private float a_soundsVolume;

    [SerializeField]
    private bool playRandomly = false;

    [SerializeField]
    private float playChance;
    [SerializeField]
    private float playTimerReset;
    private float playAudioTimer = 0.0f;


    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audio.volume = a_soundsVolume;
    }

    private void Update()
    {
        AudioRandomPlay();
    }


    private void AudioRandomPlay ()
    {
        if (!playRandomly)
            return;

        playAudioTimer -= Time.deltaTime;

        if (playAudioTimer <= 0)
        {
            int rng = Random.Range(0, 10);

            if (playChance <= rng)
            {
                audio.PlayOneShot(audio.clip);
            } else
            {
                playAudioTimer = playTimerReset;
            }

        }
    }


}
