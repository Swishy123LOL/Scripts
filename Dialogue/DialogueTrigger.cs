using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    [Space]
    public Dialogue dialogue;
    public DialogueTrigger NextDialogue;
    public DialogueTrigger OtherDialogue;
    [Header("Bollean")]
    [Space]
    public bool IsCutscene = false;
    public bool IsNextDialogue;

    [Header("Animation Properties")]
    [Space]

    float currentTime;

    public GameObject[] CsGameobject;

    public GameObject[] Player;

    public CinemachineVirtualCamera[] cam;

    public GameObject[] DialogueTexts;

    public Animator[] Animator;

    public string[] sounds;

    public AudioManager audioManager;
    bool FinalEnd = false;

    public GameObject[] EnableObject;

    [Header("Option")]
    [Space]
    public bool Option;

    public string OptionString1;
    public string OptionString2;

    public int OptionAt;

    public UnityEvent OnOption1;
    public UnityEvent OnOption2;

    [Header("Event")]
    [Space]

    public UnityEvent EndEvent;
    UnityAction endAction;

    [HideInInspector]
    public bool Interactable = false;

    [Header("Item")]
    public string ItemName;
    [TextArea(3, 10)]
    public string ItemDescription;
    [TextArea(3, 10)]
    public string UseDialogue;
    public bool DestroyAfterUse;
    public int Heal;

    void Start()
    {
        //GetScript();
    }
    public void TriggerDialogue ()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        DialogueOption dialogueOption = FindObjectOfType<DialogueOption>();

        dialogueManager.currentSound = dialogue.Sound;
        dialogueManager.StartDialogue(dialogue);
        dialogueManager.StartImage(dialogue);
        dialogueManager.LockDialogue = false;
        dialogueManager.DialogueIndexOption = OptionAt;
        dialogueManager.OptionEnabled = Option;
        dialogueManager.string1 = OptionString1;
        dialogueManager.string2 = OptionString2;
        dialogueManager.iscutscene = IsCutscene;
        dialogueManager.CurrentDialogue = this;
        dialogueManager.NextDialogue = NextDialogue;
        dialogueManager.IsNextDialogue = IsNextDialogue;
        dialogueManager.EndEvent = EndEvent;

        dialogueOption.event1 = OnOption1;
        dialogueOption.event2 = OnOption2;
    }

    void AddScript()
    {
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            if (FindObjectOfType<ScriptManager>().CheckScript(dialogue.sentences[i], gameObject, i) == false)
            {
                FindObjectOfType<ScriptManager>().AddScript(dialogue.sentences[i], gameObject, i);
                Debug.Log(string.Format("Added Script: {0}, {1}, {2}", dialogue.sentences[i], gameObject, i));
            }
        }
    }

    void GetScript()
    {
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            dialogue.sentences[i] = FindObjectOfType<ScriptManager>().GetScript(dialogue.sentences[i], gameObject, i);
            Debug.Log(string.Format("Script Loaded: {0}, {1}, {2}", dialogue.sentences[i], gameObject, i));
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            AddScript();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GetScript();
        }
        if (Input.GetKeyDown(KeyCode.E) && Interactable == true && DialogueManager.IsPlaying == false && IsCutscene == false)
        {
            TriggerDialogue();
            FindObjectOfType<DialogueManager>().iscutscene = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && FinalEnd == true)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentences();

            foreach (GameObject gameObject in Player)
            {
                gameObject.SetActive(true);
            }

            foreach (GameObject gameObject in CsGameobject)
            {
                gameObject.SetActive(false);
            }

            foreach (string sound in sounds)
            {
                FindObjectOfType<AudioManager>().Stop(sound);
            }

            cam[0].Priority = 10;
            cam[1].Priority = 5;

            Destroy(gameObject);
        }


    }
    void OnTriggerEnter2D (Collider2D collider2D)
    {
        Interactable = true;
        FindObjectOfType<DialogueManager>().OnCollision = true;
    }
    void OnTriggerExit2D(Collider2D collider2D)
    {
        Interactable = false;
        FindObjectOfType<DialogueManager>().OnCollision = false;
    }

    #region CutSceneManagement
    public void StartCs()
    {
        foreach (GameObject gameObject in Player)
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in CsGameobject)
        {
            gameObject.SetActive(true);
        }
    }
    public void DisplayNext()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentences();
        FindObjectOfType<DialogueManager>().DisplayNextSprite();
    }

    public void EndCs()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();

        foreach (GameObject gameObject in Player)
        {
            gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in CsGameobject)
        {
            gameObject.SetActive(false);
        }
    }

    public void LockDialogue()
    {
        FindObjectOfType<DialogueManager>().iscutscene = true;
    }
    public void FinalEndCs()
    {
        FinalEnd = true;
    }

    public void GetPosition()
    {
        for (int i = 0; i < Player.Length; i++)
        {
            Player[i].transform.position = new Vector3(CsGameobject[i].transform.position.x, CsGameobject[i].transform.position.y, Player[i].transform.position.z);
        }
    }

    public void SwitchCam()
    {
        cam[0].Priority = 5;
        cam[1].Priority = 10;
    }

    public void SwitchCamBack()
    {
        cam[1].Priority = 5;
        cam[0].Priority = 10;
    }

    public void HideDialogueBox()
    {
        foreach (GameObject game in DialogueTexts)
        {
            game.SetActive(false);
        }
    }

    public void ShowDialogueBox()
    {
        foreach (GameObject game in DialogueTexts)
        {
            game.SetActive(true);
        }
    }

    public void TriggerAnim1()
    {
        Animator[0].SetTrigger("Bool");
    }

    public void TriggerAnim2()
    {
        Animator[1].SetTrigger("Bool");
    }
    
    public void Destroy()
    {
        Collider2D collider2D = GetComponent<Collider2D>();
        Destroy(collider2D);
    }

    #endregion

    public void AddItem()
    {
        PlayerItemManager.AddItem(ItemName, ItemDescription, UseDialogue, DestroyAfterUse, Heal);
    }

    public void triggerDifferentDialogue(DialogueTrigger dialogueTrigger){
        dialogueTrigger.TriggerDialogue();
    }

    public void triggerOtherDialogue(){
        OtherDialogue.TriggerDialogue();
    }

    public void SetOtherDialogue(DialogueTrigger dialogueTrigger){
        dialogueTrigger.OtherDialogue = (dialogueTrigger == null) ? this : OtherDialogue;
    }

    public void Save()
    {
        SaveManager.Save();
    }

    public void DisableObject(string name)
    {
        GameObject.Find(name).SetActive(false);
    }

    public void _EnableObject(string name)
    {
        TimelineControl.FindInGameobject(name).SetActive(true);
    }

    public void PlaySound(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
    }

    public void StopSound(string name)
    {
        FindObjectOfType<AudioManager>().Stop(name);
    }
}
