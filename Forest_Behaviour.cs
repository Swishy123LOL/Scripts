using UnityEngine;

public class Forest_Behaviour : MonoBehaviour
{
    public TriggerTimeline[] timelines;
    void PlayTimeline()
    {
        string[] data = SaveManager._special;

        if (data[0] == "1")
        {
            timelines[0].PlayTimeline();
        }

        else if (data[0] == "2")
        {
            timelines[1].PlayTimeline();
        }
    }

    void Start()
    {
        FindObjectOfType<TimelineControl>().onAddedTimeline += PlayTimeline;
    }
}
