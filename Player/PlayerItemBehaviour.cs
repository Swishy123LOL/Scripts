using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerItemBehaviour : MonoBehaviour
{
    public TextMeshProUGUI[] Texts;
    public TextMeshProUGUI ItemCount;
    public ButtonControl ItemButtonControl;
    public ButtonControl OptionButtonControl;
    public DialogueTrigger ThrowDialogue;
    public DialogueTrigger DescriptionDialogue;
    public DialogueTrigger UseDialogue;
    int e;

    void Start()
    {
        UpdateItem();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerItemManager.AddItem("Pancake" + e, "PANCAKE - HEAL ?? HP | Not a healing item, but tasty indeed", "You ate the Pancake..........you recovered........your hunger ?");
            e++;
            UpdateItem();
        }
    }

    public void UpdateItem()
    {
        for (int i = 0; i < Texts.Length; i++)
        {
            Texts[i].text = PlayerItemManager.GetName(i);
        }
        ItemCount.text = PlayerItemManager.playerItems.Count.ToString() + "/11"; 
    }

    public void AddItem(string name, string description, string useDialogue, bool destroyAfterUse = false, int heal = 0)
    {
        PlayerItemManager.AddItem(name, description, useDialogue, destroyAfterUse, heal);
        UpdateItem();
    }

    public void RemoveItem()
    {
        StopAllCoroutines();
        OptionButtonControl.FREEZE = true;
        ThrowDialogue.dialogue.sentences[1] = "Throw " + PlayerItemManager.GetName(ItemButtonControl.currentIndex - 1) + " away ?";
        ThrowDialogue.TriggerDialogue();
        ThrowDialogue.DisplayNext();
    }

    public void ConfirmRemoveItem()
    {
        PlayerItemManager.RemoveItem(ItemButtonControl.currentIndex - 1);
        UpdateItem();
    }

    public void EnableButtonControl(int index)
    {
        StartCoroutine(Enable(index));
    }

    public void EnableButtonControl2()
    {
        StartCoroutine(Enable2());
    }

    IEnumerator Enable(int index)
    {
        yield return new WaitForSeconds(4.5f / 6f);
        OptionButtonControl.CustomTriggerNext(index);

        if (ItemButtonControl.texts[0].text == "")
        {
            OptionButtonControl.CustomTriggerNext(1);
        }
        yield return new WaitForSeconds(0.02f);
        OptionButtonControl.FREEZE = false;
    }

    public void DisplayDescription()
    {
        StopAllCoroutines();
        DescriptionDialogue.dialogue.sentences[0] = PlayerItemManager.GetDecription(ItemButtonControl.currentIndex - 1);
        DescriptionDialogue.TriggerDialogue();
        OptionButtonControl.FREEZE = true;
    }

    public void DisplayUseDialogue()
    {
        StopAllCoroutines();
        UseDialogue.dialogue.sentences[0] = PlayerItemManager.GetUseDialogue(ItemButtonControl.currentIndex - 1);
        UseDialogue.TriggerDialogue();
        OptionButtonControl.FREEZE = true;
    }

    IEnumerator Enable2()
    {
        yield return new WaitForSeconds(0.1f);
        OptionButtonControl.FREEZE = false;
    }
}
