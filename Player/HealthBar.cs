using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    float currentHealth;

    public GameObject ObjectParent;

    public Image[] images;

    Color color;

    public Color[] newcolor;
    void Start()
    {
        color.a = 0;

        for (int i = 0; i < newcolor.Length; i++)
        {
            newcolor[i] = images[i].color;
        }
    }
    void Update()
    {
        currentHealth = slider.value;

        if (ObjectParent.activeSelf == false)
        {
            foreach (Image image in images)
            {
                image.color = color;
            }
        }

        else
        {
            for (int i = 0; i < newcolor.Length; i++)
            {
                images[i].color = newcolor[i];
            }
        }

        bool thing = ObjectParent.activeSelf;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
