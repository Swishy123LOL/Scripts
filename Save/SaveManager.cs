using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static int Spawn;
    public static bool isLoaded;

    public static int csCount = 5;
    public static int specialCount = 5;

    public static bool[] cScene;
    public static string[] _special;

    void Awake()
    {
        DontDestroyOnLoad(this);

        cScene = new bool[csCount];
        _special = new string[specialCount];

        if (!File.Exists(Application.dataPath + "/Savedata/Settings.json"))
        {
            GameData.Settings data = new GameData.Settings();
            data.isMuted = false;
            data.masterVolume = 100; //Set to default value

            SaveSettings(data);
        }
    }

    public static GameData.Normal GetDataNormal(int index = -1)
    {
        int i = FindObjectOfType<GlobalTime>().GlobalIndex;
        if (index > -1) i = index; 

        string _data = CustomSave.Load("Data" + i, "Savedata/", ".json");

        GameData.Normal data = new GameData.Normal();
        if (_data != "") data = JsonUtility.FromJson<GameData.Normal>(_data);

        return data;
    }

    public static GameData.Unique GetDataUnique(int index = -1)
    {
        int i = FindObjectOfType<GlobalTime>().GlobalIndex;
        if (index > -1) i = index;

        string _data = CustomSave.Load("DataUnique" + i, "Savedata/", ".json");

        GameData.Unique data = new GameData.Unique(); 
        if (_data != "") data = JsonUtility.FromJson<GameData.Unique>(_data);

        return data;
    }

    public static GameData.Settings GetSettings()
    {
        string _data = CustomSave.Load("Settings", "Savedata/", ".json");

        GameData.Settings data = JsonUtility.FromJson<GameData.Settings>(_data);
        return data;
    }

    public static void ResetSave(int index)
    {
        GameData.Normal data = new GameData.Normal();
        GameData.Unique data1 = new GameData.Unique();

        string value = JsonUtility.ToJson(data, true);
        CustomSave.Save(value, "Data" + index, "Savedata/", ".json");

        string value1 = JsonUtility.ToJson(data1, true);
        CustomSave.Save(value1, "DataUnique" + index, "Savedata/", ".json");
    }

    public void _Load()
    {
        Load();
    }

    public void _Save()
    {
        Save();
    }

    public static void Load()
    {
        if (!isLoaded)
        {
            GlobalTime gtime = FindObjectOfType<GlobalTime>();
            int i = gtime.GlobalIndex;

            string _data = CustomSave.Load("Data" + i, "Savedata/", ".json");
            GameData.Normal data = JsonUtility.FromJson<GameData.Normal>(_data);

            SceneManager.LoadScene(data.areaIndex);
            SceneManager.sceneLoaded += (Scene scene, LoadSceneMode loadSceneMode) =>
            {
                if (!isLoaded)
                {
                    _data = CustomSave.Load("Data" + i, "Savedata/", ".json");
                    data = JsonUtility.FromJson<GameData.Normal>(_data);

                    string _data1 = CustomSave.Load("DataUnique" + i, "Savedata/", ".json");
                    GameData.Unique data1 = JsonUtility.FromJson<GameData.Unique>(_data1);

                    Player player = FindObjectOfType<Player>();
                    SpriteRenderer ren = player.GetComponent<SpriteRenderer>();
                    PlayerMovement move = player.GetComponent<PlayerMovement>();

                    Vector3 pos;

                    pos.x = data.Position_X;
                    pos.y = data.Position_Y;
                    pos.z = data.Position_Z;

                    player.transform.position = pos;

                    gtime.secondrunnin = data.Time;
                    player.PlayerHealth = data.PlayerHealth;

                    ren.flipX = data.facingX;
                    ren.sprite = data.facingY ? move.backSprite : move.frontSprite;

                    cScene = data1.CScene;
                    _special = data1.special;

                    isLoaded = true;
                }
            };
        }
    }

    public static void Save()
    {
        Player player = FindObjectOfType<Player>();
        GlobalTime gtime = FindObjectOfType<GlobalTime>();
        SpriteRenderer ren = player.GetComponent<SpriteRenderer>();
        PlayerMovement move = player.GetComponent<PlayerMovement>();

        int i = gtime.GlobalIndex;

        GameData.Normal data = new GameData.Normal
        {
            areaIndex = SceneManager.GetActiveScene().buildIndex,
            PlayerHealth = player.PlayerHealth,

            Position_X = player.transform.position.x,
            Position_Y = player.transform.position.y,
            Position_Z = player.transform.position.z,

            Time = gtime.secondrunnin,

            facingX = ren.flipX,
            facingY = move.IsFoward,

            Created = true
        };

        GameData.Unique data1 = new GameData.Unique
        {
            CScene = cScene,
            special = _special
        };

        string value = JsonUtility.ToJson(data, true);
        CustomSave.Save(value, "Data" + i, "Savedata/", ".json");

        string value1 = JsonUtility.ToJson(data1, true);
        CustomSave.Save(value1, "DataUnique" + i, "Savedata/", ".json");
    }

    public static void QuickSave(GameData.Normal data1, GameData.Unique data2, int index = -1)
    {
        int i = index;
        if (index == -1) i = FindObjectOfType<GlobalTime>().GlobalIndex;

        string value1 = JsonUtility.ToJson(data1, true);
        CustomSave.Save(value1, "Data" + i, "Savedata/", ".json");

        string value2 = JsonUtility.ToJson(data2, true);
        CustomSave.Save(value2, "DataUnique" + i, "Savedata/", ".json");
    }

    public static void SaveSettings(GameData.Settings data)
    {
        string value = JsonUtility.ToJson(data, true);
        CustomSave.Save(value, "Settings", "Savedata/", ".json");
    }
}
