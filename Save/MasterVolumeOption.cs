using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterVolumeOption : MonoBehaviour
{
    public string Percentage = "100";
    public TextMeshProUGUI Text;
    float time = 0f;
    float next = 0.025f;
    float delta = 0.025f;
    public bool muted = false;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Text.text = Percentage + "%";
        for (int i = 0; i < FindObjectOfType<PlayerSettings>().audios.Length; i++)
        {
            float volume = FindObjectOfType<PlayerSettings>().defaultVolume[i] * float.Parse(Percentage) / 100;
            FindObjectOfType<PlayerSettings>().audios[i].volume = volume;
        }
        if (muted == true)
        {
            Text.text = "MUTED";
        }
        foreach (AudioSource audio in FindObjectOfType<PlayerSettings>().audios)
        {
            audio.mute = muted;
        }
        enabled = false;
    }

    void Update()
    {
        if (muted == false)
        {
            time += Time.deltaTime;
            Text.text = Percentage + "%";
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (float.Parse(Percentage) > 0 && time > next)
                {
                    next = time + delta;
                    Percentage = (float.Parse(Percentage) - 1).ToString();
                    for (int i = 0; i < FindObjectOfType<PlayerSettings>().audios.Length; i++)
                    {
                        FindObjectOfType<PlayerSettings>().audios[i].volume = FindObjectOfType<PlayerSettings>().defaultVolume[i] * (float.Parse(Percentage) / 100);
                    }
                    next -= time;
                    time = 0f;
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (float.Parse(Percentage) < 100 && time > next)
                {
                    next = time + delta;
                    Percentage = (float.Parse(Percentage) + 1).ToString();
                    for (int i = 0; i < FindObjectOfType<PlayerSettings>().audios.Length; i++)
                    {
                        FindObjectOfType<PlayerSettings>().audios[i].volume = FindObjectOfType<PlayerSettings>().defaultVolume[i] * (float.Parse(Percentage) / 100);
                    }
                    next -= time;
                    time = 0f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            muted = !muted;
            Text.text = "MUTED";
            foreach (AudioSource audio in FindObjectOfType<PlayerSettings>().audios)
            {
                audio.mute = !audio.mute;
            }
        }
    }

    public void Enable(bool Bool)
    {
        enabled = Bool;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Percentage = SaveManager.GetSettings().masterVolume.ToString();
        muted = SaveManager.GetSettings().isMuted; //this won't work?
    }
}
