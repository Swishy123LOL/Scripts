using System;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene_ButtonBehaviour : MonoBehaviour
{
    public SpriteRenderer[] Buttons;
    public Sprite[] ButtonsPressed;

    [Space]

    public SpriteRenderer ButtonE;
    public Sprite ButtonE_Highlighted;
    Sprite ButtonE_Normal;

    Dictionary<int, bool> hasPressed = new Dictionary<int, bool>();

    [Space]

    public AudioManager audioManager;
    public string PressSound;
    public string CompleteSound;

    [Space]

    public KeyCode[] InputKeys;

    [Space]

    public Animator animator;
    [Space]
    public GameObject UIComputer;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        for (int i = 0; i < Buttons.Length; i++)
        {
            hasPressed.Add(i, false);
        }

        ButtonE_Normal = ButtonE.sprite;

        FindObjectOfType<PlayerSettings>().introfullscreen = false;
    }

    void Update()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if(Input.GetKeyDown(InputKeys[i]) && returnValue(i) == false)
            {
                Buttons[i].sprite = ButtonsPressed[i];
                audioManager.Play(PressSound);

                hasPressed.Remove(i);
                hasPressed.Add(i, true);

                if (returnValueAll() == true)
                {
                    audioManager.Play(CompleteSound);
                    animator.SetBool("1", true);
                }
            }
        }

        if (ButtonE.enabled == true)
        {
            if (FindObjectOfType<DialogueManager>().OnCollision == true)
            {
                ButtonE.sprite = ButtonE_Highlighted;
            }

            else { ButtonE.sprite = ButtonE_Normal; }
        }
    }

    bool returnValue(int key)
    {
        return hasPressed[key];
    }

    bool returnValueAll()
    {
        bool value = true;
        foreach (var press in hasPressed)
        {
            if (press.Value == false)
            {
                value = false;
            }
        }

        return value;
    }
}
