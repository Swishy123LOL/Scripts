using TMPro;
using UnityEngine;

public class INTRO : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    public TextMeshProUGUI[] text2;
    public int number = 0;
    bool enable = false;
    bool enable2 = false;
    Animator animator;
    public GameObject[] destroy;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (enable == true)
        {
            if (number == -1)
            {
                number = 2;
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                number--;
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                number++;
            }
            if (number % 3 == 0)
            {
                Color alpha = new Color(.6f, .6f, .6f, .5f);
                text[0].color = Color.yellow;
                text[1].color = alpha;
                text[2].color = alpha;
            }
            if (number % 3 == 1 || number % 3 == -1)
            {
                Color alpha = new Color(.6f, .6f, .6f, .5f);
                text[1].color = Color.yellow;
                text[0].color = alpha;
                text[2].color = alpha;
            }
            if (number % 3 == 2 || number % 3 == -2)
            {
                Color alpha = new Color(.6f, .6f, .6f, .5f);
                text[2].color = Color.yellow;
                text[1].color = alpha;
                text[0].color = alpha;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (TextMeshProUGUI text in text)
                {
                    Destroy(text);
                }
                animator.SetBool("1", true);
                number = 0;
                enable = false;
            }
        }

        if (enable2 == true)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                number--;
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                number++;
            }

            if (number % 2 == 0)
            {
                Color alpha = new Color(.6f, .6f, .6f, .5f);
                text2[0].color = Color.yellow;
                text2[1].color = alpha;
            }

            if (number % 2 != 0)
            {
                Color alpha = new Color(.6f, .6f, .6f, .5f);
                text2[1].color = Color.yellow;
                text2[0].color = alpha;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (TextMeshProUGUI text in text2)
                {
                    Destroy(text);
                }
                animator.SetBool("2", true);
                enable2 = false;
            }
        }
    }

    public void SetBool1()
    {
        enable = true;
    }

    public void SetBool2()
    {
        enable2 = true;
    }

    public void Destroy()
    {
        foreach (GameObject gameObject in destroy)
        {
            Destroy(gameObject);
        }
    }
}
