using UnityEngine;

public class DialogueTrigger2 : MonoBehaviour
{
    public Dialogue dialogue;
    DialogueManager2 dlgmng;

    void Awake(){
        dlgmng = FindObjectOfType<DialogueManager2>();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.E)){
            dlgmng.TriggerDialogue(dialogue);
        }
    }
}
