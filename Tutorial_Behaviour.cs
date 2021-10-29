using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

public class Tutorial_Behaviour : MonoBehaviour
{
    public DialogueTrigger[] dialogues;
    public TriggerTimeline timeline;
    public EnemyCollision collision;
    public Material material;
    public Sprite defDialogueBox;
    public GameObject[] visuals;
    public AnimationCurve bounce;
    bool a,b,c,d,e,f,g,h,i,j,k,l;
    public int phase;
    float time;
    float ypos;
    RectTransform dialogueText;
    DialogueManager dialogueManager;

    void Awake()
    {
        RectTransform screenRender = GameObject.FindGameObjectWithTag("ScreenRender").GetComponent<RectTransform>();
        RectTransform UI = GameObject.FindGameObjectWithTag("UI").GetComponent<RectTransform>();
        PlayerSettings settings = FindObjectOfType<PlayerSettings>();

        if (Screen.fullScreen) 
        { 
            screenRender.localScale /= settings.Scale;
            UI.localScale *= settings.Scale;
        }
        settings.scaleUI = false;

        enabled = false;
        SetValue();
    }
    void Start()
    {
        dialogues[0].TriggerDialogue();

        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.OnEndDialogue += delegate { phase++; };
        FindObjectOfType<PlayerCollision>().OnHit += Action1;

        StartCoroutine(enumerator());
        SetValueToZero();

        FindObjectOfType<AudioManager>().Play("Tutorial");

        dialogueText = GameObject.Find("DialogueTexts").GetComponent<RectTransform>();
        ypos = dialogueText.position.y;

        GameObject obj = GameObject.Find("E_statuethatyoudontregonisebutposeathreattoyou");
        obj.GetComponent<EnemyCollision>().onTakeDamage += Action5;

        obj.SetActive(false);
    }

    void Action5()
    {
        dialogues[12].TriggerDialogue();
        FindObjectOfType<TimelineControl>().StopTimeline();
        dialogueManager.OnEndDialogue += Action4;
        c = true;
    }
    void SwitchVisual(int i)
    {
        foreach (var visual in visuals)
        {
            visual.SetActive(false);
        }

        if (i != -1) visuals[i].SetActive(true);
    }

    void Action1()
    {
        StartCoroutine(_Action1());
    }

    IEnumerator _Action1()
    {
        if (phase == 9)
        {
            dialogueManager.OnEndDialogue += Action2;

            FindObjectOfType<BulletContainer>().RemoveAll();
            collision.GetComponent<EnemyAI>().enabled = false;
            collision.GetComponent<E_DummyBehaviour>().pause = true;

            if (g)
            {
                yield return new WaitForSeconds(.5f);
                phase--;
                dialogues[9].TriggerDialogue();
            }
            if (!g)
            {
                yield return new WaitForSeconds(.5f);
                g = true;
                phase--;
                dialogues[8].TriggerDialogue();
            }
        }
    }

    void Action2()
    {
        collision.GetComponent<EnemyAI>().enabled = true;
        collision.GetComponent<E_DummyBehaviour>().pause = false;

        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.OnEndDialogue -= Action2;
    }

    void Action3()
    {
        phase = 8;
        dialogues[5].TriggerDialogue();

        c = true; d = true;
        collision.GetComponent<EnemyAI>().enabled = false;
        collision.onTakeDamage -= Action3;
    }

    void Action4()
    {
        phase = 9;
        GameObject.Find("DialogueTexts").SetActive(true);

        GameObject obj1 = GameObject.Find("E_statuethatyoudontregonisebutposeathreattoyou");
        GameObject obj2 = GameObject.Find("E_statuethatyoudontregonisebutposeathreattoyou2");

        obj2.transform.position = obj1.transform.position;
        Destroy(obj1);

        d = true;
        dialogueManager.OnEndDialogue -= Action4;
    }

    void SetValueToZero()
    {
        material.SetFloat("_AbberationStrength", 0);
        material.SetFloat("_DoubleOffset", 0);
        material.SetFloat("_DoubleStrength", 0);
        material.SetFloat("_VignettePower", 0);
        material.SetFloat("_ScanSpeed", 0);
        material.SetFloat("_LineScale", 0);
        material.SetFloat("_LineWeightMin", 0);
        material.SetFloat("_LineWeightMax", 0);
        material.SetFloat("_LineDistributionPow", 0);
        material.SetFloat("_LineWeight", 0);
    }

    void SetValue()
    {
        LeanTween.value(0f, 1f, 2f).setOnUpdate((float val) => { material.SetFloat("_AbberationStrength", val); });
        LeanTween.value(0f, 0.28f, 2f).setOnUpdate((float val) => { material.SetFloat("_DoubleOffset", val); });
        LeanTween.value(0f, 4f, 2f).setOnUpdate((float val) => { material.SetFloat("_DoubleStrength", val); });
        LeanTween.value(0f, 0.3f, 2f).setOnUpdate((float val) => { material.SetFloat("_VignettePower", val); });
        LeanTween.value(0f, 5f, 2f).setOnUpdate((float val) => { material.SetFloat("_ScanSpeed", val); });
        LeanTween.value(0f, 1.5f, 2f).setOnUpdate((float val) => { material.SetFloat("_LineScale", val); });
        LeanTween.value(0f, 0.2f, 2f).setOnUpdate((float val) => { material.SetFloat("_LineWeightMin", val); });
        LeanTween.value(0f, 0.7f, 2f).setOnUpdate((float val) => { material.SetFloat("_LineWeightMax", val); });
        LeanTween.value(0f, 4f, 2f).setOnUpdate((float val) => { material.SetFloat("_LineDistributionPow", val); });
        LeanTween.value(0f, 1f, 2f).setOnUpdate((float val) => { material.SetFloat("_LineWeight", val); });
    }

