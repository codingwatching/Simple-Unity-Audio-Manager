﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : BaseAudioFeedback
{
    [SerializeField]
    bool playOnStart;

    [SerializeField]
    bool playOnEnable;

    [Tooltip("Play sound after this long")]
    [SerializeField]
    float delay = 0;

    [SerializeField]
    bool stopOnDisable = true;

    [SerializeField]
    bool stopOnDestroy = true;

    [SerializeField]
    bool loopSound = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (playOnStart)
        {
            Play();
        }

        if (am == null) am = AudioManager.GetInstance();
    }

    public void Play()
    {
        if (soundFile != null)
        {
            am.PlaySoundOnce(soundFile, transform, priority, pitchShift, delay);
        }
        else
        {
            am.PlaySoundOnce(sound, transform, priority, pitchShift, delay);
        }
    }

    public void Stop()
    {
        if (soundFile != null)
        {
            if (am.IsSoundPlaying(soundFile, transform))
            {
                am.StopSound(soundFile, transform);
            }
        }
        else
        {
            if (am.IsSoundPlaying(sound, transform))
            {
                am.StopSound(sound, transform);
            }
        }
    }

    private void OnEnable()
    {
        if (playOnEnable)
        {
            StartCoroutine(PlayOnEnable());
        }

        am = AudioManager.GetInstance();
    }

    IEnumerator PlayOnEnable()
    {
        while (!AudioManager.GetInstance())
        {
            yield return new WaitForEndOfFrame();
        }
        while (!AudioManager.GetInstance().Initialized())
        {
            yield return new WaitForEndOfFrame();
        }

        am = AudioManager.GetInstance();

        Play();
    }

    private void OnDisable()
    {
        if (stopOnDisable)
        {
            Stop();
        }
    }

    private void OnDestroy()
    {
        if (stopOnDestroy)
        {
            Stop();
        }
    }
}
