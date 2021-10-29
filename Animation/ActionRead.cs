using UnityEngine;
using System;

public class ActionRead : MonoBehaviour
{
    public TextAsset[] timeLines;
    public static ActionProperty[] actionProperty;
    TimelineActions.ActionType actionType;
    string[] actionTypes;

    [System.Serializable]
    public struct ActionProperty
    {
        public string[] actions;
        public string[] actionValues;
        public float[] times;
    }
    void Awake(){
        actionType = TimelineActions.actionType;
        actionTypes = new string[Enum.GetValues(typeof (TimelineActions.ActionType)).Length];
        actionProperty = new ActionProperty[timeLines.Length];

        int i = default(int); // Unnecessary, literally... 

        foreach (TimelineActions.ActionType action in Enum.GetValues(typeof (TimelineActions.ActionType)))
        {
            actionTypes[i] = action.ToString();
            i++;
        }
        //This is to assign action strings

        for (int o = 0; o < actionProperty.Length; o++)
        {
            try
            {
                actionProperty[o] = ReadTimeline(o);
            }
            catch (System.Exception e)
            {
                Debug.Log(string.Format("Error in reading action at: {0} - {1}", o, e));
                throw;
            }
        }
    }

    string GetTimeline(int index){
        return timeLines[index].text;
    }

    ActionProperty ReadTimeline(int index){
        string tl = GetTimeline(index).Replace(" ", "");

        string[] _actions = new string[ReturnLine(tl)];
        string[] _actionValues = new string[ReturnLine(tl)];
        float[] _times = new float[ReturnLine(tl)];

        for (int i = 0; i < ReturnLine(tl); i++)
        {
            string action = "";
            string ln = GetLine(tl, i + 1);

            for (int o = 0; o < actionTypes.Length; o++)
            {
                if (ln.Contains(actionTypes[o])){
                    action = actionTypes[o];
                    _actionValues[i] = getBetween(ln, action + "(", ")");
                    _times[i] = float.Parse(getBetween(ln, _actionValues[i] + "):", ","));

                    break;
                }
            }
            _actions[i] = action;
        }

        return new ActionProperty{
            actions = _actions,
            actionValues = _actionValues,
            times = _times
        }; 
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

    public string getBetween(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }

        return "";
    }

    #endregion
}
