using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Button2 : MonoBehaviour
{
    public SaveFrameProperties[] properties;
    [Serializable]
    public struct SaveFrameProperties
    {
        public TextMeshProUGUI locationText;
        public TextMeshProUGUI locationNameText;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI timeText2;

        public Image image;
    }

    int crrIndex;
    public Sprite defSprite, newSprite;
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
            GameData.Normal data = SaveManager.GetDataNormal(i + 1);
            properties[i].locationNameText.text = (data.areaIndex == 0 || data.areaIndex == 1) ? "???" : HelpingHand.GetSceneName(data.areaIndex);
            properties[i].timeText2.text = HelpingHand.ConvertTime(data.Time);
        }

        button1 = FindObjectOfType<MainMenu_Button1>();
        tlControl = FindObjectOfType<TimelineControl>();
    }

    void Update()
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !b)
        {
            b = true;
            arrow.gameObject.SetActive(false);

            timelines[0].PlayTimeline();
            tlControl.onEndTimeline += PlayTimeline1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !b)
        {
            b = true;
            arrow.gameObject.SetActive(false);

            timelines[0].PlayTimeline();
            tlControl.onEndTimeline += PlayAnimation;
        }
    }

    void Load()
    {
        GameData.Normal data1 = new GameData.Normal();
        GameData.Unique data2 = new GameData.Unique();

        data1.Created = true;
        data1.areaIndex = 25; //either last scene or something else

        data2.CScene = new bool[SaveManager.csCount];
        data2.special = new string[SaveManager.specialCount];

        FindObjectOfType<GlobalTime>().GlobalIndex = crrIndex + 1;

        SaveManager.QuickSave(data1, data2, crrIndex + 1);
        SaveManager.Load();
    }

    void Change()
    {
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

    void PlayTimeline1()
    {
        timelines[1].PlayTimeline();
        tlControl.onEndTimeline -= PlayTimeline1;
        tlControl.onEndTimeline += ResetBool;
    }

    void PlayAnimation()
    {
        GameObject.Find("BG").SetActive(false);
        AnimationCreator anim = GameObject.Find("BG2").GetComponent<AnimationCreator>();

        anim.PlayAnimation();
        anim.OnEndAnimation += Load;
    }

    void ResetBool()
    {
        button1.b = false;
        button1.arrow.gameObject.SetActive(true);
        button1.arrow.PlayAnimation();
        tlControl.onEndTimeline -= ResetBool;
    }
}
