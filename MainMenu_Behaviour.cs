using System;
using UnityEngine;

public class MainMenu_Behaviour : MonoBehaviour
{
    public TriggerTimeline[] timelines;
    public static Action OnNext;
    public static Action OnBack;
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnNext?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnBack?.Invoke();
        }
    }
}
