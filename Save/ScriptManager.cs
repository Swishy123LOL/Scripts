using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptManager : MonoBehaviour
{
    HandleTextFile handleText = new HandleTextFile();
    [TextArea(1, 100)]
    public string m_script;
    public TextAsset Script;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        m_script = GameData.Script;
    }

    public void AddScript(string sentence, GameObject parentGameobject, int index)
    {
        CustomSave.Write("/s/" + SceneManager.GetActiveScene().name + "/s/ " + "/n/" + parentGameobject.name + "/n/ " + "/j/" + index + "/j/ : " + "/% "+ sentence + "%/", "Script", "Savedata/", ".txt");
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

    public string GetScript (string sentence, GameObject parentGameobject, int index)
    {
        m_script = GameData.Script;
        m_script = getBetween(m_script ,"/s/" + SceneManager.GetActiveScene().name + "/s/ " + "/n/" + parentGameobject.name + "/n/ " + "/j/" + index + "/j/ : " + "/% ", "%/");
        return m_script;
    }

    public bool CheckScript(string sentence, GameObject parentGameobject, int index)
    {
        m_script = GameData.Script;
        bool IsScript = m_script.Contains("/s/" + SceneManager.GetActiveScene().name + "/s/ " + "/n/" + parentGameobject.name + "/n/ " + "/j/" + index + "/j/ : " + "/% ");
        return IsScript;
    }
}
