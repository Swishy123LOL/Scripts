using UnityEngine;
using UnityEngine.Rendering;

public class Forest_Behaviour2 : MonoBehaviour
{
    TimelineControl timeline;
    public GameObject[] obj;
    void Start()
    {
        timeline = FindObjectOfType<TimelineControl>();
        timeline.onEndTimeline += () => {
            SaveManager._special[1] = "1";
        };

        if (SaveManager._special[1] == "1")
        {
            obj[0].SetActive(true);
            obj[1].SetActive(false);
            obj[0].GetComponent<DialogueTrigger>().enabled = false;
            obj[2].GetComponent<DialogueTrigger>().enabled = true;
        }
    }
}