    void Update()
    {
        if (j)
        {
            time += Time.deltaTime;
            dialogueText.position = new Vector3(dialogueText.position.x, ypos + bounce.Evaluate(time*2), dialogueText.position.y);
        }
        if (l)
        {
            if (dialogueManager.CurrentDialogueIndex == 1)
            {
                FindObjectOfType<Tutorial_Corruption>().Corrupt(1);
                l = false;
            }
        }
    }

    IEnumerator enumerator()
    {
        while (true)
        {
            if (phase == 1)
            {
                if (!a)
                {
                    SwitchVisual(0);
                    a = true;
                }

                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
                {
                    dialogues[1].TriggerDialogue();
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9))
                {
                    dialogues[2].TriggerDialogue();
                    phase--;
                }
            }

            else if (phase == 2)
            {
                if (!k)
                {
                    SwitchVisual(1);
                    k = true;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PlayerAttack player = FindObjectOfType<PlayerAttack>();
                    if (player.currentAttackIndex == 1)
                    {
                        yield return new WaitForSeconds(1);
                        dialogues[3].TriggerDialogue();
                        phase = 3;
                    }

                    else if (player.currentAttackIndex == 2)
                    {
                        yield return new WaitForSeconds(1.7f);
                        dialogues[4].TriggerDialogue();
                        phase = 3;
                    }
                }
            }

            else if (phase == 3 && !b)
            {
                SwitchVisual(-1);
                b = true;

                yield return new WaitForSeconds(.5f);
                timeline.PlayTimeline();

                yield return new WaitForSeconds(2.5f);
                if (phase == 5) phase = 4;
            }

            else if (phase == 8 && !c)
            {
                SwitchVisual(2);
                collision.onTakeDamage += Action3;

                yield return new WaitForSeconds(5);
                if (!d) 
                {
                    phase--;
                    dialogues[6].TriggerDialogue();
                    d = true;
                }
            }

            else if (phase == 9)
            {
                if (h == false)
                {
                    SetValue();
                    h = true;
                }
                c = true;
                if (!e)
                {
                    SwitchVisual(3);
                    collision.GetComponent<E_DummyBehaviour>().enabled = true;
                    collision.GetComponent<EnemyAI>().enabled = true;
                    e = true;
                }

                E_DummyBehaviour behaviour = collision.GetComponent<E_DummyBehaviour>();
                if (behaviour.a >= 6 && !f)
                {
                    f = true;
                    FindObjectOfType<BulletContainer>().RemoveAll();
                    collision.GetComponent<EnemyAI>().enabled = false;
                    collision.GetComponent<E_DummyBehaviour>().pause = true;

                    yield return new WaitForSeconds(.5f);
                    dialogues[7].TriggerDialogue();
                    SwitchVisual(-1);
                    l = true;
                }
            }

            else if (phase == 10 && !i)
            {
                dialogueManager.LockLockDialogue = true;
                dialogues[11].TriggerDialogue();

                yield return new WaitForSeconds(1);
                FindObjectOfType<PlayerMovement>().forceEnable = true;
                FindObjectOfType<PlayerAttack>().forceEnable = true;
                j = true;

                CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();
                CinemachineBasicMultiChannelPerlin noise = cam?.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                noise.m_FrequencyGain = 2;
                noise.m_AmplitudeGain = 1;
                LeanTween.value(noise.m_AmplitudeGain, 0, 2)
                .setOnUpdate((float val) => { noise.m_AmplitudeGain = val; })
                .setEaseOutSine();

                FindObjectOfType<AudioManager>().Play("Hit");

                yield return new WaitForSeconds(3);

                GameObject.Find("DialogueBox").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                GameObject obj1 = GameObject.Find("E_statuethatyoudontregonisebutposeathreattoyou2");
                GameObject obj2 = GameObject.Find("E_statuethatyoudontregonisebutposeathreattoyou3");

                Vector3 pos = obj2.transform.position;
                obj2.transform.position = obj1.transform.position;
                obj1.transform.position = pos;

                obj2.GetComponent<EnemyAI>().enabled = true;
                obj2.GetComponent<E_DummyBehaviour2>().enabled = true;
                obj2.GetComponent<CharacterMoveAnimation>().enabled = true;
                obj2.GetComponent<EnemyCollision>().onNoHealth += TutorialComplete;

                GameObject.Find("PolyorbSpawner").GetComponent<Tutorial_Spawner>().enabled = true;
                SwitchVisual(4);

                i = true;
            }

            yield return null;
        }
    }

    void TutorialComplete()
    {
        StartCoroutine(_TutorialComplete());
    }

    IEnumerator _TutorialComplete()
    {
        j = false;
        Destroy(GameObject.Find("PolyorbSpawner"));
        Destroy(GameObject.Find("E_statuethatyoudontregonisebutposeathreattoyou3"));

        FindObjectOfType<AudioManager>().Stop("Tutorial2");
        FindObjectOfType<AudioManager>().Stop("Tutorial");

        dialogueManager.LockLockDialogue = false;

        SetValueToZero();
        FindObjectOfType<AudioManager>().Stop("Tutorial2");

        GameObject.Find("FullBlackCover").GetComponent<Image>().enabled = true;

        GameObject obj = GameObject.Find("DialogueBox");
        SpriteRenderer ren = obj.GetComponent<SpriteRenderer>();

        ren.sprite = defDialogueBox;
        ren.sortingLayerName = "More Top";

        obj.transform.localScale = new Vector3(32, 32, 1);
        GameObject.Find("DialogueTexts").transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 5, 0);
        yield return new WaitForSeconds(1);
        dialogues[10].TriggerDialogue();   

        dialogueManager.OnEndDialogue += delegate { SceneManager.LoadScene(21); };
    }
}
