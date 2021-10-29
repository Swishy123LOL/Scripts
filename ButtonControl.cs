using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ButtonControl : MonoBehaviour
{
    [Header("Main")]
    public TextMeshProUGUI[] texts;
    TextMeshProUGUI textContainer;
    TextMeshProUGUI preTextContainer;
    [Header("Optional")]
    public ButtonControl Above;
    public ButtonControl Below;
    public UnityEvent EventWhenActive;
    [Space]
    public ButtonControl[] Next;
    public KeyCode NextKey = KeyCode.Space;
    [Space]
    public ButtonControl Back;
    public KeyCode BackKey = KeyCode.LeftShift;
    bool next;
    [Space]
    public bool InvertInput;
    public bool LoopAround;
    public bool HaveEmptyString;
    public bool HaveOneNext;
    public bool HaveNext;
    bool _LoopAround;
    [Header("Scroll")]
    public bool Scroll;
    public int maxScrollength;
    TextMeshProUGUI currentLastText;
    TextMeshProUGUI lastText;
    TextMeshProUGUI currentFirstText;
    TextMeshProUGUI firstText;
    Vector2[] textsPos;
    [Header("Event")]
    public UnityEvent[] NextEvent;
    public UnityEvent BackEvent;
    public UnityEvent[] Event;
    [HideInInspector]
    public int length;

    public int currentIndex;
    Color[] defaultColors;
    bool stop;
    KeyCode[] leftKeys;
    KeyCode[] rightKeys;
    KeyCode[] upKeys;
    KeyCode[] downKeys;
    KeyCode[] container;
    public int[,] test;

    [HideInInspector]
    public bool FREEZE;
    void Start()
    {
        _LoopAround = !LoopAround;
        container = new KeyCode[2];

        leftKeys = new KeyCode[2];
        leftKeys[0] = KeyCode.D;
        leftKeys[1] = KeyCode.RightArrow;

        rightKeys = new KeyCode[2];
        rightKeys[0] = KeyCode.A;
        rightKeys[1] = KeyCode.LeftArrow;

        upKeys = new KeyCode[2];
        upKeys[0] = KeyCode.W;
        upKeys[1] = KeyCode.UpArrow;

        downKeys = new KeyCode[2];
        downKeys[0] = KeyCode.S;
        downKeys[1] = KeyCode.DownArrow;

        if (InvertInput == true)
        {
            for (int i = 0; i < 2; i++)
            {
                container[i] = leftKeys[i];
                leftKeys[i] = downKeys[i];
                downKeys[i] = container[i];
            }
            for (int i = 0; i < 2; i++)
            {
                container[i] = rightKeys[i];
                rightKeys[i] = upKeys[i];
                upKeys[i] = container[i];
            }
        }

        currentIndex = 1;
        length = texts.Length;
        lastText = texts[texts.Length - 1];
        firstText = texts[0];

        defaultColors = new Color[texts.Length];

        textsPos = new Vector2[maxScrollength];
        for (int i = 0; i < maxScrollength; i++)
        {
            textsPos[i] = texts[i].transform.position;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            defaultColors[i] = texts[i].color;
        }

        if (Scroll == true)
        {
            for (int i = maxScrollength; i < texts.Length; i++)
            {
                texts[i].enabled = false;
            }
        }

        if (Next != null)
        {
            NextEvent = new UnityEvent[Next.Length];
        }
    }

    void Update()
    {
        if (FREEZE == false)
        {
            if (currentIndex > 0)
            {
                if (texts[currentIndex - 1].text == "")
                {
                    currentIndex -= 1;
                }
            }
            if (Event.Length > 0)
            {
                if (Event[currentIndex - 1] != null)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Event[currentIndex - 1].Invoke();
                    }
                }
            }

            if (EventWhenActive != null && enabled == true)
            {
                EventWhenActive.Invoke();
            }

            if (HaveNext == true)
            {
                if (Input.GetKeyDown(NextKey))
                {
                    TriggerNext();
                }
            }

            if (Back != null)
            {
                if (Input.GetKeyDown(BackKey))
                {
                    next = true;
                    texts[currentIndex - 1].color = defaultColors[currentIndex - 1];
                    Back.enabled = true;
                    if (BackEvent != null)
                    {
                        BackEvent.Invoke();
                    }
                    Back.next = false;
                    Back.stop = false;
                    enabled = false;
                }
            }
            if (Scroll == true)
            {
                currentLastText = texts[maxScrollength - 1];
                currentFirstText = texts[0];
                for (int i = maxScrollength; i < texts.Length; i++)
                {
                    texts[i].enabled = false;
                }
                for (int i = 0; i < maxScrollength; i++)
                {
                    texts[i].enabled = true;
                    texts[i].transform.position = textsPos[i];
                }
                if (currentIndex > maxScrollength && _LoopAround == false || currentIndex > maxScrollength && _LoopAround == true && currentLastText != lastText && texts[currentIndex - 1].text != "")
                {
                    if (texts.Length > 2)
                    {
                        currentIndex = maxScrollength;
                        textContainer = texts[texts.Length - 2];
                        texts[texts.Length - 2] = texts[texts.Length - 1];
                        texts[texts.Length - 1] = texts[0];

                        for (int i = texts.Length - 3; i > -1; i--)
                        {
                            preTextContainer = texts[i];
                            texts[i] = textContainer;
                            textContainer = preTextContainer;
                        }
                    }
                }
                if (currentIndex < 1 && _LoopAround == false || currentIndex < 1 && currentFirstText != firstText)
                {
                    currentIndex = 1;
                    textContainer = texts[1];
                    texts[1] = texts[0];
                    texts[0] = texts[texts.Length - 1];

                    for (int i = 2; i < texts.Length; i++)
                    {
                        preTextContainer = texts[i];
                        texts[i] = textContainer;
                        textContainer = preTextContainer;
                    }
                }
            }
            if (Above != null)
            {
                if (Input.GetKeyDown(downKeys[0]) || Input.GetKeyDown(downKeys[1]))
                {
                    stop = true;
                    texts[currentIndex - 1].color = defaultColors[currentIndex - 1];
                    Above.enabled = true;
                    Above.next = false;
                    Above.stop = false;
                    Above.currentIndex = currentIndex;
                    enabled = false;
                }
            }
            if (Below != null)
            {
                if (Input.GetKeyDown(upKeys[0]) || Input.GetKeyDown(upKeys[1]))
                {
                    stop = true;
                    texts[currentIndex - 1].color = defaultColors[currentIndex - 1];
                    Below.enabled = true;
                    Below.next = false;
                    Below.stop = false;
                    Below.currentIndex = currentIndex;
                    enabled = false;
                }
            }
            if (Input.GetKeyDown(leftKeys[0]) || Input.GetKeyDown(leftKeys[1]))
            {
                if (HaveEmptyString == true)
                {
                    if (texts[currentIndex].text != "")
                    {
                        currentIndex += 1;
                        if (currentIndex == length + 1 && _LoopAround == false)
                        {
                            currentIndex = 1;
                        }
                        else if (currentIndex == length + 1 && _LoopAround == true && Scroll == false)
                        {
                            currentIndex = length;
                        }
                        else if (currentIndex == maxScrollength + 1 && _LoopAround == true && Scroll == true && currentLastText == lastText)
                        {
                            currentIndex = maxScrollength;
                        }
                    }
                }

                else
                {
                    currentIndex += 1;
                    if (currentIndex == length + 1 && _LoopAround == false)
                    {
                        currentIndex = 1;
                    }
                    else if (currentIndex == length + 1 && _LoopAround == true && Scroll == false)
                    {
                        currentIndex = length;
                    }
                    else if (currentIndex == maxScrollength + 1 && _LoopAround == true && Scroll == true && currentLastText == lastText)
                    {
                        currentIndex = maxScrollength;
                    }
                }
            }
            if (Input.GetKeyDown(rightKeys[0]) || Input.GetKeyDown(rightKeys[1]))
            {
                currentIndex -= 1;
                if (currentIndex == 0 && Scroll == false && _LoopAround == false)
                {
                    currentIndex = length;
                }
                else if (currentIndex == 0 && _LoopAround == true && Scroll == false)
                {
                    currentIndex = 1;
                }
                else if (currentIndex == 0 && _LoopAround == true && Scroll == true && currentFirstText == firstText)
                {
                    currentIndex = 1;
                }
            }
            if (stop == false && next == false)
            {
                for (int i = 0; i < texts.Length; i++)
                {
                    if (currentIndex - 1 == i)
                    {
                        texts[i].color = Color.yellow;
                    }
                    else if (currentIndex - 1 != i)
                    {
                        texts[i].color = defaultColors[i];
                    }
                }
            }
        }
    }

    public string GetCurrentTextString()
    {
        string text = texts[currentIndex - 1].text;
        return text;
    }

    public void TriggerNext()
    {
        if (HaveOneNext == true)
        {
            if (Next.Length > 0 && Next[0].texts[0].text != "")
            {
                next = true;
                texts[currentIndex - 1].color = defaultColors[currentIndex - 1];
                Next[0].enabled = true;
                if (NextEvent[0] != null)
                {
                    NextEvent[0].Invoke();
                }
                Next[0].next = false;
                Next[0].stop = false;
                enabled = false;
            }
        }
        else
        {
            if (Next[currentIndex - 1] != null)
            {
                if (Next.Length > 0 && Next[currentIndex - 1].texts[0].text != "")
                {
                    next = true;
                    texts[currentIndex - 1].color = defaultColors[currentIndex - 1];
                    Next[currentIndex - 1].enabled = true;
                    if (NextEvent[currentIndex - 1] != null)
                    {
                        NextEvent[currentIndex - 1].Invoke();
                    }
                    Next[currentIndex - 1].next = false;
                    Next[currentIndex - 1].stop = false;
                    enabled = false;
                }
            }
        }
    }

    public void CustomTriggerNext(int index)
    {
        if (Next.Length > 0 && Next[index].texts[0].text != "")
        {
            next = true;
            texts[currentIndex - 1].color = defaultColors[currentIndex - 1];
            Next[index].enabled = true;
            if (NextEvent[index] != null)
            {
                NextEvent[index].Invoke();
            }
            Next[index].next = false;
            Next[index].stop = false;
            enabled = false;
        }
    }
}
