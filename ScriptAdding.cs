using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptAdding : MonoBehaviour
{
    DialogueTrigger[] dlgs;
    ScriptManager script;
    int crr = 0;
    int count;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        DontDestroyOnLoad(this);

        dlgs = FindObjectsOfType<DialogueTrigger>();
        script = FindObjectOfType<ScriptManager>();

        count = SceneManager.sceneCountInBuildSettings;
        foreach (var dlg in dlgs)
        {
            for (int i = 0; i < dlg.dialogue.sentences.Length; i++)
            {
                if (script.CheckScript(dlg.dialogue.sentences[i], dlg.gameObject, i) == false)
                {
                    script.AddScript(dlg.dialogue.sentences[i], dlg.gameObject, i);
                    Debug.Log(string.Format("Added Script: {0}, {1}, {2}", dlg.dialogue.sentences[i], dlg.gameObject, i));
                }
            }
        }

        crr++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("crr=" + crr);
        if (crr < count)
        {
            dlgs = FindObjectsOfType<DialogueTrigger>();
            script = FindObjectOfType<ScriptManager>();

            foreach (var dlg in dlgs)
            {
                for (int i = 0; i < dlg.dialogue.sentences.Length; i++)
                {
                    if (script.CheckScript(dlg.dialogue.sentences[i], dlg.gameObject, i) == false)
                    {
                        script.AddScript(dlg.dialogue.sentences[i], dlg.gameObject, i);
                        Debug.Log(string.Format("Added Script: {0}, {1}, {2}", dlg.dialogue.sentences[i], gameObject, i));
                    }
                }
            }
            crr++;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
