using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour
{
    private float defWidth;
    private float defHeight;
    public RectTransform ScreenRender;
    public RectTransform UI;
    public float Scale;
    public GameObject PlayerMenu;
    public GameObject gameDebug; bool a;
    public static bool IsPlaying = false;
    public string CurrentMenuString;
    public MasterVolumeOption volumeOption;
    public float[] defaultVolume;
    public AudioSource[] audios;
    public Animator animator;
    public int Framerate;
    public bool scaleUI;
    [HideInInspector]
    public bool introfullscreen = true;
    public static bool IsFullScreen;
    public void Awake()
    {
        defWidth = (Screen.currentResolution.width / 1.65f) / 2.2181818f;
        defHeight = Screen.currentResolution.height / 1.65f;

        //Screen.SetResolution(Mathf.FloorToInt(defWidth), Mathf.FloorToInt(defHeight), false);
    }

    void Start()
    {
        audios = new AudioSource[FindObjectOfType<AudioManager>().sounds.Length];
        defaultVolume = new float[audios.Length];
        audios = gameObject.GetComponents<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;

        for (int i = 0; i < audios.Length; i++)
        {
            defaultVolume[i] = audios[i].volume;
        }

        Application.targetFrameRate = Framerate;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            gameDebug.SetActive(a);
            a = !a;
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            ChangeFullScreen();
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape))
        {
            IsPlaying = !IsPlaying;

            if (IsPlaying == false)
            {
                SaveSetting();
                PlayerMenu.SetActive(false);
            }
            if (IsPlaying == true)
            {
                PlayerMenu.SetActive(true);

                GameObject.Find("ITEM").GetComponent<PlayerItemBehaviour>().UpdateItem();
                FindObjectOfType<PlayerStatBehaviour>().UpdateStat();
            }
        }

        if (IsPlaying == true)
        {
            if (PlayerMenu.GetComponent<ButtonControl>().currentIndex > 0)
            {
                CurrentMenuString = PlayerMenu.GetComponent<ButtonControl>().GetCurrentTextString();
            }
            switch (CurrentMenuString)
            {
                case "SETTING":
                    GameObject.Find("ITEM").GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                    GameObject.Find("SETTING").GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                    GameObject.Find("STAT").GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                    break;
                case "STAT":
                    GameObject.Find("ITEM").GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                    GameObject.Find("SETTING").GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                    GameObject.Find("STAT").GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                    break;
                case "ITEM":
                    GameObject.Find("ITEM").GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                    GameObject.Find("SETTING").GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                    GameObject.Find("STAT").GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                    break;
            }
        }
    }
    public void ChangeFullScreen()
    {
        IsFullScreen = !IsFullScreen;
        if (!Screen.fullScreen)
        {
            Screen.SetResolution(Screen.currentResolution.width , Screen.currentResolution.height, true);

            if (!scaleUI) ScreenRender.localScale /= Scale;
            if (scaleUI) UI.localScale /= Scale;

            if (!introfullscreen) {
                GameObject.Find("PlayerUI").GetComponent<RectTransform>().localScale /= Scale;                      
                GameObject.Find("BlackCovers").GetComponent<Transform>().localScale /= Scale;
                FindObjectOfType<IntroScene_Behaviour>().Computer.GetComponent<RectTransform>().localScale /= Scale;
            }            
        }
        else
        {
            Screen.SetResolution(Mathf.FloorToInt(defWidth), Mathf.FloorToInt(defHeight), false);

            if (!scaleUI) ScreenRender.localScale *= Scale;
            if (scaleUI) UI.localScale *= Scale;

            if (!introfullscreen) {
                GameObject.Find("PlayerUI").GetComponent<RectTransform>().localScale *= Scale;                            
                GameObject.Find("BlackCovers").GetComponent<Transform>().localScale *= Scale;     
                FindObjectOfType<IntroScene_Behaviour>().Computer.GetComponent<RectTransform>().localScale *= Scale; 
            }
        }
    }

    public void SaveSetting()
    {
        volumeOption = FindObjectOfType<MasterVolumeOption>();

        GameData.Settings data = new GameData.Settings();
        if (volumeOption)
        {
            data.isMuted = volumeOption.muted;
            data.masterVolume = float.Parse(volumeOption.Percentage);
        }

        string value = JsonUtility.ToJson(data, true);
        CustomSave.Save(value, "Settings", "Savedata/", ".json");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        ScreenRender = GameObject.FindGameObjectWithTag("ScreenRender").GetComponent<RectTransform>();
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<RectTransform>();
        animator = GameObject.Find("MENU BG").GetComponent<Animator>();
        volumeOption = FindObjectOfType<MasterVolumeOption>();

        PlayerMenu = GameObject.Find("PlayerMenu");
        PlayerMenu.SetActive(false);

        if (Screen.fullScreen)
        {
            UI.localScale /= Scale;
        }

        volumeOption.Percentage = SaveManager.GetSettings().masterVolume.ToString();
        volumeOption.muted = SaveManager.GetSettings().isMuted;

        for (int i = 0; i < audios.Length; i++)
        {
            float volume = defaultVolume[i] * float.Parse(volumeOption.Percentage) / 100;
            audios[i].volume = volume;
        }

        foreach (AudioSource audio in audios)
        {
            audio.mute = volumeOption.muted;
        }
    }
}
