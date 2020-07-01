using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [HideInInspector]
    public bool Interactable = false;

    float currentTime;

    public GameObject[] CsGameobject;

    public GameObject[] Player;

    public CinemachineVirtualCamera[] cam;

    public GameObject[] DialogueTexts;

    public Animator[] Animator;

    public string[] sounds;

    bool FinalEnd = false;
    void Start()
    {
        
    }
    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<DialogueManager>().StartImage(dialogue);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Interactable == true && DialogueManager.IsPlaying == false)
        {
            TriggerDialogue();

            FindObjectOfType<DialogueManager>().iscutscene = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && FinalEnd == true)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentences();

            foreach (GameObject gameObject in Player)
            {
                gameObject.SetActive(true);
            }

            foreach (GameObject gameObject in CsGameobject)
            {
                gameObject.SetActive(false);
            }

            foreach (string sound in sounds)
            {
                FindObjectOfType<AudioManager>().Stop(sound);
            }

            cam[0].Priority = 10;
            cam[1].Priority = 5;

            Destroy(gameObject);
        }


    }
    void OnTriggerEnter2D (Collider2D collider2D)
    {
        Interactable = true;
    }
    void OnTriggerExit2D(Collider2D collider2D)
    {
        Interactable = false;
    }

    #region CutSceneManagement
    public void StartCs()
    {
        foreach (GameObject gameObject in Player)
        {
            gameObject.SetActive(false);
        }
    }
    public void DisplayNext()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentences();
        FindObjectOfType<DialogueManager>().DisplayNextSprite();
    }

    public void EndCs()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();

        foreach (GameObject gameObject in Player)
        {
            gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in CsGameobject)
        {
            gameObject.SetActive(false);
        }
    }

    public void LockDialogue()
    {
        FindObjectOfType<DialogueManager>().iscutscene = true;
    }
    public void FinalEndCs()
    {
        FinalEnd = true;
    }

    public void GetPosition()
    {
        for (int i = 0; i < Player.Length; i++)
        {
            Player[i].transform.position = new Vector3(CsGameobject[i].transform.position.x, CsGameobject[i].transform.position.y, Player[i].transform.position.z);
        }
    }

    public void SwitchCam()
    {
        cam[0].Priority = 5;
        cam[1].Priority = 10;
    }

    public void HideDialogueBox()
    {
        foreach (GameObject game in DialogueTexts)
        {
            game.SetActive(false);
        }
    }

    public void ShowDialogueBox()
    {
        foreach (GameObject game in DialogueTexts)
        {
            game.SetActive(true);
        }
    }

    public void TriggerAnim1()
    {
        Animator[0].SetTrigger("Bool");
    }

    public void TriggerAnim2()
    {
        Animator[1].SetTrigger("Bool");
    }


    public void PlaySound1()
    {     
        FindObjectOfType<AudioManager>().Play(sounds[0]);
    }

    public void PlaySound2()
    {  
        FindObjectOfType<AudioManager>().Play(sounds[1]);
    }

    public void PlaySound3()
    {
       
        FindObjectOfType<AudioManager>().Play(sounds[2]);
    }

    public void PlaySound4()
    {
        FindObjectOfType<AudioManager>().Play(sounds[3]);
    }

    public void PlaySound5()
    {
        FindObjectOfType<AudioManager>().Play(sounds[4]);
    }

    public void PlaySound6()
    {
        FindObjectOfType<AudioManager>().Play(sounds[5]);
    }

    public void PlaySound7()
    {
        FindObjectOfType<AudioManager>().Play(sounds[6]);
    }

    public void PlaySound8()
    {
        FindObjectOfType<AudioManager>().Play(sounds[7]);
    }

    public void StopAllSound()
    {
        foreach (string sound in sounds)
        {
            FindObjectOfType<AudioManager>().Stop(sound);
        }
    }

    public void ShakeCamera()
    {
        CameraShaker.Instance.ShakeOnce(4f, 6f, 1.5f, 2f);
    }

#endregion
}
