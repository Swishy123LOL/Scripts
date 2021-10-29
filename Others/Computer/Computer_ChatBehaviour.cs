using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class Computer_ChatBehaviour : MonoBehaviour
{
    [Header("Messages")]
    public Computer_Message[] messages;
    [Space]
    public Computer_Message[] c1messages;
    public Computer_Message[] c2messages;
    [Space]
    public int CurrentMessages;
    int _CurrentMessages;
    public AudioManager audioManager;

    [Space]
    [Header("GameObject")]
    public GameObject messagePref;
    GameObject[] messageGameobjects;
    public RectTransform parrent;
    public RectTransform startPos;

    RectTransform UI;
    float defRes = 0.60375f;
    float scale;

    int _index = 0;
    [Header("Debug")]
    public float time = 1f;


    [Space]
    [Header("Transition")]
    public LeanTweenType TransType;
    public SpriteRenderer BlackCover;
    public DialogueTrigger dialogueTrigger;
    [Header("Corrupt Properties")]
    public Material CorruptMaterial;
    public Material FontMaterial;
    public enum ChatType
    {
        Normal,
        Corrupted
    };
    [Space]
    public ChatType chatType = ChatType.Normal;
    public bool Choice;
    
    void Start() {
        BlackCover = GameObject.Find("BlackCover2").GetComponent<SpriteRenderer>();
        chatType = FindObjectOfType<Computer_ChatHandler>().type;
        Choice = FindObjectOfType<Computer_ChatHandler>().Choice;
        audioManager = FindObjectOfType<AudioManager>();

        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<RectTransform>();
        switch (chatType)
        {
            case ChatType.Normal:
                UpdateMessageGameobj(CurrentMessages);

                messageGameobjects = new GameObject[messages.Length];
                CurrentMessages = FindObjectOfType<Computer_ChatHandler>().Messages;

                _CurrentMessages = CurrentMessages;
                StartCoroutine(SpawnMessageInstant(messages));

                break;

            case ChatType.Corrupted:
                UpdateMessageGameobj(CurrentMessages);

                if (Choice == true) {
                    messageGameobjects = new GameObject[c1messages.Length];
                    CurrentMessages = FindObjectOfType<Computer_ChatHandler>().Messages;

                    _CurrentMessages = CurrentMessages;
                    StartCoroutine(SpawnMessageInstant(c1messages));
                }
                else
                {
                    messageGameobjects = new GameObject[c2messages.Length];
                    CurrentMessages = FindObjectOfType<Computer_ChatHandler>().Messages;

                    _CurrentMessages = CurrentMessages;
                    StartCoroutine(SpawnMessageInstant(c2messages));
                }

                _CurrentMessages = CurrentMessages;
                break;
        }
    }

    IEnumerator SpawnMessageInstant(Computer_Message[] _messages)
    {
        CurrentMessages -= _CurrentMessages;
        for (int i = 0; i < _messages.Length; i++)
        {
            if (i >= _CurrentMessages)
            {
                yield return null;
                
                if (chatType == ChatType.Corrupted && _messages[i].type == Computer_Message.Type.Corrupted){
                    FindObjectOfType<Computer_WindowBehaviour>().PlaysoundWithDifferentAttribute("txtsound" + UnityEngine.Random.Range(1, 4), 1, UnityEngine.Random.Range(0.5f, 1));
                }

                else
                {
                    FindObjectOfType<Computer_WindowBehaviour>().Playsound("txtsound");
                }
            }

            GameObject _gameobject = Instantiate(messagePref, parrent);
            RectTransform rect = _gameobject.GetComponent<RectTransform>();

            messageGameobjects[i] = _gameobject;

            UpdatePos(i);
            Computer_Message.Type type = _messages[i].type;
            switch (type)
            {
                case Computer_Message.Type.Default:

                    Image image = _gameobject.GetComponent<Image>();
                    image.sprite = _messages[i].MessageSprite;

                    TextMeshProUGUI message = _gameobject.GetComponent<Computer_MessageText>().Message;
                    message.text = _messages[i].Message;

                    break;
                case Computer_Message.Type.Corrupted:

                    Image m_image = _gameobject.GetComponent<Image>();
                    m_image.sprite = _messages[i].MessageSprite;
                    m_image.material = CorruptMaterial;

                    TextMeshProUGUI m_message = _gameobject.GetComponent<Computer_MessageText>().Message;
                    m_message.text = _messages[i].Message;
                    m_message.fontMaterial = FontMaterial;

                    break;
                
                case Computer_Message.Type.Option:

                    Image _image = _gameobject.GetComponent<Image>();
                    _image.sprite = _messages[i].MessageSprite;

                    TextMeshProUGUI __message = _gameobject.GetComponent<Computer_MessageText>().Message;
                    __message.text = "";

                    Computer_MessageText _message = _gameobject.GetComponent<Computer_MessageText>();
                    _message.YesButton.gameObject.SetActive(true);
                    _message.NoButton.gameObject.SetActive(true);

                    break;
            }

            LeanTween.scaleX(_gameobject, 1, 1f).setEase(TransType);
            CurrentMessages++;

            if (i == _messages.Length - 1 && chatType == ChatType.Corrupted)
            {
                Debug.Log("Last message!");
                yield return new WaitForSeconds(2.5f);

                FindObjectOfType<Computer_FolderDisplacement>().enabled = false;

                GameObject.Find("ComputerLayout").SetActive(false);
                GameObject.Find("Messages").SetActive(false);
                GameObject.Find("ExitButton").SetActive(false);
                GameObject.Find("Folders").SetActive(false);
                GameObject.Find("BlankCover").SetActive(false);


                GameObject.Find("RenderScreen").GetComponent<RawImage>().enabled = false;

                GetComponent<Image>().enabled = false;

                GameObject.Find("Computer").GetComponent<LoopingAnimationUI>().enabled = false;
                TimelineControl.FindInGameobject("DEBUG")?.SetActive(true);

                Destroy(GameObject.Find("Computer____"));
                Destroy(GameObject.Find("Computer_Frame5"));


                BlackCover.enabled = true;
                FindObjectOfType<Computer_WindowBehaviour>().StopAllSounds();
                yield return new WaitForSeconds(1.5f);

                dialogueTrigger.TriggerDialogue();
                FindObjectOfType<DialogueManager>().OnEndDialogue += TransitionScreen;
            }
        }
    }

    void UpdatePos(int index) {
        scale = UI.localScale.x / defRes;
        if (PlayerSettings.IsFullScreen == true){
            scale /= 1.185185f;
        }

        if (index > 0) {
            RectTransform rect = messageGameobjects[index].GetComponent<RectTransform>();
            rect.position = new Vector3(rect.position.x, startPos.position.y + (rect.rect.height / 2) * scale);

            float y = rect.position.y;
            float o = 1;

            for (int i = index - 1; i > -1; i--)
            {
                RectTransform _rect = messageGameobjects[i].GetComponent<RectTransform>();
                _rect.position = new Vector3(_rect.position.x, y + rect.rect.height * scale * o);
                o++;
            }

            if (index == 4 + _index) 
            {
                messageGameobjects[_index].SetActive(false);
                _index++;
            }
        }
        else {
            RectTransform rect = messageGameobjects[index].GetComponent<RectTransform>();
            rect.position = new Vector3(rect.position.x, startPos.position.y + (rect.rect.height / 2) * scale);
        }
    }

    void UpdateMessageGameobj(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Computer_Message[] des = new Computer_Message[messages.Length];

            Array.Copy(messages, 1, des, 0, messages.Length - 1);
            messages = des;
        }
    }

    void TransitionScreen(){
        StartCoroutine(_TransitionScreen());
    }

    IEnumerator _TransitionScreen(){
        yield return new WaitForSeconds(.5f);
        audioManager.Play("IntroClock_First");
    }
}
