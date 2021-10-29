using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    public bool TransitionAtStart;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (TransitionAtStart == true)
        {
            animator.SetBool("Bool3", true);
        }
    }
}
