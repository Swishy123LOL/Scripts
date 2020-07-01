using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    [Range(0f ,2f)]
    public float Volume;

    [Range(-1f, 1f)]
    public float Pan;

    [Range(1f, 2f)]
    public float Pitch = 1f;

    [HideInInspector]
    public AudioSource TheSource;

    public bool loop;
}
