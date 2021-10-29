using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Tutorial_Visual5 : MonoBehaviour
{
    public TextMeshProUGUI text1, text2;
    public EnemyStat stat;
    public Slider slider;
    public DialogueTrigger[] dialogues;
    public float speed = 1;
    float max;
    float value;
    double preValue;
    double percentage;
    bool a,b,c,d,e;

    void Start()
    {
        max = stat.Health;
        slider.minValue = -max;
        slider.value = -max;
        value = max;

        StartCoroutine(enumerator());
    }

    void Update()
    {
        value = Mathf.Lerp(value, stat.Health, Time.deltaTime * speed);
        slider.value = -value;
        percentage = Math.Round(100 - (value * 100 / max), 1);

        text1.text = string.Format("{0}%", percentage);
        text2.text = string.Format("{0}%", percentage);

        if (preValue != Math.Round(value))
        {
            preValue = Math.Round(value);
            FindObjectOfType<Tutorial_Corruption>().Corrupt((int)Math.Round(percentage));
            FindObjectOfType<Tutorial_Corruption>().corruptionLevel = (int)Math.Round(percentage);
        }
    }

    IEnumerator enumerator()
    {
        while (true)
        {
            if (percentage > 0 && !a)
            {
                a = true;
                dialogues[0].TriggerDialogue();
            }

            if (percentage >= 25 && !b)
            {
                b = true;
                dialogues[1].TriggerDialogue();

                yield return new WaitForSeconds(2);
                FindObjectOfType<DialogueManager>().DisplayNextSentences();
            }

            if (percentage >= 50 && !c)
            {
                c = true;
                dialogues[2].TriggerDialogue();

                yield return new WaitForSeconds(2);
                FindObjectOfType<DialogueManager>().DisplayNextSentences();
            }

            if (percentage >= 90 && !d)
            {
                d = true;
                dialogues[3].TriggerDialogue();

                yield return new WaitForSeconds(2);
                FindObjectOfType<DialogueManager>().DisplayNextSentences();
            }

            yield return null;
        }
    }
}
