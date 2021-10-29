using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject GameObject;
    public float TextSpeed = 0.01f;
    public static bool IsPlaying = false;
    public static bool moveToSpawn = false;
    public Image Image;
    public GameObject arrow;
    public RawImage rawImage;
    public Animator Animator;

    public bool iscutscene = false;

    Dialogue dialogue;

    private Queue<string> sentences;
    private Queue<Sprite> sprites;
    public GameObject boss;
    string _sentence = "";

    public TextMeshProUGUI m_DialogueText;
    public int CurrentDialogueIndex;
    public int DialogueIndexOption;
    public bool OptionEnabled;
    public string string1;
    public string string2;
    public bool LockDialogue;
    public bool LockLockDialogue; //please don't ask...

    public bool IsThereBossCauseImLikeToUseBadCodes = false;

    public DialogueTrigger NextDialogue;
    public DialogueTrigger CurrentDialogue;
    public bool IsNextDialogue;
    public UnityEvent EndEvent;

    AudioManager DialogueAudioManager;
    public string currentSound;

    DialogueRead dialogueRead;

    public int CurrentCharIndex;

    [Space]

    public bool OnCollision;

    //Delegates
    public delegate void EndDialogueDelegate();
    public event EndDialogueDelegate OnEndDialogue;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject.SetActive(false);
        sentences = new Queue<string>();
        sprites = new Queue<Sprite>();
        DialogueAudioManager = GameObject.Find("Save Manager").GetComponent<AudioManager>();

        dialogueRead = FindObjectOfType<DialogueRead>();
    } 

    public void StartDialogue(Dialogue dialouge)
    {
        nameText.text = dialouge.name;
        IsPlaying = true;
        CurrentDialogueIndex = -1;

        if (IsThereBossCauseImLikeToUseBadCodes == true)
        {
            if (boss.activeSelf == true)
            {
                Color color = Color.clear;
                m_DialogueText.color = color;
                nameText.color = color;
                Image.color = color;
                rawImage.color = color;
            }
        }
        GameObject.gameObject.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        if (currentSound == "" || currentSound == null)
        {
            currentSound = "txt";
        }

        dialogueRead.onFinal += delegate {arrow.SetActive(true);};
        DisplayNextSentences(true);
    }
    public void StartImage(Dialogue dialogue)
    {
        foreach (Sprite sprite in dialogue.sprites)
        {
            sprites.Enqueue(sprite);
        }
        DisplayNextSprite(true);
    }
    public void DisplayNextSentences(bool ignoreCheck = false)
    {
        if (dialogueText.text == dialogueRead.GetDialogueInstant(_sentence) || ignoreCheck){
            DisplayNextSprite();
            arrow.SetActive(false);

            CurrentCharIndex = 0;
            DialogueRead dlr = FindObjectOfType<DialogueRead>();
            dlr.StopAllCoroutines();

            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();

            StopAllCoroutines();
            TypeSentence(sentence);

            CurrentDialogueIndex++;
            if (CurrentDialogueIndex == DialogueIndexOption & OptionEnabled == true)
            {
                LockDialogue = true;
                FindObjectOfType<DialogueOption>().Open(string1, string2);
            }
        }

        else{
            dialogueText.text = dialogueRead.GetDialogueInstant(_sentence);
            dialogueRead.StopAllCoroutines();

            arrow.SetActive(true);
        }
    }

    public void DisplayNextSprite(bool ignoreCheck = false)
    {
        if (dialogueText.text == _sentence || ignoreCheck){
            if (sprites.Count > 0){
                Sprite sprite = sprites.Dequeue();
                Image.sprite = sprite;
            }
        }
    }

    void TypeSentence(string sentence)
    {
        _sentence = sentence;
        
        dialogueText.text = "";

        dialogueRead.ReturnDialogue(sentence, dialogueText, TextSpeed, DialogueAudioManager);

        CurrentCharIndex = 0;

        StopAllCoroutines();
    }

    public void EndDialogue(bool invokeEvent = true)
    {
        if (invokeEvent) OnEndDialogue?.Invoke();
        
        StopAllCoroutines();
        if (EndEvent != null)
        {
            EndEvent.Invoke();
        }
        
        GameObject.SetActive(false);
        Animator.SetBool("Bool", true);
        if (IsNextDialogue == true)
        {
            CurrentDialogue.enabled = false;
            NextDialogue.enabled = true;
        }
        IsPlaying = false;
    }
 

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !iscutscene && !LockDialogue && IsPlaying && !LockLockDialogue)
        {
            DisplayNextSentences();
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

    public void SetCutSceneBool(bool theBool)
    {
        LockDialogue = theBool;
    }
}
