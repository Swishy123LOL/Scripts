using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ScriptSortingAlgorithm : MonoBehaviour
{
    [TextArea(3, 50)]
    public string Script;
    string preScript;
    int sceneCount;
    string[] sceneNames;
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

    string ChangeScript(string script, ref int iterationIndex) {
        string final = "";

        for (int n = 0; n < sceneNames.Length; n++)
        {
            string name = sceneNames[n];
            int o = 0;
            for (int i = 1; i < ReturnLine(script) + 1; i++)
            {  
                string line = GetLine(script, i);
                if (line.Contains("/s/" + name + "/s/") == true) {
                    o++;
                }
                //Get the current scene messages
            }

            final += Environment.NewLine;
            final += Environment.NewLine;

            final += string.Format("------------------------------------------------Scene Name: [{0}], Lines: [{1}]------------------------------------------------", name, o);
            //Insert some debug lines

            final += Environment.NewLine;
            for (int i = 1; i < ReturnLine(script) + 1; i++)
            {
                string line = GetLine(script, i);
                string next = "";

                if (line.Contains("/s/" + name + "/s/") == true) {
                    next = line;
                    final += Environment.NewLine;
                    script = script.Replace(line, "");
                    //If current line matches scene name, add it to "next", and enter a new line
                }

                iterationIndex++;
                final += next;
            }
        }

        return final;
    }

    public static string Reverse( string s )
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse( charArray );
        return new string( charArray );
    }

    string ReturnNameByPath(string path){
        string curr = "";
        for (int i = path.Length - 1; i > -1; i--)
        {
            if (path[i].ToString() == "/"){
                break;
            }
            curr += path[i];
        }
        curr = Reverse(curr);
        curr = curr.Replace(".unity", "");
        return curr;
    }
    public void _ChangeScript() {
        float timeElasped = 0f; 
        int iterations = 0;

        timeElasped += Time.deltaTime;

        sceneCount = SceneManager.sceneCountInBuildSettings;
        sceneNames = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            sceneNames[i] = ReturnNameByPath(path);
        }

        Script = CustomSave.Load("Script", "Savedata/", ".txt");
        preScript = Script;

        Script = ChangeScript(Script, ref iterations);

        Debug.Log("Finished changing script - Iterations: " + iterations);
    }
    public void FinishScript() {
        if (Script == "") {
            Debug.Log("Cannot save an empty string!");
            return;
        }

        CustomSave.Save(preScript, "Script Backup", "Savedata/", ".txt");
        CustomSave.Save(Script,"Script", "Savedata/", ".txt");

        Script = "";
        Debug.Log("Script Saved to " + CustomSave.ReturnPath("Script", "Savedata/", ".txt"));
    }
}
