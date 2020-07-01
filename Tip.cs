using UnityEngine;
using Cinemachine;
using TMPro;

public class Tip : MonoBehaviour
{
    public bool tip = true;
    public GameObject boss;

    public DialogueTrigger dialogueTrigger;
    public Animator animator;
    public bool finaltip = false;
    public CinemachineVirtualCamera Cam1;
    public CinemachineVirtualCamera Cam2;
    bool tip3;
    bool tip5;
    public TextMeshProUGUI text;
    public TextMeshProUGUI back;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (tip == true && boss.activeSelf == true)
        {
            tip = false;
            animator.SetBool("IsTip", true);
            FindObjectOfType<DialogueManager>().TextSpeed = 0.003f;
        }
    }

    void FixedUpdate()
    {
        if (tip3 && Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            animator.SetBool("Tip3", true);
        }
        if (tip5 && Input.GetKeyDown(KeyCode.Space)) 
        {
            animator.SetBool("Tip5", true);
        }
    }

    public void SetBool1()
    {
        tip3 = true;
    }

    public void SetBool2() 
    {
        tip5 = true;
    }

    public void SwitchTehCam() 
    {
        Cam1.Priority = 5;
        Cam2.Priority = 10; 
    }

    public void SwitchTehCamBack() 
    {
        Cam2.Priority = 5;
        Cam1.Priority = 10; 
    }

    public void NowDodge() 
    {
        FindObjectOfType<PlayerMovement>().moveSpeed = 0f;
        FindObjectOfType<DialogueManager>().dialogueText = text;
        Color color = Color.white;
        FindObjectOfType<DialogueManager>().m_DialogueText.color = color;
        FindObjectOfType<DialogueManager>().nameText.color = color;
        FindObjectOfType<DialogueManager>().Image.color = color;
        FindObjectOfType<DialogueManager>().rawImage.color = color;
    }

    public void TehBack() 
    {
        FindObjectOfType<PlayerMovement>().moveSpeed = 10f;
        FindObjectOfType<DialogueManager>().dialogueText = back;
        
        Color color = Color.clear;
        FindObjectOfType<DialogueManager>().m_DialogueText.color = color;
        FindObjectOfType<DialogueManager>().nameText.color = color;
        FindObjectOfType<DialogueManager>().Image.color = color;
        FindObjectOfType<DialogueManager>().rawImage.color = color;
    }

    public void FinalTip() 
    {
        finaltip = true;
    }
}
