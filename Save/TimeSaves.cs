using BayatGames.SaveGameFree;
using TMPro;
using UnityEngine;

public class TimeSaves : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI text;
    public int index;
    string time;
    string seconds;
    string minutes;
    string hours;
    float secondrunnin;

    void Start()
    {
        Debug.Log("SceneIndex" + FindObjectOfType<GlobalTime>().GlobalIndex.ToString());
        text = gameObject.GetComponent<TextMeshProUGUI>();
        secondrunnin = SaveManager.GetDataNormal(index).Time;

        seconds = Mathf.FloorToInt(secondrunnin % 60).ToString();
        minutes = Mathf.FloorToInt((secondrunnin / 60) % 60).ToString();
        hours = Mathf.FloorToInt(secondrunnin / 3600).ToString();

        if (SaveGame.Exists("areaIndex" + index) == false)
        {
            time = "--:--:--";
        }

        else
        {
            if (int.Parse(hours) < 10)
            {
                hours = "0" + hours;
            }
            if (int.Parse(minutes) < 10)
            {
                minutes = "0" + minutes;
            }
            if (int.Parse(seconds) < 10)
            {
                seconds = "0" + seconds;
            }
            time = hours + ":" + minutes + ":" + seconds;
        }

        text.text = time;
    }
}
