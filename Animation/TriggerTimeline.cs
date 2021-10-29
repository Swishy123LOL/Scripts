using UnityEngine;
public class TriggerTimeline : MonoBehaviour
{
    public BoxCollider2D trigger;
    BoxCollider2D playerCollider;
    [Header("Booleans")]
    public bool triggerOnStart;
    public bool save;
    [Header("Properties")]
    public int timelineIndex;
    public int saveIndex;
    TimelineControl timeline;
    bool triggered;

    void Awake(){
        playerCollider = FindObjectOfType<Player>()?.GetComponent<BoxCollider2D>();
        timeline = FindObjectOfType<TimelineControl>();

        if (triggerOnStart == true){
            timeline.onAddedTimeline += PlayTimeline;
        }
    }

    void Update(){
        if (trigger != null){
            if (trigger.IsTouching(playerCollider) && triggered == false){
                PlayTimeline(); 
                triggered = true;      
            }
        }
    }

    public void PlayTimeline(){
        if (save)
        {
            if (!SaveManager.cScene[saveIndex])
            {
                SaveManager.cScene[saveIndex] = true;
                timeline.PlayTimeline(timelineIndex);
            }
        }

        else
        {
            timeline.PlayTimeline(timelineIndex);
        }

        if (triggerOnStart == true)
        {
            timeline.onAddedTimeline -= PlayTimeline;
        }
    }
}
