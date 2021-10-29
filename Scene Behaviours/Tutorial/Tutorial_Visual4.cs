using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Visual4 : MonoBehaviour
{
    public TextMeshProUGUI text1, text2;
    public E_DummyBehaviour behaviour;
    public Slider slider;
    public float speed = 1;
    bool a;

    public void EnableSlider()
    {
        a = true;
    }

    void Update()
    {
        if (a)
        {
            slider.value = Mathf.Lerp(slider.value, behaviour.a, Time.deltaTime * speed);

            text1.text = string.Format("{0}/6", behaviour.a.ToString());
            text2.text = string.Format("{0}/6", behaviour.a.ToString());
        }
    }
}
