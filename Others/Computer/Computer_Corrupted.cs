using UnityEngine;
using UnityEngine.UI;

public class Computer_Corrupted : MonoBehaviour
{
    public Image WhiteCover;
    public Image InvisibleCover;
    public float value;
    public AnimationCurve curve;
    public Animator animator;
    bool transition;
    float time;

    void Update(){
        if (transition == true) {
            time += Time.deltaTime;

            value = curve.Evaluate(time);
            WhiteCover.color = new Color(1, 1, 1, value);

            if (time >= 1) {
                FindObjectOfType<Computer_WindowBehaviour>().DisableWindow(0);
                animator.SetBool("2", true);
            }

            if (value == 0) {
                time = 0;
                transition = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.B)){
            Transition();
        }
    }

    public void Transition() {
        transition = true;
    }

    public void TriggerMouse() {
        Cursor.visible = true;
        InvisibleCover.gameObject.SetActive(false);
    }

    public void EntriggerMouse() {
        Cursor.visible = false;
        InvisibleCover.gameObject.SetActive(true); 
    }
}
