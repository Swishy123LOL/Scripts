using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameDebug : MonoBehaviour
{
    [Header("FrameRate")]
    public TextMeshProUGUI FPSCount;
    public float UpdateTime;
    [Header("SceneIndex")]
    public TextMeshProUGUI SceneIndex;
    [Header("PlayerSpeed")]
    public TextMeshProUGUI PlayerSpeed;
    [Header("PlayerPosition")]
    public TextMeshProUGUI posX;
    public TextMeshProUGUI posY;
    public TextMeshProUGUI posZ;
    [Header("ConsoleLog")]
    public TextMeshProUGUI consoleLog;

    int currentScene;

    float TimeRunning;
    bool renderedLine;
    [Header("PlayerHealth")]
    public TextMeshProUGUI PlayerHealth;

    void Awake()
    {
        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += RenderLine;
        Application.logMessageReceived += EchoDebugMessage;

        CustomSave.Create("ConsoleLog", "Savedata/", ".txt");
        CustomSave.Write(String.Format("--------------------Session at: {0}-------------------{1}", DateTime.Now, Environment.NewLine),
        "ConsoleLog", "Savedata/", ".txt");
    }

    void Update()
    {
        TimeRunning += Time.deltaTime;
        if (TimeRunning > UpdateTime)
        {
            TimeRunning -= UpdateTime;

            FPSCount.text = "fps: " + Mathf.Round(Time.frameCount / Time.time).ToString();
            SceneIndex.text = "sceneIndex: " + SceneManager.GetActiveScene().buildIndex.ToString();
                
            if (FindObjectOfType<PlayerMovement>() == true){
                PlayerSpeed.text = "speed: " + Math.Round(FindObjectOfType<PlayerMovement>().moveSpeed, 2).ToString();
            }
            if (FindObjectOfType<Player>() == true){
                Player player = FindObjectOfType<Player>();
                PlayerHealth.text = "playerHealth: " + player.PlayerHealth.ToString();

                posX.text = "posX: " + player.transform.position.x;
                posY.text = "posY: " + player.transform.position.y;
                posZ.text = "posZ: " + player.transform.position.z;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene - 1);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.B)){
            renderedLine = !renderedLine;
            if (renderedLine == true){
                FindObjectOfType<LineRender>().RenderLine();
            }
            else
            {
                FindObjectOfType<LineRender>().RemoveLine();
            }
        }
    }

    void RenderLine(Scene scene, LoadSceneMode sceneMode){
        if (renderedLine == true){
            FindObjectOfType<LineRender>().RemoveLine();
            FindObjectOfType<LineRender>().RenderLine();
        }
    }

    void EchoDebugMessage(string logString, string stackTrace, LogType type){
        string LogString = "";
        LogString += String.Format("[{1}]: {0}", logString, DateTime.Now);

        LogString += Environment.NewLine;
        LogString += Environment.NewLine;

        LogString += String.Format("Stack Trace: {0}", stackTrace);

        LogString += String.Format("TYPE: {0}", type);

        LogString += Environment.NewLine;

        consoleLog.text += LogString;
        CustomSave.Write(LogString, "ConsoleLog", "Savedata/", ".txt");
    }
}
