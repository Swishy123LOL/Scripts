using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public bool WithAnimation;
    public Animator animator;
    public float delayTime;
    public void LoadNextScene()
    {
        int s = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(s + 1);
    }

    public void LoadPreviousScene()
    {
        int s = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(s - 1);
    }

    public void LoadScene(int s)
    {
        if (WithAnimation == true)
        {
            if (animator == null)
            {
                return;
            }
            else
            {
                animator.SetBool("Bool", true);
            }
        }
        StartCoroutine(WaitToLoad(s));
    }

    IEnumerator WaitToLoad(int s)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(s);
    }
}
