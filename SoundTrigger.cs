using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundTrigger : MonoBehaviour
{
    public string Sound;
    void Start()
    {
        //use for testing
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name == "Character")
        {
            FindObjectOfType<AudioManager>().Play(Sound);
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name == "Character")
        {
            FindObjectOfType<AudioManager>().Stop(Sound);
        }
    }
}
