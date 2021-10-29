using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusSave : MonoBehaviour
{
    public TextMeshProUGUI status;
    public string index;
    public GameObject Button;
    public Sprite spriteEmpty;
    public Image image;
    public TextMeshProUGUI time;
    public GameObject deleteButton;

    bool FileCreated;

    void Start()
    {
        string _data = CustomSave.Load("Data" + index, "Savedata/", ".json");
        GameData.Normal data = new GameData.Normal();

        if (_data != "") data = JsonUtility.FromJson<GameData.Normal>(_data);

        FileCreated = data.Created;
    }
    public void Update()
    {
        if (FileCreated == false)
        {
            image.sprite = spriteEmpty;
            status.text = "(Empty)";
            time.text = "--:--";
            time.color = Color.gray;
            deleteButton.SetActive(false);
            Destroy(Button);
        }

        else
        {
            status.text = "";
        }
    }
}
