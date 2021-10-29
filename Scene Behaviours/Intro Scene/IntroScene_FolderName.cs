using TMPro;
using UnityEngine;

public class IntroScene_FolderName : MonoBehaviour
{
    string _Name;

    TextMeshProUGUI Name;
    TextMeshProUGUI NameDark;
    void Start()
    {
        _Name = gameObject.transform.parent.name;

        Name = gameObject.transform.Find("FolderName").GetComponent<TextMeshProUGUI>();
        NameDark = gameObject.transform.Find("FolderNameBlack").GetComponent<TextMeshProUGUI>();

        Name.text = _Name;
        NameDark.text = _Name;
    }
}
