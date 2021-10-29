using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public Animator CsAnimator;
    public bool UseInput;
    bool Interactable = false;

    void Start()
    {
        CsAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Interactable == true && Input.GetKeyDown(KeyCode.E))
        {
            CsAnimator.SetTrigger("Trigger");
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name == "Character" && UseInput == false)
        {
            CsAnimator.SetTrigger("Trigger");
        }
        if (collider2D.gameObject.name == "Character" && UseInput == true)
        {
            Interactable = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider2D)
    {
        Interactable = false;
    }
}
