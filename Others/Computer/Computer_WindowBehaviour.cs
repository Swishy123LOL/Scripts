using UnityEngine;
using UnityEngine.UI;

public class Computer_WindowBehaviour : MonoBehaviour
{
    public GameObject[] Windows;
    public Image ComputerLayout;

    [Space]
    [Header("Required Property")]
    public LeanTweenType EaseType;
    public GameObject BlankCover;
    public AudioManager audioManager;

    [Space]
    [Header("Events")]
    public Button.ButtonClickedEvent OnWindowOpen;
    public Button.ButtonClickedEvent OnWindowExit;

    GameObject[] _Windows;

    void Start()
    {
        _Windows = new GameObject[Windows.Length];
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha7)){
            StopAllSounds();
        }
    }
    public void EnableWindow(int index)
    {
        BlankCover.SetActive(true);
        GameObject gameobject = Instantiate(Windows[index], transform);
        _Windows[index] = gameobject;

        RectTransform rect = gameobject.GetComponent<RectTransform>();
        rect.localScale = new Vector2(1.4f, 1.4f);

        Image image = gameobject.GetComponent<Image>();
        image.SetNativeSize();

        Vector3 def = rect.localScale;

        rect.localScale = def * .96f;
        LeanTween.scale(gameobject, def, .5f).setEase(EaseType);
    }

    public void DisableWindow(int index)
    {
        ComputerLayout.color = Color.white;
        
        Destroy(_Windows[index]);
        BlankCover.SetActive(false);
    }

    public void OnExit()
    {
        IOnWindowExit onWindowExit = gameObject.GetComponent<IOnWindowExit>();
        onWindowExit.OnWindowExit();
    }

    public void Playsound(string sound)
    {
        audioManager.Play(sound);
    }

    public void StopAllSounds(){
        foreach (var sound in audioManager.sounds)
        {
            audioManager.Stop(sound.name);
        }
    }
    public void PlaysoundWithDifferentAttribute(string sound, float volume = 1f, float pitch = 1f)
    {
        audioManager.Play(sound);
        audioManager.SetVolume(sound, volume);
        audioManager.SetPitch(sound, pitch);
    }
}
