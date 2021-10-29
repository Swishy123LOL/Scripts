using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using System;

public class DialogueRead : MonoBehaviour
{
    public bool apl;
    public string[] action;
    public string[] tg;
    public List<IndexType> indx = new List<IndexType>();
    public Action onFinal;
    [System.Serializable]
    public struct IndexType{
        public int start;
        public int end;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ReturnDialogue(string dlg, TextMeshProUGUI _text, float time, AudioManager audio){
        string d = dlg;
        int t = tagCheck(d);
        bool b = SentenceCheck(ref d);
        
        StartCoroutine(_DisplayDialogue(d, b, t, _text, time, audio));
    }

    public bool SentenceCheck(ref string str){
        int i = tagCheck(str);
        bool hasAction = false;
        action = new string[i];
        tg = new string[i];
        IndexType[] indices = new IndexType[i];
        //Action and tg is a new varible for every sentence

        indx.Clear();

        bool active = false;
        string crr = "";
        int _indx = 0;
        foreach (char chr in str)
        {
            if (chr == '>'){
                active = false;
                continue;
            }

            if (chr == '<'){
                active = true;
                continue;
            }

            if (active == true){
                if (chr == '/'){
                    _indx++;
                    apl = false;
                    hasAction = true;
                    continue;
                }
                apl = true;
                action[_indx] += chr.ToString();
            }

            if (apl == true){
                tg[_indx] += chr;
            }

            crr += chr;
        }      

        for (int o = 0; o < _indx; o++)
        {
            tg[o] = tg[o].Replace(action[o], "");
            crr = crr.Replace(action[o], "");

            indices[o] = new IndexType {
                start = crr.IndexOf(tg[o]),
                end = crr.IndexOf(tg[o]) + (tg[o].Length - 1) + 1
            };
            indx.Add(indices[o]);
        }
        //Because "tg" and "crr" has "action" in it, we need to replace with an empty string  
        str = crr;
        //"crr" is the final output

        return hasAction;
    }

    public int tagCheck(string str){
        return Regex.Matches(str, "</>").Count;
        //The number of tag is determined by the number of "</>"
    }

    public void ActionCheck(ref string c, string a, ref bool b){
        switch (a[0])
        {
            case 'c':
                ColorAction(ref c, a);
                break;

            case 'w':
                WaitAction(ref c, a);
                break;

            case 'b':
                b = true;
                break;
        }
    }

    void ColorAction(ref string c, string a){
        string col = "white";
        switch (a[1])
        {
            case '0':
            col = "red";
            break;

            case '1':
            col = "blue";
            break;

            case '2':
            col = "yellow";
            break;

            case '3':
            col = "green";
            break;

            case '4':
            col = "orange";
            break;

            case '5':
            col = "purple";
            break;

            case '6':
            col = "black";
            break;
        }
        c = string.Format("<color={1}>{0}</color>", c, col);
    }

    public void DisplayDialogue(string dlg, bool b, int t, TextMeshProUGUI txt, float time, AudioManager audio){
        StartCoroutine(_DisplayDialogue(dlg, b, t, txt, time, audio));
    }

    public string GetDialogueInstant(string dlg)
    {
        string final = "";
        string d = dlg;
        int t = tagCheck(d);
        bool b = SentenceCheck(ref d);

        int crr = 0;
        bool f = false;
        int i = 0;

        foreach (char chr in d)
        {
            string s = chr.ToString();
            bool br = false;

            if (b == true && i < t && action[i].Contains("c"))
            {
                if (crr == indx[i].start) f = true;
                if (crr == indx[i].end) { f = false; i++; }
                if (f == true) ActionCheck(ref s, action[i], ref br);
                crr++;
            }

            final += s;
        }

        return final;
    }

    System.Collections.IEnumerator _DisplayDialogue(string dlg, bool b, int t, TextMeshProUGUI txt, float time, AudioManager audio){
        int crr = 0;
        bool f = false;
        int i = 0;

        Coroutine crt = StartCoroutine(PlaySound(dlg, audio));
        foreach (char chr in dlg)
        {
            if (time == 0) time = 0.02f;
            float _time = time;
            string s = chr.ToString();
            string df = s;
            bool br = false;
            
            if (b == true && i < t) {
                if (crr == indx[i].start) f = true; 
                if (crr == indx[i].end) { f = false; i++; }
                if (f == true) ActionCheck(ref s, action[i], ref br); 
                if (float.TryParse(s, out time)) s = df;
                //Check if "s" is a number, if yes, action is a "wait" type
                if (br == true)
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentences(true);
                    break;  
                }
                crr++;
            }

            if (s == ".") _time = .2f;
            if (s == ",") _time = .3f;

            txt.text += s;
            yield return new WaitForSeconds(_time);
        }

        if (crt != null) StopCoroutine(crt);
        onFinal?.Invoke();
    }

    System.Collections.IEnumerator PlaySound(string dlg, AudioManager audio){
        foreach (char s in dlg)
        {
            float t = .06f;
            if (s == '.') t = .2f;
            if (s == ',') t = .3f;
            audio.Play("txt");
            yield return new WaitForSeconds(t);
        }
    }

    void WaitAction(ref string c, string a){
        float num = float.Parse(a[1].ToString()) / 10;
        c = num.ToString();
    }
}
