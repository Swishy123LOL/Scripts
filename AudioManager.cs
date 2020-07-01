using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {

        foreach (Sound s in sounds)
        {
            s.TheSource = gameObject.AddComponent<AudioSource>();
            s.TheSource.clip = s.clip;

            s.TheSource.volume = s.Volume;
            s.TheSource.panStereo = s.Pan;
            s.TheSource.loop = s.loop;
            s.TheSource.pitch = s.Pitch;
        }
    }

    void Start()
    {

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.TheSource.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.TheSource.Stop();
    }

    public void Pause(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.TheSource.Pause();
    }

    public void Resume(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.TheSource.UnPause();
    }

    public void PlayGlass()
    {
        Play("Glass_Tower");
    }

    public void StopGlass()
    {
        Stop("Glass_Tower");
    }
}
