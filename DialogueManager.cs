using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using System.Diagnostics.Contracts;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject GameObject;
    public float TextSpeed = 0.01f;
    public static bool IsPlaying = false;
    public static bool moveToSpawn = false;
    public Image Image;
    public RawImage rawImage;
    public Animator Animator;

    public bool iscutscene = false;

    Dialogue dialogue;

    private Queue<string> sentences;
    private Queue<Sprite> sprites;
    public GameObject boss;

    public TextMeshProUGUI m_DialogueText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.SetActive(false);
        sentences = new Queue<string>();
        sprites = new Queue<Sprite>();
    } 

    public void StartDialogue(Dialogue dialouge)
    {
        nameText.text = dialouge.name;
        IsPlaying = true;

        if (boss.activeSelf == true)
        {
            Color color = Color.clear;
            m_DialogueText.color = color;
            nameText.color = color;
            Image.color = color;
            rawImage.color = color;
        }
        GameObject.gameObject.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentences();
    }
    public void StartImage(Dialogue dialogue)
    {
        foreach (Sprite sprite in dialogue.sprites)
        {
            sprites.Enqueue(sprite);
        }
        DisplayNextSprite();
    }
    public void DisplayNextSentences()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSprite()
    {
        if (sprites.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sprite sprite = sprites.Dequeue();
        Image.sprite = sprite;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            FindObjectOfType<AudioManager>().Play("Blip");
            dialogueText.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }
    }
    public void EndDialogue()
    {
        if (BlockedArea.Ignored == true)
        {
            moveToSpawn = true;
        }
        IsPlaying = false;
        GameObject.gameObject.SetActive(false);
        Animator.SetBool("Bool", true);
    }
 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && iscutscene == false)
        {
            DisplayNextSentences();
            DisplayNextSprite();
        }

        else if (iscutscene == true)
        {
            IsPlaying = false;
        }

    }

    IEnumerator Wait (float time)
    {
        yield return new WaitForSeconds(time);

        DisplayNextSentences();
        DisplayNextSprite();
    }
}
