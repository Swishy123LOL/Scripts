using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class TimelineControl : MonoBehaviour
{
    public Queue<string> Actions = new Queue<string>();
    public List<float> Times = new List<float>();
    int[] timelineCount;
    int[] currTimeline;
    public static bool isPause;
    public delegate void onAddedTimelineDlg();
    public event onAddedTimelineDlg onAddedTimeline;
    public Action onEndTimeline;
    int crrIndex = 0;

    void Start(){
        timelineCount = new int[ActionRead.actionProperty.Length];
        currTimeline = new int[ActionRead.actionProperty.Length];

        AddAllTimeline();
    }

    void AddAllTimeline(){
        for (int i = 0; i < ActionRead.actionProperty.Length; i++)
        {
            for (int o = 0; o < ReturnLine(FindObjectOfType<ActionRead>().timeLines[i].text); o++)
            {
                AddTimeline(ActionRead.actionProperty[i].actions[o],
                ActionRead.actionProperty[i].times[o],
                ActionRead.actionProperty[i].actionValues[o],
                i);
            }
        }
        
        onAddedTimeline?.Invoke();
    }

    IEnumerator ExecuteTimeline(int o, int startIndex = 0){
        for (int i = startIndex; i < timelineCount[o]; i++)
        {
            if (!isPause) {
                yield return new WaitForSecondsRealtime(ActionRead.actionProperty[o].times[i]);
            
                FindObjectOfType<TimelineActions>().InvokeAction(ActionRead.actionProperty[o].actions[i], 
                ActionRead.actionProperty[o].actionValues[i]);

                currTimeline[o]++;
            }
        }

        if (!isPause) onEndTimeline?.Invoke();
    }

    void AddTimeline(string action, float time, string values, int o){
        Actions.Enqueue(action);
        Times.Add(time);
        
        timelineCount[o]++;
    }

    public static GameObject FindInGameobject(string name){
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    public static GameObject FindGameobject(string name){
        return GameObject.Find(name);
    }

    public void PauseTimeline(bool hasDialogueTrigger = false, bool unpauseTween = true){
        isPause = true;
        LeanTween.pauseAll();

        if (hasDialogueTrigger == true) 
        {
            if (unpauseTween) FindObjectOfType<DialogueManager>().OnEndDialogue += UnpauseTimeline;
            else FindObjectOfType<DialogueManager>().OnEndDialogue += UnpauseTimelineWithoutUnpauseTween;
        }
    }

    public void UnpauseTimeline(){
        isPause = false;
        LeanTween.resumeAll();

        FindObjectOfType<DialogueManager>().OnEndDialogue -= UnpauseTimeline;

        StartCoroutine(ExecuteTimeline(crrIndex, currTimeline[crrIndex]));
    }

    public void UnpauseTimelineWithoutUnpauseTween() //lol don't ask why
    {
        isPause = false;

        FindObjectOfType<DialogueManager>().OnEndDialogue -= UnpauseTimelineWithoutUnpauseTween;

        StartCoroutine(ExecuteTimeline(crrIndex, currTimeline[crrIndex]));
    }

    public void PlayTimeline(int index){
        StartCoroutine(ExecuteTimeline(index));
        crrIndex = index;
    }

    public void StopTimeline()
    {
        StopAllCoroutines();
    }

    #region Includes

    string GetLine(string text, int lineNo)
    {
        string[] lines = text.Replace("\r","").Split('\n');
        return lines.Length >= lineNo ? lines[lineNo-1] : null;
    }

    int ReturnLine(string text)
    {
        string[] lines = text.Replace("\r","").Split('\n');
        return lines.Length;
    }

    #endregion
}
