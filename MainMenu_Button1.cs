using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Button1 : MonoBehaviour
{
    //only be used in main menu
    public Image[] images = new Image[3];
    public TextMeshProUGUI[] texts = new TextMeshProUGUI[3];
    public AnimationCreator[] anims = new AnimationCreator[3];
    public TriggerTimeline[] timelines;
    public Sprite defSprite,newSprite;
    public AnimationCreator arrow;
    TimelineControl tlControl;
    MainMenu_Button2 button2;
    MainMenu_Button3 button3;
    int crrIndex = 1;
    [HideInInspector]
    public bool a, b;

    void Start()
    {
        arrow.PlayAnimation();
        tlControl = FindObjectOfType<TimelineControl>();
        button2 = FindObjectOfType<MainMenu_Button2>();
        button3 = FindObjectOfType<MainMenu_Button3>();

        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !b)
        {
            crrIndex++;
            if (crrIndex > 3) crrIndex = 0;

            Change();
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !b)
        {
            crrIndex--;
            if (crrIndex < 0) crrIndex = 3;

            Change();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !b)
        {
            b = true;
            arrow.gameObject.SetActive(false);
            switch (crrIndex)
            {
                case 0:
                    anims[2].StopAnimation();
                    anims[1].StopAnimation();
                    anims[0].StopAnimation();
                    anims[2].PlayAnimation();
                    a = false;

                    anims[2].OnEndAnimation += () => Application.Quit();
                    break;

                case 1:
                    timelines[0].PlayTimeline();
                    tlControl.onEndTimeline += PlayTimeline1;

                    break;

                case 2:
                    timelines[0].PlayTimeline();
                    tlControl.onEndTimeline += PlayTimeline2;

                    break;
            }
        }
    }

    void Change()
    {
        foreach (var image in images)
        {
            image.sprite = defSprite;
        }

        foreach (var text in texts)
        {
            text.color = Color.white;
        }

        if (crrIndex != 0)
        {
            if (a)
            {
                anims[2].StopAnimation();
                anims[0].StopAnimation();
                anims[1].PlayAnimation();
                a = false;
            }

            images[crrIndex].sprite = newSprite;
            texts[0].enabled = false;
            texts[crrIndex].color = Color.black;
        }

        else
        {
            a = true;
            texts[0].enabled = true;

            anims[2].StopAnimation();
            anims[1].StopAnimation();
            anims[0].PlayAnimation();
        }

        LeanTween.move(GameObject.Find("Arrow"), GameObject.Find("ArrowPos" + crrIndex).transform.position, .05f).setEaseOutSine();
    }

    void Exit()
    {
        anims[2].StopAnimation();
        anims[0].StopAnimation();
        anims[1].PlayAnimation();
        a = false;

        anims[2].OnEndAnimation += () => Application.Quit(); 
    }

    void PlayTimeline1()
    {
        timelines[1].PlayTimeline();
        tlControl.onEndTimeline -= PlayTimeline1;
        tlControl.onEndTimeline += ResetBool1;
    }

    void PlayTimeline2()
    {
        timelines[2].PlayTimeline();
        tlControl.onEndTimeline -= PlayTimeline2;
        tlControl.onEndTimeline += ResetBool2;
    }

    void ResetBool1()
    {
        button2.b = false;
        button2.arrow.gameObject.SetActive(true);
        button2.arrow.PlayAnimation();
        tlControl.onEndTimeline -= ResetBool1;
    }

    void ResetBool2()
    {
        button3.b = false;
        button3.arrow.gameObject.SetActive(true);
        button3.arrow.PlayAnimation();
        tlControl.onEndTimeline -= ResetBool2;
    }
}
