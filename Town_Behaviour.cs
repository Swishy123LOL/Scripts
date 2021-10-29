using UnityEngine;
using System.Collections;

public class Town_Behaviour : MonoBehaviour
{
    public BoxCollider2D[] BlockedAreas;
    public BoxCollider2D PlayerCollider;
    public DialogueTrigger[] Dialogues;
    int index;
    int touchIndex;
    bool touched = false;
    float offset = .01f;

    void Start(){
        FindObjectOfType<DialogueManager>().OnEndDialogue += SetBack;
    }

    void Update() {
        for (int i = 0; i < 2; i++)
        {
            if (BlockedAreas[0].IsTouching(PlayerCollider) && touched == false && index == 0){
                Dialogues[0].TriggerDialogue();
                touched = true; 

                index = 1;
                touchIndex = 0;
            }

            if (BlockedAreas[1].IsTouching(PlayerCollider) && touched == false && index == 0){
                Dialogues[0].TriggerDialogue();
                touched = true; 

                index = 1;
                touchIndex = 1;

                offset = -.01f;
            }

            if (BlockedAreas[i].IsTouching(PlayerCollider) && touched == false && index == 1){
                if (i == touchIndex){
                    Dialogues[0].TriggerDialogue();
                    touched = true; 

                    index = 1;
                    touchIndex = i;
                    offset = (touchIndex == 1)? -.01f : .01f; 
                }

                if (i != touchIndex){
                    Dialogues[1].TriggerDialogue();
                    touched = true; 

                    index = 1;
                    touchIndex = i;
                    offset = (touchIndex == 1)? -.01f : .01f;
                }
            }   
        }
    }

    IEnumerator _SetBack(){
        Player player = FindObjectOfType<Player>();
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - offset);
        
        yield return new WaitForSeconds(.1f);
        touched = false;
    }
    void SetBack(){
        StartCoroutine(_SetBack());
    }
}
