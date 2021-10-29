using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Boss : MonoBehaviour
{
    [Header("References")]
    public Slider slider;
    public Slider slider2;
    public Slider fillCover;
    public Animator animator;
    [Header("Properties")]
    public float maxHealth;
    public float slideSpeed1 = 1;
    public float slideSpeed2 = 1;
    float health;
    float health2;
    float crr;
    void Start(){
        slider.maxValue = maxHealth;
        slider2.maxValue = maxHealth;
        fillCover.maxValue = maxHealth;
        slider.value = maxHealth;

        health = slider.value;
        slider2.value = slider.value;
        health2 = health;
        crr = health;
    }

    void Update(){
        slider2.value = health2;
        fillCover.value = health2;
        health2 = Mathf.Lerp(health2, crr, Time.deltaTime * 1.5f * slideSpeed2);

        slider.value = health;
        health = Mathf.Lerp(health, crr, Time.deltaTime * 4 * slideSpeed1);
    }
    public void TakeDamage(float dmg){
        if (animator) animator.ResetTrigger("Trigger");
        crr -= dmg;
        if (animator) animator.SetTrigger("Trigger");
    }

    public void Transition(bool type){
        if (!type) LeanTween.moveY(gameObject, 192, 2).setEase(LeanTweenType.easeInBack);
        if (type) LeanTween.moveY(gameObject, GetComponent<RectTransform>().position.y - 100, 2).setEase(LeanTweenType.easeOutExpo);
    }
}
