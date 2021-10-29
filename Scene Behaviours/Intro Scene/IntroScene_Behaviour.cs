using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene_Behaviour : MonoBehaviour
{
    Animator animator;
    AudioManager audioManager;
    DialogueManager dialogueManager;
    public GameObject Computer;
    void Start() {
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        dialogueManager.OnEndDialogue += SetCutscene;
    }
    void SetCutscene() {
        animator.SetBool("4", true);
    }

    void Update()
    {
        float? length = audioManager.GetLength("IntroClock_First");
        if (audioManager.Time("IntroClock_First") == length)
        {
            SceneManager.LoadScene(2);
        }
    }
}
