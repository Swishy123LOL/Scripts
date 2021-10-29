using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    public float secondrunnin;
    public int seconds;
    public int minutes;
    public string hours;
    public float speed = 1f;
    public int GlobalIndex;
    [Range(0, 10)]
    public float TIMESCALE = 1;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        Time.timeScale = TIMESCALE;
    }
    void Update()
    {
        secondrunnin += Time.deltaTime * speed;
    }

    public void SetGlobalIndex(int index)
    {
        GlobalIndex = index;
    }
}
