using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Button3 : MonoBehaviour
{
    public SaveFrameProperties[] properties;
    [Serializable]
    public struct SaveFrameProperties
    {
        public TextMeshProUGUI locationText;
        public TextMeshProUGUI locationNameText;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI timeText2;

        public GameObject defSave, emptySave;
        public bool isEmpty;

        public Image image;
    }

    public OptionProperties[] properties2;
    [Serializable]
    public struct OptionProperties
    {
        public TextMeshProUGUI text;
        public Image icon;
        public Image image;
    }

    bool a;
    int crrIndex, preIndex;
    public Sprite defSprite, newSprite, defSprite2, newSprite2, defSprite3, newSprite3;
    public TriggerTimeline[] timelines;
    public AnimationCreator arrow;

    MainMenu_Button1 button1;
    TimelineControl tlControl;

    [HideInInspector]
    public bool b;

    void Awake()
    {
        b = true;
        for (int i = 0; i < 3; i++)
        {
            GameData.Normal data = SaveManager.GetDataNormal(i+1);
            properties[i].locationNameText.text = (data.areaIndex == 0 || data.areaIndex == 1) ? "???" : HelpingHand.GetSceneName(data.areaIndex);
            properties[i].timeText2.text = ConvertTime(data.Time);

            if (!data.Created)
            {
                properties[i].defSave.SetActive(false);
                properties[i].emptySave.SetActive(true);
            }

            properties[i].isEmpty = !data.Created;
        }

        button1 = FindObjectOfType<MainMenu_Button1>();
        tlControl = FindObjectOfType<TimelineControl>();
    }

    void Update()
    {
        if (!a)
        {
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !b)
            {
                crrIndex++;
                if (crrIndex > 2) crrIndex = 0;

                Change();
            }

            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !b)
            {
                crrIndex--;
                if (crrIndex < 0) crrIndex = 2;

                Change();
            }
        }

        else if (a)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                crrIndex++;
                if (crrIndex > 1) crrIndex = 0;

                Change();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                crrIndex--;
                if (crrIndex < 0) crrIndex = 1;

                Change();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !b)
        {
            if (!a)
            {
                if (!properties[crrIndex].isEmpty)
                {
                    a = true;
                    preIndex = crrIndex;
                    crrIndex = 0;

                    Change();
                }
            }

            else
            {
                if (crrIndex == 0)
                {
                    FindObjectOfType<GlobalTime>().GlobalIndex = preIndex+1;
                    SaveManager.Load();
                }

                if (crrIndex == 1)
                {
                    SaveManager.ResetSave(preIndex+1);
                    properties[preIndex].defSave.SetActive(false);
                    properties[preIndex].emptySave.SetActive(true);
                    properties[preIndex].isEmpty = true;

                    a = false;
                    crrIndex = preIndex;
                    Change();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !b)
        {
            if (a)
            {
                a = false;
                crrIndex = preIndex;

                Change();
            }

            else
            {
                b = true;
                arrow.gameObject.SetActive(false);
                timelines[0].PlayTimeline();
                tlControl.onEndTimeline += PlayTimeline1;
            }
        }
    }

    void Change()
    {
        if (!a)
        {
            for (int i = 0; i < properties2.Length; i++)
            {
                properties2[i].text.color = Color.white;
                properties2[i].image.sprite = defSprite2;
                if (i == 1) properties2[i].icon.sprite = defSprite3;
            }

            foreach (var property in properties)
            {
                property.image.sprite = defSprite;
                property.image.SetNativeSize();

                property.timeText.color = Color.black;
                property.timeText2.color = Color.black;
                property.locationText.color = Color.black;
                property.locationNameText.color = Color.black;
            }

            properties[crrIndex].image.sprite = newSprite;
            properties[crrIndex].image.SetNativeSize();

            properties[crrIndex].timeText.color = Color.white;
            properties[crrIndex].timeText2.color = Color.white;
            properties[crrIndex].locationText.color = Color.white;
            properties[crrIndex].locationNameText.color = Color.white;

            LeanTween.move(GameObject.Find("Arrow"), GameObject.Find("ArrowPos" + crrIndex).transform.position, .05f).setEaseOutSine();
        }

        else
        {
            for (int i = 0; i < properties2.Length; i++)
            {
                properties2[i].text.color = Color.white;
                properties2[i].image.sprite = defSprite2;
                if (i == 1) properties2[i].icon.sprite = defSprite3;
            }

            properties2[crrIndex].text.color = Color.black;
            properties2[crrIndex].image.sprite = newSprite2;
            if (crrIndex == 1) properties2[crrIndex].icon.sprite = newSprite3;

            LeanTween.move(GameObject.Find("Arrow"), GameObject.Find("ArrowPos" + (crrIndex + 3)).transform.position, .05f).setEaseOutSine();
        }
    }

    void PlayTimeline1()
    {
        timelines[1].PlayTimeline();
        tlControl.onEndTimeline -= PlayTimeline1;
        tlControl.onEndTimeline += ResetBool;
    }

    void ResetBool()
    {
        button1.b = false;
        button1.arrow.gameObject.SetActive(true);
        button1.arrow.PlayAnimation();
        tlControl.onEndTimeline -= ResetBool;
    }

    string ConvertTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt(time / 60 % 60);
        int seconds = Mathf.FloorToInt(time % 60);

        string res = (hours < 10) ? "0" + hours.ToString() + ":" : hours.ToString() + ":";
        res += (minutes < 10) ? "0" + minutes.ToString() + ":" : minutes.ToString() + ":";
        res += (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();

        return res;
    }
}
