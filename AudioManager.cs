using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    string crr;

    public static AudioManager instance;
    [System.Serializable]
    public struct AudioProperty
    {
        public float volume;
        public float pitch;
    }
    public AudioProperty audioProperties;
    public bool UseProperties;
    public AudioSource[] audioSources;

    void Awake()
    {
        audioSources = new AudioSource[sounds.Length];

        audioProperties.volume = 1f;
        audioProperties.pitch = 1f;

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].TheSource = gameObject.AddComponent<AudioSource>();
            audioSources[i] = sounds[i].TheSource;
            sounds[i].TheSource.clip = sounds[i].clip;
            sounds[i].TheSource.volume = sounds[i].Volume;
            sounds[i].TheSource.panStereo = sounds[i].Pan;
            sounds[i].TheSource.loop = sounds[i].loop;
            sounds[i].TheSource.pitch = sounds[i].Pitch;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update() {
        if (UseProperties == true) {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].TheSource.volume = audioProperties.volume;
                sounds[i].TheSource.pitch = audioProperties.pitch;
           }        
        }
    }

    public Sound Find(string name){
        return Array.Find(sounds, sound => sound.name == name);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s?.TheSource.Play();
    }
    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s?.TheSource.Stop();
    }

    public void Pause(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s?.TheSource.Pause();
    }

    public float? GetLength(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        return s?.TheSource.clip.samples;
    }

    public float? Time(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        return s?.TheSource.timeSamples;
    }

    public void Resume(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s?.TheSource.UnPause();
    }
    public void SetVolume(string sound,float value){
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s != null) s.TheSource.volume = value;
    }

    public void SetPitch(string sound,float value){
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s != null) s.TheSource.pitch = value;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            if (player.Sound != "" && player.Sound != crr)
            {
                if (crr != null) Stop(crr);
                crr = player.Sound;
                Play(crr);
            }
        }
    }
}
