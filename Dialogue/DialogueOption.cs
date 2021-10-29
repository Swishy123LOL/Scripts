using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueOption : MonoBehaviour
{
    Animator animator;
    public TextMeshProUGUI string1;
    public TextMeshProUGUI string2;
    string _text1;
    string _text2;
    public delegate void OnOption1();
    public int currentInput;
    public UnityEvent event1;
    public UnityEvent event2;
    bool opened;
    void Start()
    {
        animator = GetComponent<Animator>();
        string1.enabled = false;
        string2.enabled = false;
        opened = false;
    }

    void Update()
    {
        if (string1.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                currentInput -= 1;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                currentInput += 1;
            }
        }

        if (currentInput % 2 == 0 && Input.GetKeyDown(KeyCode.Space) && opened == true)
        {
            Option1();
        }

        else if (currentInput % 2 != 0 && Input.GetKeyDown(KeyCode.Space) && opened == true)
        {
            Option2();
        }

        if (currentInput % 2 == 0)
        {
            string2.color = Color.white;
            string1.color = Color.yellow;
        }

        else
        {
            string1.color = Color.white;
            string2.color = Color.yellow;
        }
    }
    public void Open(string text1, string text2)
    {
        animator.SetBool("Open", true);
        string1.enabled = true;
        string2.enabled = true;
        _text1 = text1;
        _text2 = text2;
    }

    public void Close()
    {
        FindObjectOfType<DialogueManager>().LockDialogue = false;
        animator.SetBool("Close", true);
        string1.text = "";
        string2.text = "";
        opened = false;
    }

    public void Back()
    {
        animator.SetBool("Open", false);
        animator.SetBool("Close", false);
    }

    public void SetString()
    {
        string1.text = _text1;
        string2.text = _text2;
        opened = true;
    }

    public void Option1()
    {
        event1.Invoke();
        Close();
    }

    public void Option2()
    {
        event2.Invoke();
        Close();
    }
}
