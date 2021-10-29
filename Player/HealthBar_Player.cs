using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HealthBar_Player : MonoBehaviour
{
    [Header("References")]
    public Slider slider;
    public Slider slider2;
    public Slider fillCover;
    public Animator animator;
    public TextMeshProUGUI text;
    public Image[] images1;
    Color[] defColors1;
    public Image[] images2;
    Color[] defColors2;
    Player player;
    [Header("Properties")]
    public float maxHealth;
    public float slideSpeed1 = 1;
    public float slideSpeed2 = 1;
    float health;
    float health2;
    float crr;
    bool a;
    void Awake()
    {
        player = FindObjectOfType<Player>();
        maxHealth = player.PlayerHealth;
        slider.maxValue = maxHealth;
        slider2.maxValue = maxHealth;
        fillCover.maxValue = maxHealth;
        slider.value = maxHealth;

        health = slider.value;
        slider2.value = slider.value;
        health2 = health;
        crr = health;
        text.text = player.PlayerHealth.ToString();

        defColors1 = new Color[images1.Length];
        defColors2 = new Color[images2.Length];

        for (int i = 0; i < images1.Length; i++)
        {
            defColors1[i] = images1[i].color;
        }

        for (int i = 0; i < images2.Length; i++)
        {
            defColors2[i] = images2[i].color;
        }
    }

    void Update()
    {
        if (Camera.main.WorldToScreenPoint(player.transform.position).y < 168)
        {
            if (a)
            {
                foreach (var image in images1)
                {
                    image.color = Color.clear;
                }

                for (int i = 0; i < images2.Length; i++)
                {
                    images2[i].color = new Color(defColors2[i].r, defColors2[i].g, defColors2[i].b, 0.4f);
                }

                a = false;
            }
        }

        else
        {
            if (!a)
            {
                for (int i = 0; i < images1.Length; i++)
                {
                    images1[i].color = defColors1[i];
                }

                for (int i = 0; i < images2.Length; i++)
                {
                    images2[i].color = defColors2[i];
                }

                a = true;
            }
        }

        slider2.value = health2;
        fillCover.value = health2;
        health2 = Mathf.Lerp(health2, crr, Time.deltaTime * 1.5f * slideSpeed2);

        slider.value = health;
        health = Mathf.Lerp(health, crr, Time.deltaTime * 4 * slideSpeed1);
    }
    public void TakeDamage(float dmg)
    {
        if (animator) animator.ResetTrigger("Trigger");
        crr -= dmg;
        if (animator) animator.SetTrigger("Trigger");
    }

    public void Transition(bool type)
    {
        if (!type) LeanTween.moveY(gameObject, 192, 2).setEase(LeanTweenType.easeInBack);
        if (type) LeanTween.moveY(gameObject, GetComponent<RectTransform>().position.y - 100, 2).setEase(LeanTweenType.easeOutExpo);
    }
}
