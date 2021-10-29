using UnityEngine;
using TMPro;

public class DialogueManager2 : MonoBehaviour
{
    //Dialogue
    GameObject dl_box;
    public GameObject dl_txt;
    DialogueRead dlr;
    

    void Awake(){
        dl_box = GameObject.Find("dl_box");
        dlr = FindObjectOfType<DialogueRead>();
    }
    void Start(){
        dl_box.SetActive(false);
    }

    public void TriggerDialogue(Dialogue dialogue){
        dl_box.SetActive(true);
    }

    public void DisplayDialogue(Dialogue dialogue){
        foreach (var dlg in dialogue.sentences)
        {
            char[] chra = dlg.ToCharArray();
            TextMeshProUGUI txt = Instantiate(dl_txt, dl_box.transform).GetComponent<TextMeshProUGUI>();

            string d = dlg;
            //dlr.ReturnDialogue(d, txt, 0.02f);
        }
    }
}
